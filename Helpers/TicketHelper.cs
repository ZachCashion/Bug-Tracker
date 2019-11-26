using Bug_Tracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bug_Tracker.Helpers
{
    public class TicketHelper 
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper rolesHelper = new UserRolesHelper();

        public int SetDefaultTicketStatus()
        {
            return db.TicketStatus.FirstOrDefault(ts => ts.Name == "Open").Id;
        }

        
        public void AssignTicket(string userId, int ticketId)
        {
            Ticket ticket = db.Tickets.Find(ticketId);
            var newUser = db.Users.Find(userId);
            ticket.DeveloperID = newUser.Id;

            db.SaveChanges();
        }

        
        public void UnassignTicket(int ticketId)
        {
            Ticket ticket = db.Tickets.Find(ticketId);
            ticket.DeveloperID = null;
            
            db.SaveChanges(); 
        }


        public List<Ticket> ListMyTickets()
        {
            var myTickets = new List<Ticket>();
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            //Step 1: Deternine my Role
            var myRole = rolesHelper.ListUserRoles(userId).FirstOrDefault();

            //Step 2: Use that role to build the appropriate set of Tickets
            //if(myRole == "Admin")
            //{
            //    myTickets.AddRange(db.Tickets);
            //}
            //else if(myRole == "Project_Manager")
            //{               
            //    myTickets.AddRange(user.Projects.SelectMany(p => p.Tickets));
            //}
            //else if(myRole == "Developer")
            //{
            //    myTickets.AddRange(db.Tickets.Where(t => t.DeveloperId == userId));
            //}
            //else if(myRole == "Submitter")
            //{
            //    myTickets.AddRange(db.Tickets.Where(t => t.SubmitterId == userId));
            //}

            switch (myRole)
            {
                
                case "Admin":
                    myTickets.AddRange(db.Tickets);
                    break;
                case "Manager":
                    myTickets.AddRange(user.Projects.SelectMany(p => p.Tickets));
                    break;
                case "Developer":
                    myTickets.AddRange(db.Tickets.Where(t => t.DeveloperID == userId));
                    break;
                case "Submiter":
                    myTickets.AddRange(db.Tickets.Where(t => t.SubmiterID == userId));
                    break;
            }
            return myTickets;
        }
    }
}