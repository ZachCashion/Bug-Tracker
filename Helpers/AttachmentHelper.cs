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

        public static string GetIcon(string fileName)
        {
            var imgPath = "";
            var ext = Path.GetExtension(fileName);
            switch (ext)
            {
                case ".css":
                    imgPath = "/FileIcons/css.png";
                    break;
                case ".js":
                    imgPath = "/FileIcons/javascript.png";
                    break;
                case ".html":
                    imgPath = "/FileIcons/html.png";
                    break;
                case ".pdf":
                    imgPath = "/FileIcons/pdf.png";
                    break;
                case ".doc":
                    imgPath = "/FileIcons/doc.png";
                    break;
                case ".docx":
                    imgPath = "/FileIcons/doc.png";
                    break;
                case ".xls":
                    imgPath = "/FileIcons/xls.png";
                    break;
                case ".xlsx":
                    imgPath = "/FileIcons/xls.png";
                    break;
                case ".txt":
                    imgPath = "/FileIcons/txt.png";
                    break;
                case ".zip":
                    imgPath = "/FileIcons/zip.png";
                    break;
                case ".7z":
                    imgPath = "/FileIcons/zip.png";
                    break;
                case ".xml":
                    imgPath = "/FileIcons/xml.jfif";
                    break;
                case ".jpg":
                    imgPath = fileName;
                    break;
                case ".gif":
                    imgPath = fileName;
                    break;
                case ".png":
                    imgPath = fileName;
                    break;
                default:
                    imgPath = "/FileIcons/file.png";
                    break;
            }
            return imgPath;
        }
    }
}