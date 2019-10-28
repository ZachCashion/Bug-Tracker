using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bug_Tracker.Models;


namespace Bug_Tracker.Helpers
{
    public class SearchHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static IQueryable<Project> IndexSearch(string searchStr)
        {
            IQueryable<Project> result = null;
            if (searchStr != null)
            {
                result = db.Projects.AsQueryable();
                result = result.Where(p => p.Name.Contains(searchStr) ||
                p.Description.Contains(searchStr) ||
                p.Tickets.Any(c => c.Discription.Contains(searchStr) ||
                c.Submiter.FirstName.Contains(searchStr) ||
                c.Submiter.LastName.Contains(searchStr) ||
                c.Submiter.DisplayName.Contains(searchStr) ||
                c.Submiter.Email.Contains(searchStr)));
            }
            else
            {
                result = db.Projects.AsQueryable();
            }
            return result.OrderByDescending(p => p.Created);
        }
    }
}