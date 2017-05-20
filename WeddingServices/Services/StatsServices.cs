using System.Collections.Generic;
using System.Linq;
using WeddingServices.Repository;
using WeddingServices.Utilities.Enums;

namespace WeddingServices.Services
{
    public class StatsServices
    {
        public IEnumerable<IGrouping<RSVPEnum, RSVP>> GetRsvpGroupedByEsito()
        {
            List<RSVP> rsvps = RSVPRepository.GetRSVPs(null);
            return rsvps.GroupBy(x => (RSVPEnum)x.Esito);
        }

        public Dictionary<RSVPEnum, int> CountRsvpByEsito()
        {
            List<RSVP> rsvps = RSVPRepository.GetRSVPs(null);
            Dictionary<RSVPEnum, int> countByEsito = new Dictionary<RSVPEnum, int>();
            int presenti = rsvps.Where(x => (RSVPEnum)x.Esito == RSVPEnum.PRESENTE).Sum(x=>x.Number);
            int assenti = rsvps.Where(x => (RSVPEnum)x.Esito == RSVPEnum.ASSENTE).Sum(x => x.Number);
            countByEsito.Add(RSVPEnum.PRESENTE, presenti);
            countByEsito.Add(RSVPEnum.ASSENTE, assenti);
            return countByEsito;
        }
    }
}
