using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    public class TicketComment
    {
        public int Id { get; set; }
        public int TicketID { get; set; }
        public string UserId { get; set; }
        public string CommentBody { get; set; }
        public DateTime Created { get; set; }
       

        //Nav
        public virtual Ticket Ticket { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}