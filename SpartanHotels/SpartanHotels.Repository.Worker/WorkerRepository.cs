using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpartanHotels.Entities;
using SpartanHotels.Repository.Contracts;
using SpartanHotels.Repository.Core;
using System.Xml.Serialization;
using System.IO;

namespace SpartanHotels.Repository.Worker
{
    public class WorkerRepository : IMasterRepository, IEventRepository
    {
        public BookingResponse AddBooking(BookingRequest request)
        {
            var dbAccess = new DbAccess();

            return dbAccess.AddBooking(request);
        }

        public BookingResponse Push(BookingRequest request)
        {
            string strReservationId = Guid.NewGuid().ToString();
            request.BookingId = strReservationId;
            DbAccess db = new DbAccess();
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BookingRequest));
                serializer.Serialize(stream, request);
                //write to db
            }

            return new BookingResponse()
            {
                BookingStatus = BookingStatus.Booked,
                ReservationId = strReservationId,
                Guest = request.Guest
            };
        }

        private void PushToFile(BookingRequest request)
        {/*
            using (FileStream stream = new FileStream(Path.Combine(ConfigurationManager.AppSettings["BookingQueuePath"], strReservationId + ".txt"), FileMode.CreateNew))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BookingRequest));
                serializer.Serialize(stream, request);
            }*/
        }
    }   
}
