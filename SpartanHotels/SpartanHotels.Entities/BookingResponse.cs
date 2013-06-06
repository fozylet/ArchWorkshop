namespace SpartanHotels.Entities
{
    public class BookingResponse
    {
        public string ReservationId { get; set; }
        public string ConfirmationNumber { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public Customer Guest { get; set; }
    }
}
