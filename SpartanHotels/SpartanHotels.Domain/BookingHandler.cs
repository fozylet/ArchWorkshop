using SpartanHotels.Domain.Contracts;
using SpartanHotels.Entities;
using SpartanHotels.Repository.Contracts;
using System;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace SpartanHotels.Domain
{
    public class BookingHandler : IBooking, ICancellation
    {
        private IMasterRepository writer;

        public BookingHandler(IMasterRepository injected)
        {
            writer = injected;
        }

        public CancellationResponse Cancel(CancellationRequest request)
        {
            return new CancellationResponse {
                ConfirmationNumber = request.ConfirmationNumber,
                BookingNumber = request.BookingNumber,
                StatusCode = BookingStatus.Cancelled
            };
        }

        public BookingResponse Book(BookingRequest request)
        {
            string strReservationId = Guid.NewGuid().ToString();
            using (FileStream stream = new FileStream(Path.Combine(ConfigurationManager.AppSettings["BookingQueuePath"], strReservationId+".txt"), FileMode.CreateNew))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(BookingRequest));
                serializer.Serialize(stream, request);

            }
            return new BookingResponse()
                {
                    BookingStatus = BookingStatus.Booked,
                    ReservationId = strReservationId,
                    Guest = request.Guest
                };
        }

        public BookingResponse Confirm(BookingRequest request)
        {
            //return writer.AddBooking(request);
            // Call DB Access and Confirm
            return new BookingResponse
            {
                BookingStatus = BookingStatus.Confirmed,
                ConfirmationNumber = "1234",
                Guest = request.Guest,
                ReservationId = "3456"
            };
        }
    }
}

