using System.Web.Mvc;
using System.Web.Routing;
using log4net;
using MatteoAndMariaWedSln.BusinessLogic;

namespace MatteoAndMariaWedSln.Controllers
{
    public class HomeController : Controller
    {

        private static readonly ILog log = LogManager.GetLogger(typeof(HomeController));
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