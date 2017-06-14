using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingServices;
using WeddingServices.Utilities;

namespace MatteoAndMariaWedWebApp.Models
{
    public class GuestbookViewModel
    {
        #region Properties
        public int IdGuestbook { get; set; }
        [Required]
        [Display(Name = "Il tuo nome")]
        public string Guestname { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Messaggio")]
        public string GuestbookContent { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime InsertDate { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime DataFine { get; set; }
        #endregion Properties

        #region Constructor
        public GuestbookViewModel()
        {

        }
        #endregion Constructor

        #region Factory
        public static GuestbookViewModel GetViewModel(Guestbook guestbook)
        {
            GuestbookViewModel guestbookVM = new GuestbookViewModel();
            guestbookVM.IdGuestbook = guestbook.IdGuestbook;
            guestbookVM.Guestname = guestbook.Guestname;
            guestbookVM.GuestbookContent = guestbook.Message;
            guestbookVM.InsertDate = guestbook.DataInsert;
            guestbookVM.DataInizio = guestbook.DataInizio;
            guestbookVM.DataFine = guestbook.DataFine;
            return guestbookVM;
        }
        #endregion Factory

        #region public
        public Guestbook ToGuestbookEntity(DateTime now)
        {
            Guestbook guestBook = new Guestbook();
            guestBook.Guestname = this.Guestname;
            guestBook.Message = this.GuestbookContent;
            guestBook.DataInsert = now;
            guestBook.DataInizio = now;
            guestBook.DataFine = DateTimeUtilities.MaxDateTime();
            guestBook.Visibile = false;
            return guestBook;
        }
        #endregion public
    }
}
