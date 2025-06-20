using AIGenerator.Data;
using AIGenerator.DTOs;
using AIGenerator.Interface;
using AIGenerator.Models;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AIGenerator.Controllers
{
    public class PortfolioController : Controller
    {
        public IPortfolioRepository _PorfolioRepo { get; set; }

        public IPortfolioParser _parser { get; set; }

        public ApplicationDbContext _context { get; set; }

        public PortfolioController(IPortfolioRepository porfolioRepo, IPortfolioParser parser, ApplicationDbContext context)
        {
            this._PorfolioRepo = porfolioRepo;
            this._parser = parser;
            this._context = context;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public IActionResult Index()
        {
            var CurrentUserId = GetUserId();
            ViewBag.portfolios = _PorfolioRepo.GetAllPortfolios(CurrentUserId);

            return View();
        }

        public IActionResult Add()
        {
            //get all services
            ViewBag.services = _context.Services.ToList();

            return View();
        }

        public async Task<IActionResult> Create(PortfolioCreateDTO createDTO, string userInput)
        {


            //  var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", createDTO.portfolioImage.FileName);
            if (createDTO.portfolioImage.Length > 0)
            {
                byte[] byteFile = null;

                Stream st = createDTO.portfolioImage.OpenReadStream();
                using (BinaryReader br = new BinaryReader(st))
                {
                    byteFile = br.ReadBytes((int)st.Length);
                }



                Portfolio parsedPortfolio = await _parser.ParsePortfolioAsync(userInput);

                parsedPortfolio.endUserId = GetUserId();
                parsedPortfolio.portfolioImageName = createDTO.portfolioImage.FileName;
                parsedPortfolio.portfolioImageContentType = createDTO.portfolioImage.ContentType;
                parsedPortfolio.portfolioImageFile = byteFile;

                var savedPortfolio = _PorfolioRepo.Create(parsedPortfolio);

                foreach (var sid in createDTO.serviceIds)
                {
                    _context.portfolioServices.Add(new PortfolioService()
                    {
                        serviceId = sid,
                        portfolioId = savedPortfolio.portfolioId
                    });
                }

               for (int i = 0; i < createDTO.projects.Count; i++)
{
    var pro = createDTO.projects[i];
    var image = createDTO.projectImages != null && createDTO.projectImages.Count > i
        ? createDTO.projectImages[i]
        : null;

    byte[]? imageBytes = null;
    string? imageName = null;
    string? contentType = null;

    if (image != null)
    {
        using var ms = new MemoryStream();
        await image.CopyToAsync(ms);
        imageBytes = ms.ToArray();
        imageName = image.FileName;
        contentType = image.ContentType;
    }

    _context.Projects.Add(new Project
    {
        portfolioId = savedPortfolio.portfolioId,
        ProjectName = pro.ProjectName,
        projectDescription = pro.projectDescription,
        projectLink = pro.projectLink,
        startDate = pro.startDate,
        endDate = pro.endDate,
        serviceId = pro.serviceId,
        isDeleted = false,
        projectImageFile = imageBytes,
        projectImageName = imageName,
        projectImageContentType = contentType
    });
}


            }

            await _context.SaveChangesAsync();
            
                return RedirectToAction("Index");
            
        }

        public async Task<IActionResult> Edit(int portfolioId)
        {

            ViewBag.services = _context.Services.ToList();

            Portfolio portfolio = new Portfolio();
            portfolio= await _PorfolioRepo.GetPortfolioById(portfolioId);

            if (portfolio == null) return NotFound();

            var dto = new PortfolioUpdateDTO
            {
                email = portfolio.email,
                phoneNumber = portfolio.phoneNumber,
                address = portfolio.address,
                githubLink = portfolio.githubLink,
                linkedInLink = portfolio.linkedInLink,
                facebookLink = portfolio.facebookLink,
                userPrompt = portfolio.userPrompt,               

                serviceIds = portfolio.portfolioServices?
            .Select(ps => ps.serviceId)
            .ToList() ?? new List<short>()
              
            };

            return View(dto);
        }

        public async Task <IActionResult> Update(PortfolioUpdateDTO dto)
        {


            if (dto.portfolioImage.Length > 0)
            {
                byte[] byteFile = null;

                Stream st = dto.portfolioImage.OpenReadStream();
                using (BinaryReader br = new BinaryReader(st))
                {
                    byteFile = br.ReadBytes((int)st.Length);
                }

                var portfolio = await _context.Portfolios
        .Include(p => p.portfolioServices)
        .SingleOrDefaultAsync(p => p.portfolioId == dto.portfolioId);


                Portfolio parsedPortfolio = new Portfolio();
                parsedPortfolio = await _parser.ParsePortfolioAsync(dto.userInput);


                if (portfolio == null)
                {
                    return NotFound();
                }

                // Update simple fields

                portfolio.endUserId = GetUserId();
                portfolio.phoneNumber = parsedPortfolio.phoneNumber;
                portfolio.firstName = parsedPortfolio.firstName;
                portfolio.lastName = parsedPortfolio.lastName;
                portfolio.secondName = parsedPortfolio.secondName;
                portfolio.thirdName = parsedPortfolio.thirdName;
                portfolio.email = parsedPortfolio.email;
                portfolio.address = parsedPortfolio.address;
                portfolio.title = parsedPortfolio.title;
                portfolio.summary = parsedPortfolio.summary;
                portfolio.DateOfBirth = parsedPortfolio.DateOfBirth;
                portfolio.linkedInLink = parsedPortfolio.linkedInLink;
                portfolio.githubLink = parsedPortfolio.githubLink;
                portfolio.facebookLink = parsedPortfolio.facebookLink;
                portfolio.userPrompt = dto.userPrompt;
                portfolio.portfolioImageFile = byteFile;
                portfolio.portfolioImageName = dto.portfolioImage.FileName;
                portfolio.portfolioImageContentType = dto.portfolioImage.ContentType;



                // 🔁 Update the many-to-many relation
                // Remove existing links
                portfolio.portfolioServices.Clear();

                // Add new PortfolioService entries
                foreach (var serviceId in dto.serviceIds.Distinct())
                {
                    portfolio.portfolioServices.Add(new PortfolioService
                    {
                        portfolioId = dto.portfolioId,
                        serviceId = serviceId
                    });
                }

                await _PorfolioRepo.Update(portfolio);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int portfolioId)
        {
            _PorfolioRepo.Delete(portfolioId);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> PortfolioView(int portfolioId)
        {
            var Portfolio =await _PorfolioRepo.GetPortfolioById(portfolioId);
            return View(Portfolio);
        }

    }
}
