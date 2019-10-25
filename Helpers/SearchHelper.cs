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
        public static IQueryable<BlogPost> IndexSearch(string searchStr)
        {
            IQueryable<BlogPost> result = null;
            if (searchStr != null)
            {
                result = db.BlogPost.AsQueryable();
                result = result.Where(p => p.Title.Contains(searchStr) ||
                p.BlogPostBody.Contains(searchStr) ||
                p.Comments.Any(c => c.CommentBody.Contains(searchStr) ||
                c.Author.FirstName.Contains(searchStr) ||
                c.Author.LastName.Contains(searchStr) ||
                c.Author.DisplayName.Contains(searchStr) ||
                c.Author.Email.Contains(searchStr)));
            }
            else
            {
                result = db.BlogPost.AsQueryable();
            }
            return result.OrderByDescending(p => p.Created);
        }
    }
}