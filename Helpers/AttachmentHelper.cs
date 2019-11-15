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
                case ".pdf":
                    imgPath = "/Images/pdf.png";
                    break;
                case ".doc":
                    imgPath = "/Images/doc.png";
                    break;
                case ".dox":
                    imgPath = "/Images/docx.png";
                    break;
                case ".xls":
                    imgPath = "/Images/xls.png";
                    break;
                case ".xlsx":
                    imgPath = "/Images/xlsx.png";
                    break;
                case ".txt":
                    imgPath = "/Images/txt.png";
                    break;
                case ".zip":
                case ".rar":
                case ".7z":
                    imgPath = "/Images/zip.png";
                    break;
                case ".xml":
                    imgPath = "/Images/xml.jfif";
                    break;
                case ".JPG":
                    imgPath = fileName;
                    break;
                case ".gif":
                case ".png":
                    imgPath = fileName;
                    break;
                default:
                    imgPath = "/Images/blank.png";
                    break;
            }
            return imgPath;
        }
    }
}