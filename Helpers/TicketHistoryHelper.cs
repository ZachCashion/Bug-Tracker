using Bug_Tracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Helpers
{
    public class TicketHistoryHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void RecordHistoricalChanges(Ticket oldTicket, Ticket newTicket)
        {
            if(oldTicket.TicketStatusId != newTicket.TicketStatusId)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Propery = "TicketStatusId",
                    OldValue = oldTicket.TicketStatus.Name,
                    NewValue = newTicket.TicketStatus.Name,
                    Updated = (DateTime)newTicket.Updated,
                    TicketId = newTicket.Id,
                    UserId = HttpContext.Current.User.Identity.GetUserId()
                };

                db.TicketHistories.Add(newHistoryRecord);
            }

            if(oldTicket.TicketPriorityId != newTicket.TicketPriorityId)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Propery = "TicketPriorityId",
                    OldValue = oldTicket.TicketPriority.Name,
                    NewValue = newTicket.TicketPriority.Name,
                    Updated = (DateTime)newTicket.Updated,
                    TicketId = newTicket.Id,
                    UserId = HttpContext.Current.User.Identity.GetUserId()
                };

                db.TicketHistories.Add(newHistoryRecord);
            }

            if (oldTicket.DeveloperID != newTicket.DeveloperID)
            {
                var newHistoryRecord = new TicketHistory
                {
                    Propery = "DeveloperID",
                    OldValue = oldTicket.Developer == null ? "Open" : oldTicket.Developer.DisplayName,
                    NewValue = newTicket.Developer == null ? "Open" : newTicket.Developer.DisplayName,
                    Updated = (DateTime)newTicket.Updated,
                    TicketId = newTicket.Id,
                    UserId = HttpContext.Current.User.Identity.GetUserId()
                };

                db.TicketHistories.Add(newHistoryRecord);
            }

            db.SaveChanges();
        }
    }
}