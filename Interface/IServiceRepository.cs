using AIGenerator.Models;

namespace AIGenerator.Interface
{
    public interface IServiceRepository
    {

        Task<List<Service>> GetAllServicesAsync();
        Task<Service?> GetServiceByIdAsync(short id);
        Task Add(Service service);
        Task Update(Service service);
        Task Delete(short id);

    }
}
