using Amazon.DynamoDBv2;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.Repositories;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class ReservationRepository : DynamoRepository, IReservationRepository
{
    public ReservationRepository(IAmazonDynamoDB dynamoDb) : base(dynamoDb)
    {
    }

    protected override string GetTableName() => "reservations";


    public async Task<bool> SaveAsync(List<IEntity> entities, CancellationToken cancellationToken)
    {
        await BatchWriteAsync(entities, new List<IEntity>(), cancellationToken);
        return true;
    }

    public Task<DailyReservationEntity?> GetDailyReservationAsync(string itemId, DateTime startDate,
        CancellationToken cancellationToken)
    {
        return GetAsync<DailyReservationEntity>(DailyReservationEntity.GetPk(itemId), startDate.ToString("yyyy-MM-dd"),
            cancellationToken);
    }

    public Task<List<DailyReservationEntity>> GetDailyReservationsAsync(
        List<DailyReservationEntity> dailyReservationRequests, CancellationToken cancellationToken)
    {
        return BatchGetAsync(dailyReservationRequests, cancellationToken);
    }

    public Task<List<ReservationEntity>> GetReservationsAsync(List<string> reservationIds,
        CancellationToken cancellationToken)
    {
        return BatchGetAsync(reservationIds.Select(q => new ReservationEntity { Id = q }).ToList(),
            cancellationToken);
    }

    public async Task<bool> DeleteAsync(List<IEntity> entities, CancellationToken cancellationToken)
    {
        await BatchWriteAsync(new List<IEntity>(), entities, cancellationToken);
        return true;
    }


    public Task<List<ItemReservationMappingEntity>> GetItemReservationsAsync(string itemId,
        CancellationToken cancellationToken)
    {
        return base.GetAllAsync<ItemReservationMappingEntity>(ItemReservationMappingEntity.GetPk(itemId),
            cancellationToken);
    }

    public Task<List<UserReservationMappingEntity>> GetUserReservationsAsync(string userId,
        CancellationToken cancellationToken)
    {
        return base.GetAllAsync<UserReservationMappingEntity>(UserReservationMappingEntity.GetPk(userId),
            cancellationToken);
    }

    public Task<ReservationEntity?> GetReservationAsync(string id, CancellationToken cancellationToken)
    {
        return GetAsync<ReservationEntity>(ReservationEntity.GetPk(), id, cancellationToken);
    }
}