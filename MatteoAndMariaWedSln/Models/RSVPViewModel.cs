using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WeddingServices;
using WeddingServices.Utilities;
using WeddingServices.Utilities.Enums;

namespace MatteoAndMariaWedSln.Models
{
    public class RSVPViewModel
    {
        #region
        public int IdRsvp { get; set; }
        public string GUID { get; set; }
        [Display(Name = "Nome:")]
        public string Name { get; set; }
        [Display(Name = "Email:")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "In tutto siamo in...")]
        public int Number { get; set; }
        [Display(Name = "Ci sarai?")]
        public RSVPEnum Esito { get; set; }
        public DateTime DataInsert { get; set; }
        [Display(Name = "Menu speciale?")]
        public bool SpecialMenu { get; set; }
        [Display(Name = "Note:")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
        [Display(Name = "Vogliamo comparire nelle foto svolte durante la cerimonia e ricevimento")]
        [DefaultValue(true)]
        public bool ConsensoPrivacy { get; set; }
        #endregion
        #region Constructor
        public RSVPViewModel()
        {
        }

        public static RSVPViewModel ToRSVPViewModel(RSVP rsvp)
        {
            RSVPViewModel rsvpViewModel = new RSVPViewModel();
            rsvpViewModel.Email = rsvp.Email;
            rsvpViewModel.GUID = rsvp.Guid;
            rsvpViewModel.IdRsvp = rsvp.IdRsvp;
            rsvpViewModel.Number = rsvp.Number;
            rsvpViewModel.Esito = (RSVPEnum)rsvp.Esito;
            rsvpViewModel.SpecialMenu = rsvp.SpecialMenu;
            rsvpViewModel.DataInsert = rsvp.DataInsert;
            rsvpViewModel.Notes = rsvp.Notes;
            rsvpViewModel.Name = rsvp.Name;
            rsvpViewModel.ConsensoPrivacy = rsvp.ConsensoPrivacy;
            return rsvpViewModel;
        }

        public RSVP ToRSVPEntity(DateTime dataInsert)
        {
            RSVP rsvp = new RSVP();
            rsvp.Name = this.Name;
            rsvp.Email = this.Email;
            rsvp.Esito = (int)this.Esito;
            rsvp.SpecialMenu = this.SpecialMenu;
            rsvp.Number = this.Number;
            rsvp.DataInsert = dataInsert;
            rsvp.DataInizio = dataInsert;
            rsvp.DataFine = DateTimeUtilities.MaxDateTime();
            rsvp.Notes = this.Notes ?? string.Empty;
            rsvp.Guid = Guid.NewGuid().ToString();
            rsvp.ConsensoPrivacy = this.ConsensoPrivacy;
            return rsvp;
        }

        #endregion Constructor
    }
}