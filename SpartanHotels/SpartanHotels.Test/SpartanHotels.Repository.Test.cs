using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpartanHotels.Entities;
using SpartanHotels.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpartanHotels.Repository.Web;

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
        }


    }
}
