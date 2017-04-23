using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using MatteoAndMariaWedSln.BusinessLogic;
using MatteoAndMariaWedSln.Models;
using MatteoAndMariaWedSln.Results;
using WeddingServices.Utilities;
using WeddingServices.Utilities.Logging;

namespace MatteoAndMariaWedSln.Controllers
{
    public class RSVPController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(RSVPController));

        [HttpGet]
        public ActionResult RSVP()
        {
            return View();
        }

        [HttpPost]
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
                return RedirectToAction("ErrorPage", "Home", exc.ToCompleteMessage());
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MyRSVP(string guid)
        {
            try
            {
                RSVPViewModel rsvp = new Services().GetRSVPByGUID(guid);
                return View(rsvp);
            }
            catch (Exception exc)
            {
                log.Error("Errore!", exc);
                exc.WriteToLog();
                return RedirectToAction("ErrorPage", "Home", exc.ToCompleteMessage());
            }
        }

        [HttpGet]
        public ActionResult Edit(string guid)
        {
            try
            {
                RSVPViewModel rsvp = new Services().GetRSVPByGUID(guid);
                return View(rsvp);
            }
            catch (Exception exc)
            {
                log.Error("Errore!", exc);
                exc.WriteToLog();
                return RedirectToAction("ErrorPage", "Home", exc.ToCompleteMessage());
            }
        }

        [HttpPost]
        public ActionResult Edit(RSVPViewModel model)
        {
            try
            {
                Services srv = new Services();
                ServiceResult result = srv.UpdateRSVP(model);
                if (!result.Esito && result.Exception == null)
                {
                    throw new Exception(result.Message);
                }
                else if (!result.Esito && result.Exception != null)
                {
                    throw result.Exception;
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception exc)
            {
                log.Error("Errore!", exc);
                exc.WriteToLog();
                return RedirectToAction("ErrorPage", "Home", exc.ToCompleteMessage());
            }
        }
    }
}
