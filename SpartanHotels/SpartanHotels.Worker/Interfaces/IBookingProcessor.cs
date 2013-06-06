using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpartanHotels.Worker.Interfaces
{
    public interface IBookingProcessor
    {
        void ConfirmBooking(IQueueManager queueManager, INotificationManager notificationManager);
    }
}
