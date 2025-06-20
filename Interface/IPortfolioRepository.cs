using AIGenerator.Models;

namespace AIGenerator.Interface
{
    public interface IPortfolioRepository
    {

         List<Portfolio> GetAllPortfolios(string UserId);

         Task<Portfolio> GetPortfolioById(int Id);

         Portfolio Create(Portfolio portfolio);

         Task Update(Portfolio portfolio);

         void Delete(int Id);




        }
}
