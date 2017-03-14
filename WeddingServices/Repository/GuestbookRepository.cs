using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingServices.Utilities.Logging;

namespace WeddingServices.Repository
{
    public static class GuestbookRepository
    {
        public static void InsertGuestbook(Guestbook guestbookEntity)
        {
            try
            {
                using (var db = new MariaMatteoWedDBEntities())
                {
                    db.Guestbook.Add(guestbookEntity);
                    db.SaveChanges();
                }
            }catch(Exception exc)
            {
                exc.WriteToLog();
                throw exc;
            }
        }

        public static List<Guestbook> GetGuestbooks(int? count)
        {
            List<Guestbook> guestbooks = new List<Guestbook>();
            try
            {
                using (var db = new MariaMatteoWedDBEntities())
                {
                    if (count.HasValue) {
                        guestbooks = db.Guestbook.OrderByDescending(x => x.DataInsert).Take(count.Value).ToList();
                    }
                    else
                    {
                        guestbooks = db.Guestbook.OrderByDescending(x => x.DataInsert).ToList();
                    }
                }
            }catch(Exception exc)
            {
                exc.WriteToLog();
            }
            return guestbooks;
        }
    }
}
