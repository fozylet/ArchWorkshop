using System.Collections.Generic;

namespace SpartanHotels.Entities
{
    public class AvailabilityResponse
    {
        public AvailabilityResponse()
        {
            Rooms = new List<Availability>();
        }

        public IList<Availability> Rooms { get; private set; }
    }
}
