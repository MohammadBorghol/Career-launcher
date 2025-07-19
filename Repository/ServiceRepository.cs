using AIGenerator.Data;
using AIGenerator.Interface;
using AIGenerator.Models;
using Microsoft.EntityFrameworkCore;

namespace AIGenerator.Repository
{
    public class ServiceRepository : IServiceRepository
    {

        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Service>> GetAllServicesAsync()
        {
            return await _context.Services.Where(s => !s.isDeleted).ToListAsync();
        }

        public async Task<Service?> GetServiceByIdAsync(short id)
        {
            return await _context.Services.SingleOrDefaultAsync(s => s.serviceId == id && !s.isDeleted);
        }

        public async Task Add(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(short id)
        {
            var service = await GetServiceByIdAsync(id);
            if (service != null)
            {
                service.isDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

    }
}
