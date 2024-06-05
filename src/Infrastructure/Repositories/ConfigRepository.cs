using Amazon.DynamoDBv2;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class ConfigRepository : DynamoRepository, IConfigRepository
{
    public ConfigRepository(IAmazonDynamoDB dynamoDb) : base(dynamoDb)
    {
    }

    protected override string GetTableName() => "reservations";

    public Task<ItemConfigEntity?> GetAsync(string itemId, CancellationToken cancellationToken = default)
    {
        return GetAsync<ItemConfigEntity>(ItemConfigEntity.GetPk(), itemId, cancellationToken);
    }

    public Task<List<ItemConfigEntity>> GetAllAsync(List<string> itemIds, CancellationToken cancellationToken = default)
    {
        return BatchGetAsync(itemIds.Select(q => new ItemConfigEntity { ItemId = q }).ToList(), cancellationToken);
    }

    public Task<bool> SaveAsync(ItemConfigEntity entity, CancellationToken cancellationToken = default)
    {
        return base.SaveAsync(entity, cancellationToken);
    }

    public Task<bool> DeleteAsync(string itemId, CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(ItemConfigEntity.GetPk(), itemId, cancellationToken);
    }
}