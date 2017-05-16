using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using log4net;
using MatteoAndMariaWedSln.BusinessLogic;
using MatteoAndMariaWedSln.Models;
using MatteoAndMariaWedSln.Results;
using reCAPTCHA.MVC;
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
        [ValidateAntiForgeryToken]
        public ActionResult RSVP(RSVPViewModel model)
        {
            try
            {
                var r = Request.Params["g-recaptcha-response"];
                // ... validate null or empty value if you want
                // then
                // make a request to recaptcha api
                using (var wc = new WebClient())
                {
                    var validateString = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                       "6LdjiCEUAAAAALlOXpRX1a13tQL7z7f_FoSMo2dK",    // secret recaptcha key
                       r); // recaptcha value
                    // Get result of recaptcha
                    var recaptcha_result = wc.DownloadString(validateString);
                    // Just check if request make by user or bot
                    if (recaptcha_result.ToLower().Contains("false"))
                    {
                        return Redirect(Url.RouteUrl(new { controller = "Home", action = "Index" }) + "#RSVP");
                    }
                }

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

            //return RedirectToAction("Index", "Home");
            return Redirect(Url.RouteUrl(new { controller = "Home", action = "Index" }) + "#RSVP");
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
