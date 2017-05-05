using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MatteoAndMariaWedSln.BusinessLogic;
using MatteoAndMariaWedSln.Results;
using MatteoAndMariaWedWebApp.Models;
using WeddingServices.Utilities;
using WeddingServices.Utilities.Logging;

namespace MatteoAndMariaWedSln.Controllers
{
    public class GuestbookController : Controller
    {
        // GET: Guestbook
        public ActionResult Get()
        {
            try
            {
                Services services = new Services();
                List<GuestbookViewModel> guestbooks = services.TakeGuestbook();
                return PartialView(guestbooks);
            }
            catch (Exception exc)
            {
                exc.WriteToLog();
                return RedirectToAction("ErrorPage", "Home", new { errMsg = exc.ToCompleteMessage() });
            }
        }

        // GET: Guestbook/Create
        public ActionResult New()
        {
            return View();
        }

        // POST: Guestbook/Create
        [HttpPost]
        public ActionResult New(GuestbookViewModel model)
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
            return Redirect(Url.RouteUrl(new { controller = "Home", action = "Index" }) + "#Guestbook");
        }
    }
}
