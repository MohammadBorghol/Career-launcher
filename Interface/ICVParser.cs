using AIGenerator.DTOs;
using AIGenerator.Models;

namespace AIGenerator.Interface
{
    public interface ICVParser
    {

        Task<Resume> ParseCvAsync(string rawText);

        //public void MapToExistingResume(ResumeUpdateDTO dto, Resume existingResume,string currentUserId);

        //ResumeUpdateDTO MapToResumeUpdateDTO(Resume resume);

    }
}
