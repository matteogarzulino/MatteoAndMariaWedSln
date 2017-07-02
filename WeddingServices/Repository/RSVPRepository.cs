using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingServices.Utilities;
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
                Trace.TraceError("RSVP Repository Error");
                exc.WriteToLog();
                throw exc;
            }
        }

        public static void UpdateRSVP(RSVP rsvpEntity, int lastId, string lastGUID)
        {
            try
            {
                using (var db = new MariaMatteoWedDBEntities())
                {
                    DateTime maxDateTime = DateTimeUtilities.MaxDateTime();
                    var current = db.RSVP.Where(x => x.IdRsvp == lastId && x.Guid == lastGUID && x.DataFine == maxDateTime).FirstOrDefault();

                    if (current != null)
                    {
                        current.DataFine = rsvpEntity.DataInizio;
                    }
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

        public static RSVP GetCurrentRSVPByGUID(string guid)
        {
            RSVP rsvp = null;
            try
            {
                using (var db = new MariaMatteoWedDBEntities())
                {
                    DateTime maxDate = DateTimeUtilities.MaxDateTime();
                    rsvp = db.RSVP.Where(x => x.Guid == guid && x.DataFine == maxDate).FirstOrDefault();
                }
            }
            catch (Exception exc)
            {
                exc.WriteToLog();
                throw exc;
            }
            return rsvp;
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
