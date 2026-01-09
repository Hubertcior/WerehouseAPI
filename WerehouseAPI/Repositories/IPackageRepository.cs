using WerehouseAPI.Entities;

namespace WerehouseAPI.Repositories
{
    public interface IPackageRepository
    {
        Task<IEnumerable<Package>> GetAllAsync();
        Task<Package?> GetByIdAsync(int id);
        Task AddAsync(Package package);
        Task UpdateAsync(Package package); 
        Task<bool> StatusExistsAsync(int statusId);
        Task<bool> SenderExistsAsync(int senderId);
    }
}
