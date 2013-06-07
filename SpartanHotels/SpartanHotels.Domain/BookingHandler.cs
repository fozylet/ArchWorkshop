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
        private IEventRepository queue;
        
        public BookingHandler(IMasterRepository w, IEventRepository q)
        {
            writer = w;
            queue = q;
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
            return queue.Push(request);
        }

        public BookingResponse Confirm(BookingRequest request)
        {
            return writer.AddBooking(request);
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

