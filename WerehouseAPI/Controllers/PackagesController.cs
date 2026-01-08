using Microsoft.AspNetCore.Mvc;
using WerehouseAPI.Data;
using WerehouseAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using WerehouseAPI.Entities;

namespace WerehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackagesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PackagesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPackageDto>> GetById(int id)
        {
            var packageDto = await _context.Packages
                .Where(p => p.Id == id) 
                .Select(p => new GetPackageDto 
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
                })
                .FirstOrDefaultAsync(); 

            if (packageDto == null)
            {
                return NotFound();
            }

            return Ok(packageDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            var sender = await _context.Senders.FindAsync(dto.SenderId);
            if(sender == null)
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

            _context.Packages.Add(newPackage);
            await _context.SaveChangesAsync();
            var result = await GetById(newPackage.Id);

            if (result.Result is OkObjectResult okResult)
            {
                var createdDto = okResult.Value;

                return CreatedAtAction(nameof(GetById), new { id = newPackage.Id }, createdDto);
            }

            return StatusCode(500, "Error while creating order");
        }
    }
}
