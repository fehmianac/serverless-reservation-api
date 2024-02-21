using Domain.Entities;

namespace Domain.Repositories;

public interface IConfigRepository
{
    Task<ItemConfigEntity?> GetAsync(string itemId, CancellationToken cancellationToken = default);
    Task<List<ItemConfigEntity>> GetAllAsync(List<string> itemIds, CancellationToken cancellationToken = default);
    Task<bool> SaveAsync(ItemConfigEntity entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string itemId, CancellationToken cancellationToken = default);
}