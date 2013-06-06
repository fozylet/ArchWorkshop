using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartanHotels.Entities
{
    public class RoomRequest
    {
        public Room RequestedRoom { get; set; }
        public int RoomCount { get; set; }
    }
}
