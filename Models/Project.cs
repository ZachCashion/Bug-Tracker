using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }


        //Nav
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }


        //Constructor
        public Project() 
        {
            Tickets = new HashSet<Ticket>();
            ApplicationUsers = new HashSet<ApplicationUser>();
        }
        
    }
}