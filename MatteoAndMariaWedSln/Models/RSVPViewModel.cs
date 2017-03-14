using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
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
        [Display(Name="Email:")]
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
        public string Notes { get; set; }
        #endregion
        #region Constructor
        public RSVPViewModel()
        {
        }
        
        #endregion Constructor
    }
}