using SpartanHotels.Entities;

namespace SpartanHotels.Domain.Contracts
{
    public interface IBooking
    {
        BookingResponse Book(BookingRequest request);
        BookingResponse Confirm(BookingRequest request);
    }
}
