using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpartanHotels.Domain;
using SpartanHotels.Entities;
using System.Collections.Generic;
using System.Configuration;

namespace SpartanHotels.Test
{
    [TestClass]
    public class SpartanHotelsDomainTests
    {
        [TestMethod]
        public void TestBook()
        {
            var requestedRooms = new List<RoomRequest>();
            requestedRooms.Add(new Entities.RoomRequest()
            {
                RequestedRoom = new Room()
                {
                    Id = "1",
                    Rate = 100.00M,
                    Title = "Test Room1"
                },
                RoomCount = 2
            });

             requestedRooms.Add(new Entities.RoomRequest()
            {
                RequestedRoom = new Room()
                {
                    Id = "2",
                    Rate = 100.00M,
                    Title = "Test Room2"
                },
             RoomCount = 3});

            var guest  =  new Customer();
            guest.FirstName = "Test Customer";
            guest.LastName = "Test LastName";
            guest.EMailAddress = "aa@test.com";

            var itinerary = new Entities.Itinerary();
            itinerary.FromDate = DateTime.Now.AddDays(5);
            itinerary.ToDate = DateTime.Now.AddDays(10);
            itinerary.HotelID = "1";

            string queuePath = ConfigurationManager.AppSettings["BookingQueuePath"];

            //BookingHandler bookingHandler = new BookingHandler();
            //var  bookingResponse = bookingHandler.Book(new Entities.BookingRequest()
            //{
            //    Guest = guest,
            //    RequestedRooms =  requestedRooms,
            //    TravelDetails = itinerary
            //});

            //Assert.IsNotNull(bookingResponse);
            //Assert.IsNotNull(bookingResponse.ReservationId);
            //Assert.IsTrue(System.IO.File.Exists(queuePath + bookingResponse.ReservationId + ".txt"));
            
        }
    }
}
