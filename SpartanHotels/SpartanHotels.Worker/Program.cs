using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace SpartanHotels.Worker
{
    public class Program
    {
        private static int PollIntervalInSeconds
        {
            get
            {
                int iPollIntervalInSeconds;
                Int32.TryParse(
                ConfigurationManager.AppSettings["PollIntervalInSeconds"], out iPollIntervalInSeconds);
                return iPollIntervalInSeconds == 0 ? 60 : iPollIntervalInSeconds;
            }

        }

        static void Main(string[] args)
        {
            int iPollIntervalInSeconds = PollIntervalInSeconds;
            while (true)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (sw.ElapsedMilliseconds < iPollIntervalInSeconds * 1000)
                {

                }
                sw.Stop();

                //BookingProcessor bookingProcessor = new BookingProcessor();

            }
        }
    }
}
