using SpartanHotels.Domain.Contracts;
using SpartanHotels.Entities;
using SpartanHotels.Repository.Contracts;
using System;

namespace SpartanHotels.Domain
{
    public class SearchHandler : IAvailability, IStatus
    {
        private ISnapshotRepository reader;

        public SearchHandler(ISnapshotRepository injected)
        {
            reader = injected;
        }

        public AvailabilityResponse Get(AvailabilityRequest request)
        {
            return reader.GetAvailableRoomList(request);
        }

        private AvailabilityResponse Dummy()
        {
            Random rnd = new Random();
            var response = new AvailabilityResponse();
            response.Rooms.Add(new Availability { Room = new Room { Description = "Minimalist room for the economist traveller.", Title = "Sleep Dorm", Id = "1", Rate = 1597 }, AvailableCount = rnd.Next(1, 10) });
            response.Rooms.Add(new Availability { Room = new Room { Description = "Just the basics - bed and bathroom with shower.", Title = "RnR", Id = "2", Rate = 899 }, AvailableCount = rnd.Next(1, 10) });
            response.Rooms.Add(new Availability { Room = new Room { Description = "Bedroom with workspace for the business traveller.", Title = "Working Suite", Id = "3", Rate = 2499 }, AvailableCount = rnd.Next(1, 10) });
            response.Rooms.Add(new Availability { Room = new Room { Description = "Private office space with minimalist sleeping quarters.", Title = "Office Away", Id = "4", Rate = 1200 }, AvailableCount = rnd.Next(1, 10) });
            response.Rooms.Add(new Availability { Room = new Room { Description = "Sleeping, living and dining accomodations.", Title = "Home Away", Id = "5", Rate = 4000 }, AvailableCount = rnd.Next(1, 10) });
            return response;
        }

        public BookingStatusResponse GetBookingStatus(BookingStatusRequest request)
        {
            return new BookingStatusResponse
            {
                ConfirmationNumber = request.ConfirmationNumber,
                BookingNumber = request.BookingNumber,
                StatusCode = BookingStatus.Confirmed
            };
        }
    }
}
