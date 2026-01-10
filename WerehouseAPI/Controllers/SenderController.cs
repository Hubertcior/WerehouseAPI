using AutoMapper;
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
        private readonly IMapper _mapper;
        public SenderController(ISenderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            var newSender = _mapper.Map<Sender>(dto);
    
            await _repository.AddAsync(newSender);

            var responseDto = _mapper.Map<GetSenderDto>(newSender);

            return CreatedAtAction(nameof(GetById), new {id = newSender.Id}, responseDto);
        }
    }
}
