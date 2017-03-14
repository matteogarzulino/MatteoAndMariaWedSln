using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatteoAndMariaWedWebApp.Models
{
    public class GuestbookViewModel
    {
        #region Properties
        public int IdGuestbook { get; set; }
        [Required]
        [Display(Name = "Il tuo nome:")]
        public string Guestname { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Messaggio:")]
        public string GuestbookContent { get; set; }
        
        public DateTime InsertDate { get; set; }
        #endregion Properties

        #region Constructor
        public GuestbookViewModel()
        {

        }
        #endregion Constructor
    }
}
