using SpartanHotels.Entities;

namespace SpartanHotels.Repository.Contracts
{
    public interface IMasterRepository
    {
        BookingResponse AddBooking(BookingRequest request);
        CancellationResponse CancelBooking(CancellationRequest request);
        BookingStatusResponse GetBookingStatus(BookingStatusRequest request);
        BookingResponse ReadQueue(BookingRequest request);
        bool AddQueue(BookingRequest request);
        bool DeleteQueue(BookingRequest request);
    }
}
