using SpartanHotels.Worker.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartanHotels.Worker
{
    public class FileQueueConfig : IQueueConfig
    {
        public string PendingBookingQueuePath
        {
            get
            {
                return ConfigurationManager.AppSettings["PendingBookingQueuePath"];
            }
        }

        public string ConfirmedBookingQueuePath
        {
            get
            {
                return ConfigurationManager.AppSettings["ConfirmedBookingQueuePath"];
            }
        }

        public string PendingNotificationQueuePath
        {
            get
            {
                return ConfigurationManager.AppSettings["PendingNotificationQueuePath"];
            }
        }

        public string RejectedNotificationQueuePath
        {
            get
            {
                return ConfigurationManager.AppSettings["RejectedNotificationQueuePath"];
            }
        }


        public string ErrorQueuePath
        {
            get
            {
                return ConfigurationManager.AppSettings["ErrorQueuePath"];
            }
        }
    }
}
