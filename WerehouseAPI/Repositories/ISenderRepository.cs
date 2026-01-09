using WerehouseAPI.Entities;

namespace WerehouseAPI.Repositories
{
    public interface ISenderRepository
    {
        Task<Sender?> GetSenderByIdAsync(int id);
        Task AddAsync(Sender sender);
    }
}
