using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpartanHotels.Entities;
using SpartanHotels.Repository.Contracts;
using SpartanHotels.Repository.Core;

namespace SpartanHotels.Repository.Worker
{
    public class WorkerRepository : IMasterRepository
    {
        public BookingResponse AddBooking(BookingRequest request)
        {
            var dbAccess = new DbAccess();

            return dbAccess.AddBooking(request);
        }
    }   
}
