using Bug_Tracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Helpers
{
    public class NotificationHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public void ManageNotifications(Ticket oldTicket, Ticket newTicket)
        {

        }
        public void AddAssignmentNotification(Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                TicketId = newTicket.Id,
                IsRead = false,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                RecipientId = newTicket.DeveloperID,
                Created = DateTime.Now,
                NotificationBody = $"You have been assigned to Ticket Id {newTicket.Id} on Project {newTicket.Project.Name}. The Ticket Title is {newTicket.Title}."
            };
            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }
        public void AddUnAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                TicketId = newTicket.Id,
                IsRead = false,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                RecipientId = newTicket.DeveloperID,
                Created = DateTime.Now,
                NotificationBody = $"You have been unassigned from Ticket Id {newTicket.Id} on Project {newTicket.Project.Name}. The Ticket Title is {newTicket.Title}."
            };
            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }
        public static List<TicketNotification> GetUnReadNotifications()
        {
            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            return db.TicketNotifications.Include("Sender").Include("Recipient").Where(t => t.RecipientId == currentUserId && !t.IsRead).ToList();
        }
    }
}