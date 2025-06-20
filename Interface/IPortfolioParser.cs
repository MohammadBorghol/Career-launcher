using AIGenerator.Models;

namespace AIGenerator.Interface
{
    public interface IPortfolioParser
    {


        Task<Portfolio> ParsePortfolioAsync(string rawText);


    }
}
