
using AIGenerator.Data;
using AIGenerator.DTOs;
using AIGenerator.Interface;
using AIGenerator.Models;
using AIGenerator.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SQLitePCL;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AIGenerator.Controllers
{
    public class ResumeController : Controller
    {
        public IResumeRepository _ResumeRepo { get; set; }

        public ICVParser _parser { get; set; }

        public ApplicationDbContext _context { get; set; }

        public ResumeController(IResumeRepository resumeRepo, ICVParser parser, ApplicationDbContext context)
        {
            this._ResumeRepo = resumeRepo;
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
            ViewBag.resumes = _ResumeRepo.GetAllResumes(CurrentUserId);

            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> Create(ResumeCreateDTO dto)
        {     

                dto.userInput = string.Join("\n", new[]
                   {
                    $"Email: {dto.email}",
                    $"Address: {dto.address}",
                    $"PhoneNumber: {dto.phoneNumber}",
                    $"linkedInLink: " + (dto.linkedInLink != null ? dto.linkedInLink.Trim() : "N/A"),
                    $"facebookLink: " + (dto.facebookLink != null ? dto.facebookLink.Trim() : "N/A"),
                    $"githubLink: " + (dto.githubLink != null ? dto.githubLink.Trim() : "N/A"),
                    $"Additional Info:\n{dto.userPrompt}"
                    }.Where(p => !string.IsNullOrWhiteSpace(p)));

                var resume = await _parser.ParseCvAsync(dto.userInput);

                var currentUserId = GetUserId();
                resume.userPrompt = dto.userPrompt;
                await _ResumeRepo.Create(resume, currentUserId);

                return RedirectToAction("ParsedResume", new { resumeId = resume.resumeId });
            
           
        }


        public async Task<IActionResult> Edit(int resumeId)
        {
            var resume = await _ResumeRepo.GetResumeById(resumeId);

            ResumeUpdateDTO dto = new ResumeUpdateDTO();

            dto.resumeId = resumeId;
            dto.email = resume.email;
            dto.address = resume.address;
            dto.phoneNumber = resume.phoneNumber;
            dto.linkedInLink = resume.linkedInLink;
            dto.facebookLink = resume.facebookLink;
            dto.githubLink = resume.githubLink;
            dto.userPrompt = resume.userPrompt;

            return View(dto);
        
        
        }


        public async Task<IActionResult> Update(ResumeUpdateDTO dto)
        {
            var currentUserId = GetUserId();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           
            var newParsedResume = await _parser.ParseCvAsync(dto.userInput);

            
            var oldResume = await _ResumeRepo.GetResumeById(dto.resumeId);
            if (oldResume == null)
                return NotFound();

            
            oldResume.firstName = newParsedResume.firstName;
            oldResume.secondName = newParsedResume.secondName;
            oldResume.thirdName = newParsedResume.thirdName;
            oldResume.lastName = newParsedResume.lastName;
            oldResume.email = newParsedResume.email;
            oldResume.phoneNumber = newParsedResume.phoneNumber;
            oldResume.linkedInLink = newParsedResume.linkedInLink;
            oldResume.githubLink = newParsedResume.githubLink;
            oldResume.facebookLink = newParsedResume.facebookLink;
            oldResume.address = newParsedResume.address;
            oldResume.DateOfBirth = newParsedResume.DateOfBirth;
            oldResume.summary = newParsedResume.summary;
            oldResume.title = newParsedResume.title;

           
            oldResume.ModifiedDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
            oldResume.userPrompt = dto.userPrompt;

            
            oldResume.educations.Clear();
            oldResume.educations.AddRange(newParsedResume.educations);

            oldResume.experiences.Clear();
            oldResume.experiences.AddRange(newParsedResume.experiences);

            oldResume.skills.Clear();
            oldResume.skills.AddRange(newParsedResume.skills);

            oldResume.languages.Clear();
            oldResume.languages.AddRange(newParsedResume.languages);

            oldResume.certificates.Clear();
            oldResume.certificates.AddRange(newParsedResume.certificates);

           
            await _ResumeRepo.Update(oldResume);

            return RedirectToAction("ParsedResume", new { resumeId = oldResume.resumeId });
        }


        public IActionResult Delete(int resumeId)
        {
            _ResumeRepo.Delete(resumeId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ParsedResume(int resumeId)
        {
            Resume resume = new Resume();
            resume =await _ResumeRepo.GetResumeById(resumeId);
            return View("ParsedResume",resume);
        }

    }
}
