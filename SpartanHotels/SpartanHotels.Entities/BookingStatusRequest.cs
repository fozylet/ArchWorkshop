using Foolproof;
using System;
using System.ComponentModel.DataAnnotations;

namespace SpartanHotels.Entities
{
    public class BookingStatusRequest
    {
        [RequiredIf("BookingNumber", "", ErrorMessage="Either Booking# or Confirmation# is required")]
        [Display(Name="Confirmation #")]
        public string ConfirmationNumber { get; set; }

        [RequiredIf("ConfirmationNumber", "", ErrorMessage = "Either Booking# or Confirmation# is required")]
        [Display(Name = "Booking #")]
        public string BookingNumber { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
