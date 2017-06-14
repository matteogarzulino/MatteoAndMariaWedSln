using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using WeddingServices.Results;
using WeddingServices.Utilities.Enums;
using WeddingServices.Utilities.Logging;

namespace WeddingServices.Services
{
    public class MailServices
    {
        public MailServices()
        {

        }

        public MailServiceResult SendMail(string subject, string message, List<string> addresses)
        {
            try
            {
                string apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
                if (string.IsNullOrEmpty(apiKey))
                {
                    apiKey = "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";
                }

                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = false;
                client.Port = 587;
                client.Host = "smtp.sendgrid.net";
                client.Credentials = new NetworkCredential("apikey", apiKey);

                MailMessage mail = new MailMessage();
                foreach (string address in addresses)
                {
                    mail.To.Add(address);
                }
                mail.From = new MailAddress("m.garzu@gmail.com", "Matteo Garzulino");
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;

                client.Send(mail);

            }
            catch (Exception exc)
            {
                exc.WriteToLog();
                return new MailServiceResult(false, exc.Message, Utilities.Enums.ResultEnum.FAIL);
            }
            return new MailServiceResult(true, "Operazione completata", Utilities.Enums.ResultEnum.SUCCESS); ;
        }

        public string PrepareRSVPMailToAdmins(RSVP rsvp)
        {
            string htmlMail = string.Format("<h1>Abbiamo ricevuto un nuovo RSVP da {0} ({1})!</h1>", rsvp.Name, rsvp.Email);
            htmlMail += string.Format("<p><strong>RSVP: </strong>{0}</p>", ((RSVPEnum)rsvp.Esito).GetDescription());
            htmlMail += string.Format("<p><strong>Persone: </strong>{0}</p>", rsvp.Number.ToString());
            htmlMail += string.Format("<p><strong>Menu speciale: </strong>{0}</p>", rsvp.SpecialMenu ? "Si" : "No");
            htmlMail += string.Format("<p><strong>Note menu speciale: </strong>{0}</p>", rsvp.Notes);
            htmlMail += string.Format("<p><strong>Consenso foto: </strong>{0}</p>", rsvp.ConsensoPrivacy ? "Si" : "No");
            htmlMail += "<hr>";
            htmlMail += string.Format("<p><strong>GUID: </strong>{0}</p>", rsvp.Guid.ToString());


            htmlMail += GetRSVPLinkHtml(rsvp);
            return htmlMail;
        }

        public string PrepareRSVPMailToGuest(RSVP rsvp)
        {
            string htmlMail = string.Format("<h1>Grazie {0} per il tuo RSVP!</h1>", rsvp.Name);
            bool pluralize = rsvp.Number > 1;
            string esitoHtml = "<p>";
            string salutoHtml = "<p>";
            switch (rsvp.Esito)
            {
                case (int)RSVPEnum.ASSENTE:
                    esitoHtml += "Ci dispiace che non potrai esserci...speriamo comunque di poterci vedere presto!";

                    break;
                case (int)RSVPEnum.PRESENTE_RICEVIMENTO:
                case (int)RSVPEnum.PRESENTE_CERIMONIA:
                case (int)RSVPEnum.PRESENTE:
                    esitoHtml += "Siamo molto contenti di poter festeggiare il nostro giorno con ";
                    if (pluralize)
                    {
                        esitoHtml += "voi!";
                    }
                    else
                    {
                        esitoHtml += "te!";
                    }
                    salutoHtml += "Ci vediamo il <strong>24 Agosto 2017, h. 16.00</strong>!";
                    break;
            }
            esitoHtml += "!";
            esitoHtml += "</p>";


            htmlMail += esitoHtml;
            htmlMail += salutoHtml;

            string rsvpLink = "<p>";
            rsvpLink += "Per modificare, <a href='http://mariaematteo.com/#RSVP'>contattaci</a>!";
            rsvpLink += "</p>";
            htmlMail += rsvpLink;

            htmlMail += "<p>Matteo e Maria</p>";
            return htmlMail;
        }

        private string GetRSVPLinkHtml(RSVP rsvp)
        {
            string rsvpLink = "<p>";
            rsvpLink += "Premi <a href='http://mariaematteo.com/RSVP/MyRSVP?Guid=" + rsvp.Guid + "'>qui</a> per modificare il tuo RSVP!";
            rsvpLink += "</p>";
            return rsvpLink;
        }
    }
}
