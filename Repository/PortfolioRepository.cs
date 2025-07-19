using AIGenerator.Data;
using AIGenerator.Interface;
using AIGenerator.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AIGenerator.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {

        private readonly ApplicationDbContext _context;

        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Portfolio> GetAllPortfolios(string userId)
        {
            return _context.Portfolios
                .Where(x => x.endUserId == userId && !x.isDeleted)
                .Include(x => x.portfolioServices)
                    .ThenInclude(x => x.service)
                .Include(x => x.projects)
                .AsNoTracking()
                .ToList();
        }

        public async Task<Portfolio> GetPortfolioById(int Id)
        {
            return _context.Portfolios
                .Include(x => x.portfolioServices)
                        .ThenInclude(x=>x.service)
                .Include(x => x.projects)
                        .ThenInclude(x=>x.service).
                 SingleOrDefault(x => x.portfolioId == Id);
        }

        public Portfolio Create(Portfolio portfolio)
        {
            var now = DateTime.UtcNow.ToString("yyyy-MM-dd");
            portfolio.createDate = now;
            portfolio.ModifiedDate = now;
            portfolio.isDeleted = false;

            _context.Portfolios.Add(portfolio);
            _context.SaveChanges(); // generates portfolioId
            return portfolio;
        }

        public async Task Update(Portfolio portfolio)
        {
            var now = DateTime.UtcNow.ToString("yyyy-MM-dd");
            portfolio.ModifiedDate = now;
            portfolio.isDeleted = false;

           await _context.SaveChangesAsync();

        }

        public void Delete(int Id)
        {
            var portfolio = _context.Portfolios.Find(Id);
            if (portfolio != null)
            {
                portfolio.isDeleted = true;
                _context.SaveChanges();
            }


        }
    }
}
