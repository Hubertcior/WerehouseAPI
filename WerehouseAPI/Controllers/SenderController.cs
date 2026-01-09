using Microsoft.AspNetCore.Mvc;
using WerehouseAPI.Data;
using WerehouseAPI.Dtos;
using WerehouseAPI.Entities;
using WerehouseAPI.Repositories;

namespace WerehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        private readonly ISenderRepository _repository;
        public SenderController(ISenderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sender = await _repository.GetSenderByIdAsync(id);
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

            await _repository.AddAsync(newSender);
            return CreatedAtAction(nameof(GetById), new {id = newSender.Id}, newSender);
        }
    }
}
