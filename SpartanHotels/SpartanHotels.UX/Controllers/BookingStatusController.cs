using SpartanHotels.Domain.Contracts;
using SpartanHotels.Entities;
using System.Web.Mvc;

namespace SpartanHotels.UX.Controllers
{
    public class BookingStatusController : BaseController
    {
        private IStatus status;

        public BookingStatusController(IStatus injected)
        {
            status = injected;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get(BookingStatusRequest request)
        {
            return Json(status.GetBookingStatus(request));
        }
    }
}
