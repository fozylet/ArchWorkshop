using SpartanHotels.Entities;
using SpartanHotels.Worker.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SpartanHotels.Worker
{
    public class FileQueueManager : IQueueManager
    {
        public Entities.BookingRequest GetPendingBookingRequest(IQueueConfig queueConfig)
        {
            while (true)
            {
                try
                {
                    var files = Directory.GetFiles(queueConfig.PendingBookingQueuePath, "*.txt", SearchOption.TopDirectoryOnly);
                    if (files.Count() > 0)
                    {
                        try
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(BookingRequest));
                            var bookingRequest = (BookingRequest)serializer.Deserialize(File.OpenRead(Path.Combine(queueConfig.PendingBookingQueuePath, files[0])));
                            if (bookingRequest == null)
                            {
                                throw new Exception("Unable to deserialize booking request");
                            }

                            return bookingRequest;

                        }
                        catch
                        {
                            // move file to error queue
                            File.Move(Path.Combine(queueConfig.PendingBookingQueuePath, files[0]), Path.Combine(queueConfig.ErrorQueuePath, files[0]));
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    // todo logging

                }
            }

            return null;
        }


        public bool SetBookingRequestComplete(BookingResponse bookingResponse, IQueueConfig queueConfig)
        {
            try
            {
                try
                {
                    string fileName = bookingResponse.ReservationId + ".txt";

                    if (File.Exists(Path.Combine(queueConfig.PendingBookingQueuePath, fileName)))
                    {
                        string queuePathToMove = string.Empty;
                        switch (bookingResponse.BookingStatus)
                        {
                            case BookingStatus.Confirmed:
                                queuePathToMove = queueConfig.ConfirmedBookingQueuePath;
                                break;
                            case BookingStatus.Rejected:
                                queuePathToMove = queueConfig.RejectedNotificationQueuePath;
                                break;
                            case BookingStatus.Booked:
                                queuePathToMove = queueConfig.PendingBookingQueuePath;
                                break;
                            default:
                                queuePathToMove = queueConfig.ErrorQueuePath;
                                break;
                        }

                        // move file to error queue
                        File.Move(Path.Combine(queueConfig.PendingBookingQueuePath, fileName), Path.Combine(queuePathToMove, fileName));

                        return true;
                    }

                }
                catch
                {

                }

            }
            catch
            {
                // todo logging

            }

            return false;
        }
    }
}
