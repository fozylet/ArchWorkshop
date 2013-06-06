using SpartanHotels.Entities;
using SpartanHotels.Worker.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace SpartanHotels.Worker
{
    public class NotificationManager : INotificationManager
    {
        private string SenderEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["SenderEmailAddress"];
            }
        }

        private string NotificationMailTitlePrefix
        {
            get
            {
                return ConfigurationManager.AppSettings["NotificationMailTitlePrefix"];
            }
        }

        public bool SendNotification(BookingResponse bookingResponse)
        {
            SmtpClient smtp = new SmtpClient();
            MailMessage message = new MailMessage();
            message.Sender = new MailAddress(SenderEmailAddress);
            message.From = new MailAddress(bookingResponse.Guest.EMailAddress);
            message.Subject = NotificationMailTitlePrefix + bookingResponse.BookingStatus.ToString();
            if (bookingResponse.BookingStatus ==BookingStatus.Rejected)
            {
                message.Body = string.Format(@"Dear Customer, We regret to inform you that your booking against 
                                                reservation number {0} was not successful.", bookingResponse.ReservationId);
            }
            else
            {
                message.Body = string.Format(@"Dear Customer, We are happy to inform you 
                                that your booking against reservation number {0} is Confirmed and your confirmation number is {1}.",
                                bookingResponse.ReservationId, bookingResponse.ConfirmationNumber);
            }

            try
            {
                smtp.Send(message);
            }
            catch
            {
                // todo : logging
                return false;
            }

            return true;
        }
    }
}
