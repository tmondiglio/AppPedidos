using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepository _repository;

        public ClienteController(ClienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
        {
            try
            {
                var clientes = await _repository.GetAll();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetById(long id)
        {
            try
            {
                var cliente = await _repository.GetById(id);
                if (cliente == null)
                    return NotFound();
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Create(Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _repository.Add(cliente);
                return CreatedAtAction(nameof(GetById), new { id = cliente.IdCliente }, cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Cliente cliente)
        {
            try
            {
                if (id != cliente.IdCliente)
                    return BadRequest();

                await _repository.Update(cliente);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message} - {ex.InnerException?.Message}");
            }
        }
    }
}