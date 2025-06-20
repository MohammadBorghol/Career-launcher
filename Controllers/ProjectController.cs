using AIGenerator.Data;
using AIGenerator.DTOs;
using AIGenerator.Interface;
using AIGenerator.Models;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using System.Threading.Tasks;

namespace AIGenerator.Controllers
{
    public class ProjectController : Controller
    {
        public IProjectRepository _ProjectRepo { get; set; }

        public ApplicationDbContext _context { get; set; }

        public ProjectController(IProjectRepository projectRepo, ApplicationDbContext context)
        {
            this._ProjectRepo = projectRepo;
           
            this._context = context;
        }

        public IActionResult Index(int portfolioId)
        {

            ViewBag.projects = _ProjectRepo.GetAllProjects(portfolioId);
            ViewBag.portfolioId = portfolioId;
            return View();
        }


        public IActionResult Add(int portfolioId)
        {
            ViewBag.services = _context.Services.ToList();

            return View();
        }


        public async Task<IActionResult> Create(ProjectCreateDTO createDTO)
        {
            if (createDTO.projectImage != null && createDTO.projectImage.Length > 0)
            {
                byte[] byteFile = null;

                Stream st = createDTO.projectImage.OpenReadStream();
                using (BinaryReader br = new BinaryReader(st))
                {
                    byteFile = br.ReadBytes((int)st.Length);
                }

                // Load the service from the database to populate the navigation property
                var service = await _context.Services.FindAsync(createDTO.serviceId);

                Project project = new Project
                {
                    portfolioId = createDTO.portfolioId,
                    ProjectName = createDTO.ProjectName,
                    projectLink = createDTO.projectLink,
                    projectDescription = createDTO.projectDescription,
                    startDate = createDTO.startDate,
                    endDate = createDTO.endDate,
                    serviceId = createDTO.serviceId,
                    service = service, 
                    projectImageName = createDTO.projectImage.FileName,
                    projectImageContentType = createDTO.projectImage.ContentType,
                    projectImageFile = byteFile,
                    isDeleted = false
                };

                _ProjectRepo.Create(project);
            }

            return RedirectToAction("Index", new { portfolioId = createDTO.portfolioId });
        }



        public async Task<IActionResult> Edit(int projectId)
        {

            ViewBag.services = _context.Services.ToList();


            var project =await _ProjectRepo.GetProjectById(projectId);

            ProjectUpdateDTO dto = new ProjectUpdateDTO();
            Portfolio portfolio = new Portfolio();
            dto.projectId = projectId;
            dto.ProjectName = project.ProjectName;
            dto.projectDescription = project.projectDescription;
            dto.startDate = project.startDate;
            dto.endDate = project.endDate;
            dto.serviceId = project.serviceId;          
            dto.projectLink = project.projectLink;
            dto.portfolioId = project.portfolioId??0;

          

            return View(dto);
        }

        public async Task<IActionResult> Update(ProjectUpdateDTO updateDTO)
        {
            // Fetch the existing project from the database
            var oldProject = await _ProjectRepo.GetProjectById(updateDTO.projectId);

            if (oldProject == null)
            {
                // Handle the case where the project is not found
                return NotFound();
            }

            // Update the basic properties
            oldProject.ProjectName = updateDTO.ProjectName;
            oldProject.projectDescription = updateDTO.projectDescription;
            oldProject.startDate = updateDTO.startDate;
            oldProject.endDate = updateDTO.endDate;
            oldProject.projectLink = updateDTO.projectLink;
            oldProject.serviceId = updateDTO.serviceId;
            oldProject.isDeleted = false;

            // Check if a new image file was uploaded
            if (updateDTO.projectImage != null && updateDTO.projectImage.Length > 0)
            {
                using var stream = updateDTO.projectImage.OpenReadStream();
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                oldProject.projectImageFile = ms.ToArray();
                oldProject.projectImageName = updateDTO.projectImage.FileName;
                oldProject.projectImageContentType = updateDTO.projectImage.ContentType;
            }

            // Save changes via repository
            await _ProjectRepo.Update(oldProject);

            // Redirect back to the portfolio's project list or details
            return RedirectToAction("Index", new { portfolioId = oldProject.portfolioId });
        }


        public IActionResult Delete(int projectId)
        {

            _ProjectRepo.Delete(projectId);

            return RedirectToAction("Index");
        }
    }
}
