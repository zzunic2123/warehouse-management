using WebApplication1.Models.DomainModels;
using WebApplication1.Models.DTO;

namespace WebApplication1.Mappers;

public static class ReservationMapper
{
    public static ReservationDto ToDto(this Reservation reservation)
    {
        ReservationDto dto = new ReservationDto();
        
        dto.Id = reservation.Id;
        dto.ReservationDate = reservation.ReservationDate;
        

        return dto;
    }
    
    public static Reservation ToDomain(this ReservationDto dto)
    {
        Reservation reservation = new Reservation();
        
        reservation.Id = dto.Id;
        reservation.ReservationDate = dto.ReservationDate;
        
        return reservation;
    }
}