using SpartanHotels.Entities;
using SpartanHotels.Repository.Contracts;
using SpartanHotels.Repository.Core;

namespace SpartanHotels.Repository.Web
{
    public class WebRepository : ISnapshotRepository
    {
        public AvailabilityResponse GetAvailableRoomList(AvailabilityRequest request)
        {
            var dbAccess = new DbAccess();

            return dbAccess.GetAvailableRoomList(request);
        }
    }   
}
