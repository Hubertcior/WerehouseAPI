using Microsoft.AspNetCore.Mvc;
using WerehouseAPI.Data;
using WerehouseAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using WerehouseAPI.Entities;
using WerehouseAPI.Repositories;
using AutoMapper;

namespace WerehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageRepository _repository;
        private readonly IMapper _mapper;
        public PackagesController(IPackageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPackageDto>> GetById(int id)
        {
            var package = await _repository.GetAllAsync();

            if (package == null) return NotFound();

            var dto = _mapper.Map<GetPackageDto>(package);

            return Ok(dto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var sender = await _repository.SenderExistsAsync(dto.SenderId);
            if (!sender) return NotFound($"Sender {dto.SenderId} does not exist");

            var newPackage = _mapper.Map<Package>(dto);
            newPackage.SerialNumber = Guid.NewGuid().ToString();
            newPackage.CreatedDate = DateTime.UtcNow;
            newPackage.StatusId = 1;

            await _repository.AddAsync(newPackage);
            var packageFromDb = await _repository.GetByIdAsync(newPackage.Id);
            var responseDto = _mapper.Map<GetPackageDto>(packageFromDb);

            return CreatedAtAction(nameof(GetById), new { id = newPackage.Id }, responseDto);
        }
        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeStatus(int id, UpdatePackageStatusDto dto)
        {
            var package = await _repository.GetByIdAsync(id);
            if (package == null) return NotFound("Nie znaleziono paczki.");

            if (!await _repository.StatusExistsAsync(dto.NewStatusId))
                return BadRequest("Zły status.");

            package.StatusId = dto.NewStatusId;

            await _repository.UpdateAsync(package);

            return NoContent();
        }
    }
}
