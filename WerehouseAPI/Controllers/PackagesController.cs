using Microsoft.AspNetCore.Mvc;
using WerehouseAPI.Data;
using WerehouseAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using WerehouseAPI.Entities;
using WerehouseAPI.Repositories;

namespace WerehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly IPackageRepository _repository;
        public PackagesController(IPackageRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPackageDto>> GetById(int id)
        {
            var packages = await _repository.GetAllAsync();

            var dtos = packages.Select(p => new GetPackageDto
                {
                    Serial = p.SerialNumber,
                    Height = p.Height,
                    Id = p.Id,
                    Weight = p.Weight,
                    Price = p.Price,
                    Status = p.Status.Name,

                    Sender = new GetSenderDto
                    {
                        Id = p.Sender.Id,
                        Name = p.Sender.Name,
                        Surname = p.Sender.Surname,
                        City = p.Sender.City
                    },
                    Receiver = new GetSenderDto
                    {
                        Id = p.Receiver.Id,
                        Name = p.Receiver.Name,
                        Surname = p.Receiver.Surname,
                        City = p.Receiver.City
                    }
                });
            return Ok(dtos);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var sender = await _repository.SenderExistsAsync(dto.SenderId);
            if (!sender)
            {
                return NotFound($"Sender {dto.SenderId} does not exist");
            }

            var newPackage = new Package
            {
                SerialNumber = Guid.NewGuid().ToString(),
                Weight = dto.Weight,
                Height = dto.Height,
                Price = dto.Price,
                CreatedDate = DateTime.UtcNow,
                StatusId = 1,

                SenderId = dto.SenderId,

                Receiver = new Receiver
                {
                    Name = dto.Receiver.Name,
                    Surname = dto.Receiver.Surname,
                    Email = dto.Receiver.Email,
                    PhoneNumber = dto.Receiver.PhoneNumber,
                    City = dto.Receiver.City,
                    Street = dto.Receiver.Street,
                    PostalCode = dto.Receiver.PostalCode
                }
            };

            await _repository.AddAsync(newPackage); 
            var result = await GetById(newPackage.Id);

            if (result.Result is OkObjectResult okResult)
            {
                var createdDto = okResult.Value;

                return CreatedAtAction(nameof(GetById), new { id = newPackage.Id }, createdDto);
            }

            return StatusCode(500, "Error while creating order");
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
