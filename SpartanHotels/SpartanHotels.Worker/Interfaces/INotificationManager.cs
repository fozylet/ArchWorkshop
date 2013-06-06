using SpartanHotels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpartanHotels.Worker.Interfaces
{
    public interface INotificationManager
    {
        bool SendNotification(BookingResponse bookingResponse);
    }
}
