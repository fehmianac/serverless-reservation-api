using Domain.Entities;
using Domain.Entities.Base;

namespace Domain.Repositories;

public interface IReservationRepository
{
    Task<bool> SaveAsync(List<IEntity> entities, CancellationToken cancellationToken);
    Task<DailyReservationEntity?> GetDailyReservationAsync(string itemId, DateTime startDate, CancellationToken cancellationToken);
    Task<List<DailyReservationEntity>> GetDailyReservationsAsync(List<DailyReservationEntity> dailyReservationRequests, CancellationToken cancellationToken);
    Task<List<ReservationEntity>> GetReservationsAsync(List<string> reservationIds, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(List<IEntity> entities, CancellationToken cancellationToken);
    Task<List<ItemReservationMappingEntity>> GetItemReservationsAsync(string itemId, CancellationToken cancellationToken);
    Task<List<UserReservationMappingEntity>> GetUserReservationsAsync(string userId, CancellationToken cancellationToken);
    Task<ReservationEntity?> GetReservationAsync(string id, CancellationToken cancellationToken);
}