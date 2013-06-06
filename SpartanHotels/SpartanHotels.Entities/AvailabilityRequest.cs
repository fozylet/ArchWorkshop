using Foolproof;
using System;
using System.ComponentModel.DataAnnotations;

namespace SpartanHotels.Entities
{
    public class AvailabilityRequest
    {
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Select a booking start date")]
        [LessThan("ToDate", ErrorMessage = "Stay cannot start after the end date")]
        //[LessThan("EndLimit", ErrorMessage="Bookings are not open beyond 90 days")]
        //[GreaterThan("StartLimit", ErrorMessage = "Bookings should be in the future")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Select a booking end date")]
        [GreaterThan("FromDate", ErrorMessage = "You cannot leave before you arrive!")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; }

        [Range(1, 6, ErrorMessage = "Rooms requested must be in the range of 1 to 6")]
        [Required(ErrorMessage = "Select how many rooms you need")]
        [Display(Name = "Number of Rooms")]
        public int RequestedRoomCount { get; set; }

        [Required(ErrorMessage = "Enter place of stay")]
        [Display(Name = "City")]
        public string City { get; set; }

        private DateTime StartLimit = DateTime.Now;
        private DateTime EndLimit = DateTime.Now.AddDays(30);
    }
}
