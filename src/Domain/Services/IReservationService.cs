using Domain.Domain;

namespace Domain.Services;

public interface IReservationService
{
    Task<bool> DeleteReservationAsync(string id, CancellationToken cancellationToken);

    Task<bool> CheckOverlappingReservationsAsync(string itemId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken);

    
    Task<bool> CheckOverlappingReservationsAsync(string reservationId,string itemId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken);

    Task<bool> CreateReservationAsync(ReservationDto reservationDto, CancellationToken cancellationToken);

    Task<ReservationDto?> GetReservationAsync(string id, CancellationToken cancellationToken);

    Task<List<ReservationDto>> GetReservationsByItemIdAsync(string itemId, CancellationToken cancellationToken);

    Task<List<ReservationDto>> GetReservationsByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<List<ReservationDto>> GetReservationsAsync(string itemId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
}