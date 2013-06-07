using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpartanHotels.Entities;
using SpartanHotels.Worker;

namespace SpartanHotels.Test
{
    [TestClass]
    public class SpartanHotelsWorkerTests
    {
        [TestMethod]
        public void TestFileQueueManagerGetQueuedRequest()
        {
            FileQueueManager queuemanager = new FileQueueManager();
            BookingRequest bookingRequest = queuemanager.GetPendingBookingRequest(new FileQueueConfig());

            Assert.IsNotNull(bookingRequest);
            Assert.IsTrue(bookingRequest.RequestedRooms.Count == 2);
        }

        [TestMethod]
        public void TestFileQueueManagerSetRequestToConfirm()
        {

        }

    }
}
