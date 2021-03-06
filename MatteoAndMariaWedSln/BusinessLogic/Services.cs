﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MatteoAndMariaWedSln.Models;
using MatteoAndMariaWedSln.Results;
using MatteoAndMariaWedWebApp.Models;
using WeddingServices;
using WeddingServices.Services;
using WeddingServices.Utilities;
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
                esito = true;
                message = string.Empty;
                services.InsertGuestbook(guestbookVM.ToGuestbookEntity(DateTime.Now));
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

        public List<GuestbookViewModel> TakeGuestbook(int? numGuests = null)
        {
            List<GuestbookViewModel> guestBooksVM = new List<GuestbookViewModel>();
            try
            {
                GuestbookServices services = new GuestbookServices();
                List<Guestbook> guestBooks = services.TakeGuestbooks(numGuests);
                if (guestBooks.Any())
                {
                    guestBooksVM = guestBooks.Select(x => GuestbookViewModel.GetViewModel(x)).ToList();
                }
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
                RSVP rsvp = rsvpVM.ToRSVPEntity(now);
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

        public ServiceResult UpdateRSVP(RSVPViewModel rsvpVM)
        {
            ServiceResult result;
            bool esito = false;
            string message = "Errore imprevisto";
            try
            {
                string lastGUID = rsvpVM.GUID;
                int lastId = rsvpVM.IdRsvp;
                DateTime now = DateTime.Now;
                RSVPServices services = new RSVPServices();
                RSVP rsvp = rsvpVM.ToRSVPEntity(now);
                message = string.Empty;
                services.UpdateRSVP(rsvp, lastId, lastGUID);

                SendMails(rsvp);

                esito = true;
                message = "Operazione completata";
                result = new ServiceResult(esito, message, rsvp.Guid);
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
                RSVP rsvp = services.GetCurrentRSVPByGUID(guid);
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