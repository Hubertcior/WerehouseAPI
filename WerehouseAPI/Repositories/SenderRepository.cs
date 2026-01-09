using WerehouseAPI.Data;
using WerehouseAPI.Entities;

namespace WerehouseAPI.Repositories
{
    public class SenderRepository : ISenderRepository
    {
        private readonly AppDbContext _context;
        public SenderRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Sender?> GetSenderByIdAsync(int id)
        {
            return await _context.Senders.FindAsync(id);
        }
        public async Task AddAsync(Sender sender)
        {
            await _context.Senders.AddAsync(sender);
            await _context.SaveChangesAsync();
        }
    }
}
