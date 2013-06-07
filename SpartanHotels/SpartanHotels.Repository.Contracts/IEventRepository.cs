using SpartanHotels.Entities;
namespace SpartanHotels.Repository.Contracts
{
    public interface IEventRepository
    {
        BookingResponse Push(BookingRequest request);
    }
}
