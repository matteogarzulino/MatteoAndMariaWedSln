﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingServices.Repository;

namespace WeddingServices.Services
{
    public class RSVPServices
    {
        public void InsertRSVP(RSVP guestBook)
        {
            RSVPRepository.InsertRSVP(guestBook);
        }

        public List<RSVP> TakeRSVP(int numGuestbooks)
        {
            return RSVPRepository.GetRSVPs(numGuestbooks);
        }

        public List<RSVP> GetRSVPs()
        {
            return RSVPRepository.GetRSVPs(null);
        }
    }
}
