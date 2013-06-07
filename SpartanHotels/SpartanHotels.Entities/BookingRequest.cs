using System.Collections.Generic;

namespace SpartanHotels.Entities
{
    public class BookingRequest
    {
        public string BookingId { get; set; }
        public Customer Guest { get; set; }
        public Itinerary TravelDetails { get; set; }
        public List<RoomRequest> RequestedRooms { get; set; }
    }
}
