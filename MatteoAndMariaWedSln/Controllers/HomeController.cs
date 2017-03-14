using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MatteoAndMariaWedSln.BusinessLogic;
using MatteoAndMariaWedSln.Models;
using MatteoAndMariaWedSln.Results;
using MatteoAndMariaWedWebApp.Models;
using WeddingServices.Utilities;
using WeddingServices.Utilities.Logging;

namespace MatteoAndMariaWedSln.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Route("Viaggio")]
        public ActionResult Viaggio()
        {
            ViewBag.Message = "Dettagli viaggio di nozze";
            return View();
        }

        [Route("Cerimonia")]
        public ActionResult Cerimonia()
        {
            ViewBag.Message = "Dettagli luogo cerimonia: Chiesa, indirizzo e coordinate, Mappa";
            return View();
        }

        [Route("RSVP")]
        public ActionResult RSVP()
        {
            ViewBag.Message = "Contatti di RVSP; form invio messagio mail?";
            return View();
        }

        [HttpPost]
        [Route("RSVP")]
        public ActionResult RSVP(RSVPViewModel model)
        {
            try
            {
                Services srv = new Services();
                ServiceResult result = srv.InsertRSVP(model);
                if (!result.Esito && result.Exception == null)
                {
                    throw new Exception(result.Message);
                }
                else if (!result.Esito && result.Exception != null)
                {
                    throw result.Exception;
                }
            }
            catch (Exception exc)
            {
                exc.WriteToLog();
                return RedirectToAction("Error", "Home", exc.ToCompleteMessage());
            }
            ViewBag.Message = "Contatti di RVSP; form invio messagio mail?";
            return View();
        }
        [Route("Ricevimento")]
        public ActionResult Ricevimento()
        {
            ViewBag.Message = "Dettagli luogo ricevimento: Ristorante, indirizzo e coordinate, Mappa, sitoweb";
            return View();
        }
        [Route("Guestbook")]
        public ActionResult Guestbook()
        {
            ViewBag.Message = "Contatti di RVSP; form invio messagio mail?";
            return View();
        }
        [HttpPost]
        [Route("Guestbook")]
        public ActionResult Guestbook(GuestbookViewModel model)
        {
            try
            {
                Services bl = new Services();
                ServiceResult result = bl.InsertGuestbook(model);
                if (!result.Esito && result.Exception == null)
                {
                    throw new Exception(result.Message);
                }
                else if (!result.Esito && result.Exception != null)
                {
                    throw result.Exception;
                }
            }
            catch (Exception exc)
            {
                exc.WriteToLog();
                return RedirectToAction("ErrorPage", "Home", new { errMsg = exc.ToCompleteMessage() });
            }
            return RedirectToAction("Guestbook");
        }

        [HttpGet]
        [Route("GuestbookList")]
        public ActionResult GuestbookList()
        {
            Services bl = new Services();
            var guestbooks = bl.TakeGuestbook();
            return PartialView("GuestbookList", guestbooks);
        }

        [Route("ErrorPage")]
        public ActionResult ErrorPage(string errMsg)
        {
            ViewBag.Message = errMsg;
            return View();
        }
    }
}