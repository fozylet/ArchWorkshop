using SpartanHotels.Entities;

namespace SpartanHotels.Repository.Contracts
{
    public interface ISnapshotRepository
    {
        AvailabilityResponse GetAvailableRoomList(AvailabilityRequest request);
    }
}
