using SpartanHotels.Domain.Contracts;
using SpartanHotels.Entities;
using System;
using System.Web.Mvc;

namespace SpartanHotels.UX.Controllers
{
    public class AvailabilityController : BaseController
    {
        private IAvailability availability;

        public AvailabilityController(IAvailability injected)
        {
            availability = injected;
        }

        public ActionResult Index()
        {
            var init = new AvailabilityRequest
            {
                FromDate = DateTime.Now.AddDays(2),
                ToDate = DateTime.Now.AddDays(5),
                RequestedRoomCount = 1,
                City = "Bangalore"
            };

            return View(init);
        }

        [HttpPost]
        public JsonResult Get(AvailabilityRequest itinerary)
        {
            int roomCount = itinerary.RequestedRoomCount;
            itinerary.RequestedRoomCount = 1;
            var response = availability.Get(itinerary);
            var booking = new BookingRequest
            {
                Guest = new Customer { FirstName = "Hello" },
                TravelDetails = new Itinerary { FromDate = itinerary.FromDate, ToDate = itinerary.ToDate, HotelID = itinerary.City }
            };

            var bundle = new object[] { response, booking };
            return Json(bundle);
        }

    }
}
