using AIGenerator.DTOs;
using AIGenerator.Interface;
using AIGenerator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace AIGenerator.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.services = await _serviceRepository.GetAllServicesAsync();
            return View();
        }

        [HttpGet("Create")]
        public IActionResult Create() => View();

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ServiceCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var service = new Service
            {
                serviceName = dto.ServiceName,
                ServiceDescription = dto.ServiceDescription
            };

            if (dto.ServiceImage != null)
            {
                using var ms = new MemoryStream();
                await dto.ServiceImage.CopyToAsync(ms);
                service.serviceImageFile = ms.ToArray();
                service.serviceImageName = dto.ServiceImage.FileName;
                service.serviceImageContentType = dto.ServiceImage.ContentType;
            }

            await _serviceRepository.Add(service);
            return RedirectToAction("Index");
        }

        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(short serviceId)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(serviceId);
            if (service == null) return NotFound();

            var dto = new ServiceUpdateDTO
            {
                ServiceId = service.serviceId,
                ServiceName = service.serviceName,
                ServiceDescription = service.ServiceDescription
            };
            return View(dto);
        }

        [HttpPost("Edit")]
        public async Task<IActionResult> Edit(short serviceId, ServiceUpdateDTO dto)
        {
            if (serviceId != dto.ServiceId) return BadRequest();

            if (!ModelState.IsValid)
                return View(dto);

            var service = await _serviceRepository.GetServiceByIdAsync(serviceId);
            if (service == null) return NotFound();

            service.serviceName = dto.ServiceName;
            service.ServiceDescription = dto.ServiceDescription;

            if (dto.ServiceImage != null)
            {
                using var ms = new MemoryStream();
                await dto.ServiceImage.CopyToAsync(ms);
                service.serviceImageFile = ms.ToArray();
                service.serviceImageName = dto.ServiceImage.FileName;
                service.serviceImageContentType = dto.ServiceImage.ContentType;
            }

            await _serviceRepository.Update(service);
            return RedirectToAction("Index");
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            await _serviceRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
