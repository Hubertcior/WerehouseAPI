using Microsoft.EntityFrameworkCore;
using WerehouseAPI.Data;
using WerehouseAPI.Entities;

namespace WerehouseAPI.Repositories
{
    public class PackageRepository : IPackageRepository
    {

        private readonly AppDbContext _context;
        public PackageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Package>> GetAllAsync()
        {
            return await _context.Packages
                .Include(p => p.Sender)
                .Include(p => p.Receiver)
                .Include(p => p.Status)
                .ToListAsync();
        }

        public async Task<Package?> GetByIdAsync(int id)
        {
            return await _context.Packages
                .Include(p => p.Sender)
                .Include(p => p.Receiver)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Package package)
        {
            await _context.Packages.AddAsync(package);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Package package)
        {
            _context.Packages.Update(package);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> StatusExistsAsync(int statusId)
        {
            return await _context.PackageStatuses.AnyAsync(s => s.Id == statusId);
        }
        public async Task<bool> SenderExistsAsync(int senderId)
        {
            return await _context.Senders.AllAsync(s => s.Id == senderId);
        }

    }
}
