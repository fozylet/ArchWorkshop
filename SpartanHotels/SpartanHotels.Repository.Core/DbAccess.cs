using System.Linq;
using System.Linq.Expressions;
using System.Data.Objects;
using SpartanHotels.Entities;

namespace SpartanHotels.Repository.Core
{
    public class DbAccess
    {
        //from, to, roomtype, city
        public AvailabilityResponse GetAvailableRoomList(AvailabilityRequest request)
        {
            var response = new AvailabilityResponse();

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
                                      where (rs.FromDate >= request.FromDate && rs.FromDate <= request.ToDate && rs.BookStatusID == (int)BookingStatus.Confirmed)
                                      select rs;

                if (aviReservations.Any())
                {
                    var totalBooking = aviReservations.GroupBy(tb => new { tb.FromDate, tb.HotelRoomID }).Select(room => new
                                    {
                                        HotelRoomID = room.Key.HotelRoomID,
                                        FromDate = room.Key.FromDate,
                                        Total = room.Count()
                                    }).AsQueryable().GroupBy(a => new { a.HotelRoomID }).Select(b => new
                                    {
                                        HotelRoomID = b.Key.HotelRoomID,
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
                                                        Room = new Room
                                                        {
                                                            Id = avi.HotelRoomID.ToString(),
                                                            Title = avi.RoomTypeID.ToString(),
                                                            Rate = (decimal)avi.Rate
                                                        },

                                                        AvailableCount = (int)avi.TotalRoomCount - roomsBooked
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
                            response.Rooms.Add( new Availability()
                               {
                                   Room = new Room
                                   {
                                       Id = avi.HotelRoomID.ToString(),
                                       Title = avi.RoomTypeID.ToString(),
                                       Rate = (decimal)avi.Rate
                                   },

                                   AvailableCount = (int)avi.TotalRoomCount
                               });
                        }
                    }

                }
            }

            return response;
        }

        public BookingResponse AddBooking(BookingRequest request)
        {
            BookingResponse response = new BookingResponse();
            using (var context = new SpartanHotelsEntities())
            {
                var eReservation = context.Reservations.Create();

                //eReservation.ConfirmationId = reservationId;

                //var aviRooms = GetAvailableRoom(request);

                //context.SaveChanges();

                //response.ConfirmationId = eReservation.ConfirmationId;
                //room number etc
            }

            return response;
        }

        public CancellationResponse CancelBooking(CancellationRequest request)
        {
            CancellationResponse response = new CancellationResponse();
            using (var context = new SpartanHotelsEntities())
            {
                var userReservations = context.Reservations.Where(rs => (rs.ConfirmationNum.ToString() == request.ConfirmationNumber && rs.BookStatusID == (int)BookingStatus.Confirmed));

                if (userReservations != null)
                {
                    foreach(var reservation in userReservations)
                        reservation.BookStatusID = (int)BookingStatus.Cancelled;
                    //context.UpdateObject(userReservations);
                    context.SaveChanges();
                }
                else
                {
                    //InvalidInput
                }
            }

            return response;
        }
    }



}

