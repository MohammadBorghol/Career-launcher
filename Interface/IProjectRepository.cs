using AIGenerator.Models;

namespace AIGenerator.Interface
{
    public interface IProjectRepository
    {


        List<Project> GetAllProjects(int portfolioId);

        Task<Project> GetProjectById(int Id);

        void Create(Project project);

        Task Update(Project project);

        void Delete(int Id);

    }
}
