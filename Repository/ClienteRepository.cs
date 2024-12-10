using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class ClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetById(long id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente> Add(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task Update(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var cliente = await GetById(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
    }
}