using SpartanHotels.Entities;

namespace SpartanHotels.Domain.Contracts
{
    public interface IStatus
    {
        BookingStatusResponse GetBookingStatus(BookingStatusRequest request);
    }
}
