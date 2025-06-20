using AIGenerator.Data;
using AIGenerator.Interface;
using AIGenerator.Models;

namespace AIGenerator.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Project> GetAllProjects(int PortfolioId)
        {
            return _context.Projects.Where(e=>e.portfolioId==PortfolioId).ToList();
        }

        public async Task<Project> GetProjectById(int Id)
        {
            return _context.Projects.SingleOrDefault(e => e.projectId == Id);
        }

        public void Create(Project project)
        {


            _context.Projects.Add(project);
            _context.SaveChanges();

        }

        public void Delete(int Id)
        {
            var Project = _context.Projects.Find(Id);
            if (Project != null)
            {
                Project.isDeleted = true;
                _context.SaveChanges();
            }
        }

       
        public async Task Update(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
        }

       
    }
}
