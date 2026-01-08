using Microsoft.AspNetCore.Mvc;
using WerehouseAPI.Data;
using WerehouseAPI.Dtos;
using WerehouseAPI.Entities;

namespace WerehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SenderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sender = await _context.Senders.FindAsync(id);
            if(sender == null)
            {
                return NotFound();
            }
            return Ok(sender);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSenderDto dto)
        {
            var newSender = new Sender
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                City = dto.City,
                Street = dto.Street,
                PostalCode = dto.PostalCode,
            };

            _context.Senders.Add(newSender);

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id = newSender.Id}, newSender);
        }
    }
}
