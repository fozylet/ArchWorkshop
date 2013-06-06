namespace SpartanHotels.Entities
{
    public class CancellationResponse
    {
        public string ConfirmationNumber { get; set; }

        public string BookingNumber { get; set; }

        public BookingStatus StatusCode { get; set; }

        public string StatusValue { get { return StatusCode.ToString(); } }
    }
}
