using SpartanHotels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpartanHotels.Worker.Interfaces
{
    public interface IQueueManager
    {
        BookingRequest GetPendingBookingRequest(IQueueConfig queueConfig);
        bool SetBookingRequestComplete(BookingResponse bookingResponse, IQueueConfig queueConfig);
    }
}
