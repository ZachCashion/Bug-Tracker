using Bug_Tracker.Models;
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

            db.SaveChanges();
        }
    }
}