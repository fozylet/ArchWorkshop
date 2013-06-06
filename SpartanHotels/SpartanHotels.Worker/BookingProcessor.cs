
using SpartanHotels.Entities;
using SpartanHotels.Worker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpartanHotels.Worker
{
    public class BookingProcessor : IBookingProcessor
    {
        public void ConfirmBooking(IQueueManager queueManager, INotificationManager notificationManager)
        {
            var fileQueueConfig = new FileQueueConfig();
            while (true)
            {
                var bookingRequest = queueManager.GetPendingBookingRequest(fileQueueConfig);

                if (bookingRequest != null)
                {
                    // book room


                    // Send Notification


                    // set booking request to complete
                    queueManager.SetBookingRequestComplete(new BookingResponse(), fileQueueConfig);
                }
            }
        }
    }
}
