using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpartanHotels.Entities;
using System;
using System.Collections.Generic;
using SpartanHotels.Repository.Web;
using SpartanHotels.Repository.Worker;

namespace SpartanHotels.Test
{
    [TestClass]
    public class SpartanHotelsRepositoryTests
    {
        [TestMethod]
        public void TestRoomAvailability()
        {
            var request = new AvailabilityRequest { FromDate = DateTime.Now.Date.AddDays(1), ToDate = DateTime.Now.Date.AddDays(1), RequestedRoomCount = 1, City = "Bangalore" };
            var response = new WebRepository().GetAvailableRoomList(request);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Rooms.Count > 0);
        }

        [TestMethod]
        public void TestBookingRequest()
        {
            var request = new BookingRequest
            {
                BookingId = "",
                Guest = new Customer { EMailAddress = "", FirstName = "", LastName = "" },
                RequestedRooms = new List<RoomRequest>
                        {
                            new RoomRequest
                                {
                                    RequestedRoom = new Room {Id = "", Description = "", Title = ""},
                                    RoomCount = 2
                                }
                        },
                TravelDetails = new Itinerary { FromDate = DateTime.Now.Date.AddDays(1), ToDate = DateTime.Now.Date.AddDays(2), HotelID = "" }
            };

            var response = new WorkerRepository().AddBooking(request);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ConfirmationNumber);
        }

        [TestMethod]
        public void TestCancellationRequest()
        {
            var request = new CancellationRequest { BookingNumber = "", ConfirmationNumber = "", LastName = "" };
            var response = new WorkerRepository().CancelBooking(request);

            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void TestReservationStatus()
        {
            var request = new BookingStatusRequest { BookingNumber = "", LastName = "" };
            var response = new WorkerRepository().GetBookingStatus(request);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.StatusCode);
        }
    }
}
