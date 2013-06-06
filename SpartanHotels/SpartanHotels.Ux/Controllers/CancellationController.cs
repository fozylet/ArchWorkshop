using SpartanHotels.Domain.Contracts;
using SpartanHotels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpartanHotels.UX.Controllers
{
    public class CancellationController : BaseController
    {
        private ICancellation canceller;

        public CancellationController(ICancellation injected)
        {
            canceller = injected;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Cancel(CancellationRequest request)
        {
            return Json(canceller.Cancel(request));
        }
    }
}
