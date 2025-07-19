using AIGenerator.Data;
using AIGenerator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AIGenerator.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<Person> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<Person> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("counts")]
        public async Task<IActionResult> GetCounts()
        {
            var resumeCount = await _context.Resumes.CountAsync();
            var portfolioCount = await _context.Portfolios.CountAsync();
            var userCount = await _userManager.Users.CountAsync(); 

            return Ok(new
            {
                resumeCount,
                portfolioCount,
                userCount
            });
        }


        [HttpGet("counts-per-day")]
        public async Task<IActionResult> GetCountsPerDay()
        {
            var today = DateTime.UtcNow.Date;

            var daysRange = Enumerable.Range(0, 7)
                .Select(offset => today.AddDays(-offset))
                .OrderBy(d => d)
                .ToList();

            // Fetch and parse Resumes
            var resumesPerDay = (await _context.Resumes.ToListAsync())
                .Where(r => DateTime.TryParse(r.createDate, out var date) && date >= today.AddDays(-6))
                .GroupBy(r => DateTime.Parse(r.createDate).Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToList();

            // Fetch and parse Portfolios
            var portfoliosPerDay = (await _context.Portfolios.ToListAsync())
                .Where(p => DateTime.TryParse(p.createDate, out var date) && date >= today.AddDays(-6))
                .GroupBy(p => DateTime.Parse(p.createDate).Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToList();

            // Build response with zeros for missing days
            var resumeCounts = daysRange.Select(day =>
                new {
                    Date = day.ToString("yyyy-MM-dd"),
                    Count = resumesPerDay.FirstOrDefault(r => r.Date == day)?.Count ?? 0
                }).ToList();

            var portfolioCounts = daysRange.Select(day =>
                new {
                    Date = day.ToString("yyyy-MM-dd"),
                    Count = portfoliosPerDay.FirstOrDefault(p => p.Date == day)?.Count ?? 0
                }).ToList();

            return Ok(new
            {
                Dates = daysRange.Select(d => d.ToString("yyyy-MM-dd")),
                ResumeCounts = resumeCounts.Select(r => r.Count),
                PortfolioCounts = portfolioCounts.Select(p => p.Count)
            });
        }


    }
}
