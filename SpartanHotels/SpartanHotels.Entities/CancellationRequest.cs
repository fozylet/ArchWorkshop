using Foolproof;
using System;
using System.ComponentModel.DataAnnotations;

namespace SpartanHotels.Entities
{
    public class CancellationRequest
    {
        [RequiredIfEmpty("BookingNumber")]
        [Display(Name = "Confirmation #")]
        public string ConfirmationNumber { get; set; }

        [RequiredIfEmpty("ConfirmationNumber")]
        [Display(Name = "Booking #")]
        public string BookingNumber { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
