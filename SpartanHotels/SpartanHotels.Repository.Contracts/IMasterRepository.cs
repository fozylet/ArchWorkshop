using SpartanHotels.Entities;

namespace SpartanHotels.Repository.Contracts
{
    public interface IMasterRepository
    {
        BookingResponse AddBooking(BookingRequest request);
    }
}
