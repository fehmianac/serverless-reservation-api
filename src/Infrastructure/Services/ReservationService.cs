using Domain.Domain;
using Domain.Entities;
using Domain.Entities.Base;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<bool> DeleteReservationAsync(string id, CancellationToken cancellationToken)
    {
         var entities = new List<IEntity>();
         var reservation = await _reservationRepository.GetReservationAsync(id, cancellationToken);
         
         if(reservation == null)
             return false;
         
         entities.Add(new ReservationEntity
         {
             Id = id
         });
         
         entities.Add(new ItemReservationMappingEntity
         {
             ItemId = reservation.ItemId,
             ReservationId = id,
         });
         
         entities.Add(new UserReservationMappingEntity()
         {
             UserId = reservation.UserId,
             ReservationId = id,
         });
         
         var dailyReservationEntity = await _reservationRepository.GetDailyReservationAsync(reservation.ItemId,
             reservation.StartDate,
             cancellationToken);
         
         if(dailyReservationEntity != null)
         {
             dailyReservationEntity.Items.Remove(id);
         }
         
         await _reservationRepository.DeleteAsync(new List<IEntity>{dailyReservationEntity},entities, cancellationToken);
         return true;
    }

    public Task<bool> CheckOverlappingReservationsAsync(string itemId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(false);
    }

    public Task<bool> CheckOverlappingReservationsAsync(string reservationId, string itemId, DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(false);
    }

    public async Task<bool> CreateReservationAsync(ReservationDto reservationDto, CancellationToken cancellationToken)
    {
        var reservationId = Guid.NewGuid().ToString();
        var entities = new List<IEntity>();
        entities.Add(new ReservationEntity
        {
            Id = reservationId,
            ItemId = reservationDto.ItemId,
            UserId = reservationDto.UserId,
            StartDate = reservationDto.StartDate,
            EndDate = reservationDto.EndDate,
            Description = reservationDto.Description,
        });

        entities.Add(new UserReservationMappingEntity
        {
            ItemId = reservationDto.ItemId,
            ReservationId = reservationId,
            UserId = reservationDto.UserId,
            Date = reservationDto.StartDate
        });
        
        entities.Add(new ItemReservationMappingEntity
        {
            ItemId = reservationDto.ItemId,
            ReservationId = reservationId,
            UserId = reservationDto.UserId,
            Date = reservationDto.StartDate
        });

        var dailyReservationEntity = await _reservationRepository.GetDailyReservationAsync(reservationDto.ItemId,
            reservationDto.StartDate,
            cancellationToken);

        if (dailyReservationEntity == null)
        {
            dailyReservationEntity = new DailyReservationEntity
            {
                Date = reservationDto.StartDate,
                ItemId = reservationDto.ItemId,
                Items = new List<string>()
            };
        }

        dailyReservationEntity.Items.Add(reservationId);
        entities.Add(dailyReservationEntity);
        return await _reservationRepository.SaveAsync(entities, cancellationToken);
    }

    public async Task<ReservationDto?> GetReservationAsync(string id, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetReservationsAsync(new List<string> { id }, cancellationToken);
        return reservation.FirstOrDefault()?.ToDto();
    }

    public async Task<List<ReservationDto>> GetReservationsByItemIdAsync(string itemId, CancellationToken cancellationToken)
    {
        var reservations = await _reservationRepository.GetItemReservationsAsync(itemId,cancellationToken);
        var entities = new List<ReservationEntity>();
        while (reservations.Any())
        {
            var top100 = reservations.Take(100).ToList();
            var reservationItems = await _reservationRepository.GetReservationsAsync(top100.Select(q=> q.ReservationId).ToList(), cancellationToken);
            entities.AddRange(reservationItems);
            reservations = reservations.Skip(100).ToList();
        }
        return entities.Select(q => q.ToDto()).ToList();
    }

    public async Task<List<ReservationDto>> GetReservationsByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var reservations = await _reservationRepository.GetUserReservationsAsync(userId,cancellationToken);
        var entities = new List<ReservationEntity>();
        while (reservations.Any())
        {
            var top100 = reservations.Take(100).ToList();
            var reservationItems = await _reservationRepository.GetReservationsAsync(top100.Select(q=> q.ReservationId).ToList(), cancellationToken);
            entities.AddRange(reservationItems);
            reservations = reservations.Skip(100).ToList();
        }
        return entities.Select(q => q.ToDto()).ToList();
    }

    public async Task<List<ReservationDto>> GetReservationsAsync(string itemId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken)
    {
        var dailyReservationRequests = new List<DailyReservationEntity>();
        while (startDate <= endDate)
        {
            dailyReservationRequests.Add(new DailyReservationEntity
            {
                ItemId = itemId,
                Date = startDate
            });
            startDate = startDate.AddDays(1);
        }

        if (!dailyReservationRequests.Any())
            return new List<ReservationDto>();

        var dailyReservations =
            await _reservationRepository.GetDailyReservationsAsync(dailyReservationRequests, cancellationToken);
        var reservationIds = dailyReservations.SelectMany(x => x.Items).ToList();
        var reservations = await _reservationRepository.GetReservationsAsync(reservationIds, cancellationToken);
        return reservations.Select(q => q.ToDto()).ToList();
    }
}