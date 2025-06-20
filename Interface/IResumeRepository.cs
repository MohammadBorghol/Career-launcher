using AIGenerator.DTOs;
using AIGenerator.Models;

namespace AIGenerator.Interface
{
    public interface IResumeRepository
    {
        List<Resume> GetAllResumes(string UserId);

        Task<Resume> GetResumeById(int id);

        Task Create(Resume resume, string userId);

        Task Update(Resume resume);

        void Delete(int id);

    }
}
