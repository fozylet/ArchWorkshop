﻿using SpartanHotels.Domain.Contracts;
using SpartanHotels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpartanHotels.UX.Controllers
{
    public class BookingController : BaseController
    {
        private IBooking bookie;

        public BookingController(IBooking injected)
        {
            bookie = injected;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Confirm(BookingRequest request)
        {
            return Json(bookie.Book(request));
        }
    }
}
