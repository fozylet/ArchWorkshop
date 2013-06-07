using System;
using System.Linq;
using SpartanHotels.Entities;

namespace SpartanHotels.Repository.Core
{
    public class DbAccess
    {
        //from, to, roomtype, city
        public AvailabilityResponse GetAvailableRoomList(AvailabilityRequest request)
        {
            var response = new AvailabilityResponse();
            try
            {
                using (var context = new SpartanHotelsEntities())
                {
                    var aviRooms = from hr in context.HotelRooms
                                   join ht in context.Hotels on hr.HotelID equals ht.HotelID
                                   where ht.City == request.City
                                   select new
                                       {
                                           hr.HotelRoomID,
                                           hr.HotelID,
                                           ht.HotelName,
                                           hr.Rate,
                                           hr.TotalRoomCount,
                                           hr.RoomTypeID,
                                           ht.City,
                                           ht.Locality,
                                           ht.Address,
                                       };

                    var aviReservations = from rs in context.Reservations
                                          where
                                              (rs.FromDate >= request.FromDate && rs.FromDate <= request.ToDate &&
                                               rs.BookStatusID == (int) BookingStatus.Confirmed)
                                          select rs;

                    if (aviReservations.Any())
                    {
                        var totalBooking =
                            aviReservations.GroupBy(tb => new {tb.FromDate, tb.HotelRoomID}).Select(room => new
                                {
                                    room.Key.HotelRoomID,
                                    room.Key.FromDate,
                                    Total = room.Count()
                                }).AsQueryable().GroupBy(a => new {a.HotelRoomID}).Select(b => new
                                    {
                                        b.Key.HotelRoomID,
                                        TotalBookedRooms = b.Max(c => c.Total)

                                    }).AsQueryable();

                        foreach (var avi in aviRooms)
                        {
                            var filteredRoom = totalBooking.FirstOrDefault(a => a.HotelRoomID == avi.HotelRoomID);
                            int roomsBooked = 0;

                            if (filteredRoom != null)
                                roomsBooked = filteredRoom.TotalBookedRooms;

                            if (avi.TotalRoomCount > roomsBooked)
                            {
                                response.Rooms.Add(new Availability()
                                    {
                                        Room = new SpartanHotels.Entities.Room
                                            {
                                                Id = avi.HotelRoomID.ToString(),
                                                Title = avi.RoomTypeID.ToString(),
                                                Rate = (decimal) avi.Rate
                                            },

                                        AvailableCount = (int) avi.TotalRoomCount - roomsBooked
                                    });
                            }
                        }
                    }
                    else
                    {
                        foreach (var avi in aviRooms)
                        {
                            if (avi.TotalRoomCount > 0)
                            {
                                response.Rooms.Add(new Availability()
                                    {

                                        Room = new Entities.Room
                                            {
                                                Id = avi.HotelRoomID.ToString(),
                                                Title = avi.RoomTypeID.ToString(),
                                                Rate = (decimal) avi.Rate
                                            },

                                        AvailableCount = (int) avi.TotalRoomCount
                                    });
                            }
                        }

                    }
                }


            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        public BookingResponse AddBooking(BookingRequest request)
        {
            var response = new BookingResponse();

            try
            {
                using (var context = new SpartanHotelsEntities())
                {
                    var eHotel = (from hr in context.HotelRooms
                                  join ht in context.Hotels on hr.HotelID equals ht.HotelID
                                  where hr.HotelRoomID.ToString() == request.TravelDetails.HotelID
                                  select new
                                      {
                                          hr.HotelID,
                                          hr.HotelRoomID,
                                          ht.HotelName,
                                          ht.Locality,
                                          ht.City
                                      }).FirstOrDefault();

                    if (eHotel != null)
                    {
                        var availabilityResponse = GetAvailableRoomList(
                            new AvailabilityRequest
                                {
                                    City = eHotel.City,
                                    FromDate = request.TravelDetails.FromDate,
                                    ToDate = request.TravelDetails.ToDate,
                                    RequestedRoomCount = request.RequestedRooms.Count()
                                });

                        var available = false;
                        foreach (var rr in request.RequestedRooms)
                        {
                            if (
                                availabilityResponse.Rooms.FirstOrDefault(
                                    ar => ar.Room.Title == rr.RequestedRoom.Title && ar.AvailableCount >= rr.RoomCount) !=
                                null)
                            {
                                available = true;
                            }
                            else
                            {
                                available = false;
                                break;
                            }
                        }

                        if (available)
                        {
                            var eCustomer = AddCustomer(request.Guest.FirstName, request.Guest.LastName,
                                                        request.Guest.EMailAddress);

                            var eReservation = new Reservation
                                {
                                    BookingNum = request.BookingId,
                                    HotelRoomID = Convert.ToInt32(request.TravelDetails.HotelID),
                                    FromDate = request.TravelDetails.FromDate,
                                    ToDate = request.TravelDetails.ToDate,
                                    ConfirmationNum = context.Reservations.Max(c => c.ConfirmationNum) + 1,
                                    CreatedBy = "",
                                    CreatedDate = DateTime.Now,
                                    ModifiedBy = "",
                                    ModifiedDate = DateTime.Now,
                                    CustomerID = eCustomer.CustomerID,
                                    BookStatusID = (int) BookingStatus.Confirmed,
                                    Remarks = ""
                                };

                            foreach (var rm in request.RequestedRooms)
                            {
                                eReservation.HotelRoomID = Convert.ToInt32(rm.RequestedRoom.Id);

                                context.Reservations.Add(eReservation);
                            }

                            context.SaveChanges();

                            response.ConfirmationNumber = eReservation.ConfirmationNum.ToString();
                            response.BookingStatus = (BookingStatus) eReservation.BookStatusID;
                            response.Guest = new Entities.Customer
                                {
                                    EMailAddress = eCustomer.Email,
                                    FirstName = eCustomer.FirstName,
                                    LastName = eCustomer.LastName
                                };
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        public CancellationResponse CancelBooking(CancellationRequest request)
        {
            var response = new CancellationResponse();
            try
            {
                using (var context = new SpartanHotelsEntities())
                {
                    var userReservations =
                        context.Reservations.Where(
                            rs =>
                            (rs.ConfirmationNum.ToString() == request.ConfirmationNumber &&
                             rs.BookStatusID == (int) BookingStatus.Confirmed));

                    if (userReservations.Any())
                    {
                        foreach (var reservation in userReservations)
                            reservation.BookStatusID = (int) BookingStatus.Cancelled;
                        context.SaveChanges();
                    }
                    else
                    {
                        //return.. invalid input, confirmation doesnt exists
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }

        public BookingStatusResponse GetBookingStatus(BookingStatusRequest request)
        {
            var response = new BookingStatusResponse();
            using (var context = new SpartanHotelsEntities())
            {
                var userReservation = context.Reservations.FirstOrDefault(rs => (rs.BookingNum == request.BookingNumber));

                if (userReservation != null)
                {
                    response.BookingNumber = userReservation.BookingNum;
                    response.ConfirmationNumber = userReservation.ConfirmationNum.ToString();
                    response.StatusCode = (BookingStatus)userReservation.BookStatusID;
                }
                else
                {
                    //return.. invalid input, reservation doesnt exists
                }
            }

            return response;
        }

        public BookingResponse ReadQueue(BookingRequest request)
        {
            var response = new BookingResponse();
            using (var context = new SpartanHotelsEntities())
            {
                var queue = context.Queues.FirstOrDefault(q => (q.ReservationID == request.BookingId));

                if (queue != null)
                {
                    response.ReservationId = queue.ReservationID;
                    response.ReservationId = queue.ReservationID;
                    response.ReservationId = queue.ReservationID;
                }
                else
                {
                    //return.. invalid input, queue doesnt exists
                }
            }

            return response;
        }

        public bool AddQueue(BookingRequest request)
        {
            using (var context = new SpartanHotelsEntities())
            {
                if (!context.Queues.Any(q => (q.ReservationID == request.BookingId)))
                {
                    var queue = new Queue {ReservationID = request.BookingId, request = new byte[0], status = false};
                    context.Queues.Add(queue);
                    context.SaveChanges();
                }
                else
                {
                    return false;
                    //return.. already exists
                }
            }

            return true;
        }

        public bool DeleteQueue(BookingRequest request)
        {
            using (var context = new SpartanHotelsEntities())
            {
                var queue = context.Queues.FirstOrDefault(q => (q.ReservationID == request.BookingId));

                if (queue != null)
                {
                    context.Queues.Remove(queue);
                    context.SaveChanges();
                }
                else
                {
                    return false;
                    //return.. invalid input, queue doesnt exists
                }
            }

            return true;
        }

        private static Customer AddCustomer(string firstName, string lastName, string emailId)
        {
            var response = new Customer();

            try
            {
                using (var context = new SpartanHotelsEntities())
                {
                    var customer = context.Customers.FirstOrDefault(c => (c.Email == emailId));

                    if (customer == null)
                    {
                        var eCustomer = new Customer
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                Email = emailId
                            };

                        context.Customers.Add(eCustomer);
                        context.SaveChanges();

                        customer = context.Customers.FirstOrDefault(c => (c.Email == emailId));
                    }

                    response.CustomerID = customer.CustomerID;
                    response.FirstName = customer.FirstName;
                    response.LastName = customer.LastName;
                    response.Email = customer.Email;
                }

            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }
    }
}

