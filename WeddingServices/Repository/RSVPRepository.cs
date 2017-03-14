using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingServices.Utilities.Logging;

namespace WeddingServices.Repository
{
    public static class RSVPRepository
    {
        public static void InsertRSVP(RSVP rsvpEntity)
        {
            try
            {
                using (var db = new MariaMatteoWedDBEntities())
                {
                    db.RSVP.Add(rsvpEntity);
                    db.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                exc.WriteToLog();
                throw exc;
            }
        }

        public static List<RSVP> GetRSVPs(int? count)
        {
            List<RSVP> guestbooks = new List<RSVP>();
            try
            {
                using (var db = new MariaMatteoWedDBEntities())
                {
                    if (count.HasValue)
                    {
                        guestbooks = db.RSVP.OrderByDescending(x => x.DataInsert).Take(count.Value).ToList();
                    }
                    else
                    {
                        guestbooks = db.RSVP.OrderByDescending(x => x.DataInsert).ToList();
                    }
                }
            }
            catch (Exception exc)
            {
                exc.WriteToLog();
                throw exc;
            }
            return guestbooks;
        }
    }
}
