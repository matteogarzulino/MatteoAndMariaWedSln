using System;
using System.Collections.Generic;
using WeddingServices.Repository;

namespace WeddingServices.Services
{
    public class GuestbookServices
    {
        public void InsertGuestbook(Guestbook guestBook)
        {
            GuestbookRepository.InsertGuestbook(guestBook);
        }

        public List<Guestbook> TakeGuestbooks(int? numGuestbooks)
        {
            return GuestbookRepository.GetGuestbooks(numGuestbooks);
        }

        public List<Guestbook> GetGuestbooks()
        {
            return GuestbookRepository.GetGuestbooks(null);
        }

        public Guestbook GetGuestbookById(int idGuestbook)
        {
            return GuestbookRepository.GetGuestbookById(idGuestbook);
        }

        public void ConfirmGuestbook(int idGuestbook, bool visible)
        {
            GuestbookRepository.ConfirmGuestbook(idGuestbook,  visible);
        }
    }
}
