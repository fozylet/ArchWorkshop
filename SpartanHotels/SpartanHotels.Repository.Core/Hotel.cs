//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpartanHotels.Repository.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hotel
    {
        public Hotel()
        {
            this.HotelRooms = new HashSet<HotelRoom>();
        }
    
        public int HotelID { get; set; }
        public string HotelName { get; set; }
        public string City { get; set; }
        public string Locality { get; set; }
        public string Address { get; set; }
    
        public virtual ICollection<HotelRoom> HotelRooms { get; set; }
    }
}
