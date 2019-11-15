using Bug_Tracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Bug_Tracker.Helpers
{
    public class AttachmentHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public void addAttachment(Ticket ticket, HttpPostedFileBase file)
        {
            var fileName = DateTime.Now.Ticks + Path.GetFileName(file.FileName);
            //file.SaveAs(Path.Combine(Server.MapPath("~/Uploads/"), fileName));

            var attachment = new TicketAttachment
            {

            };

            db.TicketAttachments.Add(attachment);
            db.SaveChanges();
        }
    }
}