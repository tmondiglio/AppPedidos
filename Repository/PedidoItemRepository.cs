using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class PedidoItemRepository
    {
        private readonly ApplicationDbContext _context;

        public PedidoItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PedidoItem>> GetAllByPedido(long pedidoId)
        {
            return await _context.PedidoItems
                .Where(i => i.IdPedido == pedidoId)
                .ToListAsync();
        }

        public async Task<PedidoItem> GetById(long pedidoId, long itemId)
        {
            return await _context.PedidoItems
                .FirstOrDefaultAsync(i => i.IdPedido == pedidoId && i.IdItem == itemId);
        }

        public async Task<PedidoItem> Add(PedidoItem item)
        {
            if (item.Cantidad <= 0)
                throw new InvalidOperationException("La cantidad debe ser mayor a cero");

            _context.PedidoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task Update(PedidoItem item)
        {
            if (item.Cantidad <= 0)
                throw new InvalidOperationException("La cantidad debe ser mayor a cero");

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long pedidoId, long itemId)
        {
            var item = await GetById(pedidoId, itemId);
            if (item != null)
            {
                _context.PedidoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}