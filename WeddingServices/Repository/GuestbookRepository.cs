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
            }
            catch (Exception exc)
            {
                exc.WriteToLog();
                throw exc;
            }
        }

        public static Guestbook GetGuestbookById(int idGuestbook)
        {
            Guestbook guestbook;
            try
            {
                using (var db = new MariaMatteoWedDBEntities())
                {
                    guestbook = db.Guestbook.Where(x => x.IdGuestbook == idGuestbook).FirstOrDefault();
                }
                if (guestbook == null)
                {
                    throw new Exception(string.Format("Guestbook non trovato. Id: {0}", idGuestbook));
                }
            }
            catch (Exception exc)
            {
                exc.WriteToLog();
                throw exc;
            }
            return guestbook;
        }

        public static void ConfirmGuestbook(int idGuestbook, bool visible)
        {
            try
            {
                using (var db = new MariaMatteoWedDBEntities())
                {
                    Guestbook currentGuestbook = db.Guestbook.Where(x => x.IdGuestbook == idGuestbook).FirstOrDefault();
                    if (currentGuestbook == null)
                    {
                        throw new Exception(string.Format("Guestbook non trovato. Id: {0}", idGuestbook));
                    }
                    currentGuestbook.Visibile = visible;
                    db.SaveChanges();
                }
            }
            catch (Exception exc)
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
                    if (count.HasValue)
                    {
                        guestbooks = db.Guestbook.Where(x=>x.Visibile).OrderByDescending(x => x.DataInsert).Take(count.Value).ToList();
                    }
                    else
                    {
                        guestbooks = db.Guestbook.Where(x => x.Visibile).OrderByDescending(x => x.DataInsert).ToList();
                    }
                }
            }
            catch (Exception exc)
            {
                exc.WriteToLog();
            }
            return guestbooks;
        }

        public static void UpdateGuestbook(Guestbook guestbook)
        {
            try
            {
                using (var db = new MariaMatteoWedDBEntities())
                {
                    Guestbook currentGuestbook = db.Guestbook.Where(x => x.IdGuestbook == guestbook.IdGuestbook).FirstOrDefault();
                    if (guestbook == null)
                    {
                        throw new Exception(string.Format("Guestbook non trovato. Id: {0}", guestbook.IdGuestbook));
                    }
                    currentGuestbook.Visibile = guestbook.Visibile;
                    db.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                exc.WriteToLog();
                throw exc;
            }
        }
    }
}
