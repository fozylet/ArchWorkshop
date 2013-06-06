using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartanHotels.Worker.Interfaces
{
    public interface IQueueConfig
    {
        string PendingBookingQueuePath
        {
            get;

        }

        string ConfirmedBookingQueuePath
        {
            get;
        }

        string PendingNotificationQueuePath
        {
            get;
        }

        string RejectedNotificationQueuePath
        {
            get;
        }


        string ErrorQueuePath
        {
            get;
        }
    }
}
