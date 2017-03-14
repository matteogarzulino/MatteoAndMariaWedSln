using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MatteoAndMariaWedSln.Models;
using MatteoAndMariaWedSln.Results;
using MatteoAndMariaWedWebApp.Models;
using WeddingServices;
using WeddingServices.Services;
using WeddingServices.Utilities.Enums;
using WeddingServices.Utilities.Logging;

namespace MatteoAndMariaWedSln.BusinessLogic
{
    public class Services
    {
        public ServiceResult InsertGuestbook(GuestbookViewModel guestbookVM)
        {
            ServiceResult result;
            bool esito = false;
            string message = "Errore imprevisto";
            try
            {
                GuestbookServices services = new GuestbookServices();
                Guestbook guestBook = new Guestbook();
                guestBook.Guestname = guestbookVM.Guestname;
                guestBook.Message = guestbookVM.GuestbookContent;
                guestBook.DataInsert = DateTime.Now;
                esito = true;
                message = string.Empty;
                services.InsertGuestbook(guestBook);
                result = new ServiceResult(esito, message);
            }
            catch (Exception exc)
            {
                message = exc.Message;
                esito = false;
                result = new ServiceResult(esito, message, exc);
                exc.WriteToLog();
            }
            return result;
        }

        public List<GuestbookViewModel> TakeGuestbook()
        {
            int numGuests = 10;
            List<GuestbookViewModel> guestBooksVM = new List<GuestbookViewModel>();
            try
            {
                GuestbookServices services = new GuestbookServices();
                List<Guestbook> guestBooks = services.TakeGuestbooks(numGuests);
            }
            catch (Exception exc)
            {
                exc.WriteToLog();
            }
            return guestBooksVM;
        }

        public ServiceResult InsertRSVP(RSVPViewModel rsvpVM)
        {
            ServiceResult result;
            bool esito = false;
            string message = "Errore imprevisto";
            try
            {
                DateTime now = DateTime.Now;
                RSVPServices services = new RSVPServices();
                RSVP rsvp = new RSVP();
                rsvp.Name = rsvpVM.Name;
                rsvp.Email = rsvpVM.Email;
                rsvp.Esito = (int)rsvpVM.Esito;
                rsvp.SpecialMenu = rsvpVM.SpecialMenu;
                rsvp.Number = rsvpVM.Number;
                rsvp.DataInsert = now;
                rsvp.DataInizio = now;
                rsvp.DataFine = DateTime.MaxValue;
                rsvp.Notes = rsvpVM.Notes ?? string.Empty;
                rsvp.Guid = Guid.NewGuid().ToString();
                message = string.Empty;
                services.InsertRSVP(rsvp);

                SendMails(rsvp);

                esito = true;
                message = "Operazione completata";
                result = new ServiceResult(esito, message);
            }
            catch (Exception exc)
            {
                message = exc.Message;
                esito = false;
                result = new ServiceResult(esito, message, exc);
                exc.WriteToLog();
            }
            return result;
        }

        public RSVPViewModel GetRSVPByGUID(string guid)
        {
            RSVPViewModel rsvpViewMode;
            try
            {
                RSVPServices services = new RSVPServices();
                List<RSVP> rsvps = services.GetRSVPs();
                RSVP rsvp = rsvps.FirstOrDefault(x => string.Equals(x.Guid, guid, StringComparison.CurrentCultureIgnoreCase));
                if (rsvp == null)
                {
                    throw new Exception(string.Format("RSVP {0} non trovato.", guid));
                }
                rsvpViewMode = RSVPViewModel.ToRSVPViewModel(rsvp);
            }
            catch (Exception exc)
            {
                exc.WriteToLog();
                throw new Exception("Purtroppo il tuo RSVP non è stato trovato!");
            }
            return rsvpViewMode;
        }

        private void SendMails(RSVP rsvp)
        {
            try
            {
                MailServices mailSvc = new MailServices();

                string mailGuest = mailSvc.PrepareRSVPMailToGuest(rsvp);
                string mailAdmin = mailSvc.PrepareRSVPMailToAdmins(rsvp);
                mailSvc.SendMail("Il tuo RSVP", mailGuest, new List<string>() { rsvp.Email });
                mailSvc.SendMail("Nuovo RSVP", mailAdmin, new List<string>() { "m.garzu@gmail.com" });
            }
            catch (Exception exc)
            {
                exc.WriteToLog();
            }
        }
    }
}