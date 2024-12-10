using Microsoft.AspNetCore.Mvc;
using Model;
using Repository;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoRepository _repository;
        private readonly ClienteRepository _clienteRepository;
        private readonly PedidoItemRepository _itemRepository;

        public PedidoController(PedidoRepository repository,
                              ClienteRepository clienteRepository,
                              PedidoItemRepository itemRepository)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetAll()
        {
            try
            {
                var pedidos = await _repository.GetAll();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetById(long id)
        {
            try
            {
                var pedido = await _repository.GetById(id);
                if (pedido == null)
                    return NotFound("Pedido no encontrado");

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> Create(Pedido pedido)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Validación de fecha del pedido
                if (pedido.FechaPedido.Date < DateTime.Today)
                    return BadRequest("La fecha del pedido no puede ser anterior a hoy");

                // Validación de fecha de entrega
                if (pedido.FechaEntrega < pedido.FechaPedido)
                    return BadRequest("La fecha de entrega no puede ser anterior a la fecha del pedido");

                // Validación de cliente existente
                var cliente = await _clienteRepository.GetById(pedido.IdCliente);
                if (cliente == null)
                    return BadRequest("El cliente especificado no existe en el sistema");

                await _repository.Add(pedido);
                return CreatedAtAction(nameof(GetById), new { id = pedido.IdPedido }, pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Pedido pedido)
        {
            try
            {
                if (id != pedido.IdPedido)
                    return BadRequest("El ID del pedido no coincide");

                // Validación de fecha del pedido
                if (pedido.FechaPedido.Date < DateTime.Today)
                    return BadRequest("La fecha del pedido no puede ser anterior a hoy");

                // Validación de fecha de entrega
                if (pedido.FechaEntrega < pedido.FechaPedido)
                    return BadRequest("La fecha de entrega no puede ser anterior a la fecha del pedido");

                // Validación de cliente existente
                var cliente = await _clienteRepository.GetById(pedido.IdCliente);
                if (cliente == null)
                    return BadRequest("El cliente especificado no existe en el sistema");

                await _repository.Update(pedido);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var pedido = await _repository.GetById(id);
                if (pedido == null)
                    return NotFound("Pedido no encontrado");

                await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}