using Bug_Tracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bug_Tracker.Controllers
{
    public class GraphingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Graphing
        public JsonResult PieChart1Data()
        {
            var myData = new List<PieChartData>();
            PieChartData data = null;
            foreach(var priority in db.TicketPriorities.ToList())
            {
                data = new PieChartData();
                data.label = priority.Name;
                data.value = db.Tickets.Where(t => t.TicketPriority.Name == priority.Name).Count();
                myData.Add(data);
            }

            return Json(myData);
        }
    }
}