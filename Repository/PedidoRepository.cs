using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class PedidoRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> GetAll()
        {
            return await _context.Pedidos.ToListAsync();
        }

        public async Task<Pedido> GetById(long id)
        {
            return await _context.Pedidos.FindAsync(id);
        }

        public async Task<Pedido> Add(Pedido pedido)
        {
            // Validacion fecha
            if (pedido.FechaPedido < DateTime.Today)
                throw new InvalidOperationException("La fecha del pedido no puede ser anterior a hoy");

            if (pedido.FechaEntrega < pedido.FechaPedido)
                throw new InvalidOperationException("La fecha de entrega no puede ser anterior a la fecha del pedido");

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        public async Task Update(Pedido pedido)
        {
            // Validaciones similares al agregar pedido
            if (pedido.FechaPedido < DateTime.Today)
                throw new InvalidOperationException("La fecha del pedido no puede ser anterior a hoy");

            if (pedido.FechaEntrega < pedido.FechaPedido)
                throw new InvalidOperationException("La fecha de entrega no puede ser anterior a la fecha del pedido");

            _context.Entry(pedido).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var pedido = await GetById(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }
        }
    }
}