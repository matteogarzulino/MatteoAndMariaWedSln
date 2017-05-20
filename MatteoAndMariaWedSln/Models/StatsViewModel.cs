using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeddingServices;
using WeddingServices.Utilities.Enums;

namespace MatteoAndMariaWedSln.Models
{
    public class StatsViewModel
    {
        public Dictionary<RSVPEnum, int> RSVPCountByEsito
        {
            get; set;
        }

        public Dictionary<RSVPEnum, List<RSVPViewModel>> RSVPByEsito
        {
            get;set;
        }

        private StatsViewModel(Dictionary<RSVPEnum, int> rsvpCountByEsito, Dictionary<RSVPEnum, List<RSVPViewModel>> rsvpByEsito)
        {
            this.RSVPByEsito = rsvpByEsito;
            this.RSVPCountByEsito = rsvpCountByEsito;
        }

        public static StatsViewModel StatsViewModelFactory(Dictionary<RSVPEnum, int> rsvpCountByEsito, IEnumerable<IGrouping<RSVPEnum, RSVP>> rsvpByEsito)
        {
            Dictionary<RSVPEnum, List<RSVPViewModel>> vmRsvpByEsito = BuildVMFromModel(rsvpByEsito);
            return new StatsViewModel(rsvpCountByEsito, vmRsvpByEsito);
        }

        private static Dictionary<RSVPEnum, List<RSVPViewModel>> BuildVMFromModel(IEnumerable<IGrouping<RSVPEnum, RSVP>> rsvpByEsito)
        {
            Dictionary<RSVPEnum, List<RSVPViewModel>> vmRsvpByEsito = new Dictionary<RSVPEnum, List<RSVPViewModel>>();
            foreach (var k in rsvpByEsito.ToList())
            {
                List<RSVPViewModel> rsvp = k.Select(x => RSVPViewModel.ToRSVPViewModel(x)).ToList();
                vmRsvpByEsito.Add(k.Key, rsvp);
            }

            return vmRsvpByEsito;
        }
    }
}