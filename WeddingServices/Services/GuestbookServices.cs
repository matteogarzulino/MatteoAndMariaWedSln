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

        public List<Guestbook> TakeGuestbooks(int numGuestbooks)
        {
            return GuestbookRepository.GetGuestbooks(numGuestbooks);
        }

        public List<Guestbook> GetGuestbooks()
        {
            return GuestbookRepository.GetGuestbooks(null);
        }
    }
}
