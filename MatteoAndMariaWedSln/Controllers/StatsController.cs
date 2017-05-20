using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using MatteoAndMariaWedSln.BusinessLogic;
using MatteoAndMariaWedSln.Models;
using WeddingServices.Utilities.Logging;

namespace MatteoAndMariaWedSln.Controllers
{
    public class StatsController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RSVPController));
        public ActionResult Index()
        {
            ActionResult result;
            try
            {
                Services services = new Services();
                StatsViewModel stats = services.GetStats();
                result = View(stats);
            }
            catch (Exception exc)
            {
                exc.WriteToLog();
                result = Redirect(Url.RouteUrl(new { controller = "Home", action = "Index" }));
            }
            return result;
        }
    }
}