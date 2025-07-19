using AIGenerator.Data;
using AIGenerator.DTOs;
using AIGenerator.Interface;
using AIGenerator.Models;
using Microsoft.EntityFrameworkCore;
using static AIGenerator.Repository.ResumeRepository;


namespace AIGenerator.Repository
{
    public class ResumeRepository : IResumeRepository
    {
            private readonly ApplicationDbContext _context;

            public ResumeRepository(ApplicationDbContext context)
            {
               this._context = context;
            }

            public List<Resume> GetAllResumes(string UserId)
            {
            return _context.Resumes
                     .Include(x => x.certificates)
                     .Include(x => x.educations)
                     .Include(x => x.experiences)
                     .Include(x => x.languages)
                     .Include(x => x.skills)
                     .Where(x => x.endUserId == UserId && !x.isDeleted)
                     .ToList();

            }

            public async Task<Resume> GetResumeById(int id)
            {
                return _context.Resumes
                 .Include(x => x.certificates)
                     .Include(x => x.educations)
                     .Include(x => x.experiences)
                     .Include(x => x.languages)
                     .Include(x => x.skills)
                .SingleOrDefault(x => x.resumeId == id);
            }

            public async Task Create(Resume resume, string userId)
        {
            var now = DateTime.UtcNow.ToString("yyyy-MM-dd");

            resume.createDate = now;
            resume.ModifiedDate = now;
            resume.isDeleted = false;
            resume.endUserId = userId;

           
           

            await _context.Resumes.AddAsync(resume);
            await _context.SaveChangesAsync();
        }

            public async Task Update(Resume resume)
            {        
                _context.Resumes.Update(resume);
            await _context.SaveChangesAsync();
        }

            public void Delete(int id)
            {
                var resume = _context.Resumes.Find(id);
                if (resume != null)
                {
                resume.isDeleted = true;

                    _context.SaveChanges();
                }
            }

        }

    }

