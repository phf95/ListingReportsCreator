using System;
using System.Collections.Generic;
using System.Text;

namespace ListingReportsCreator.BusinessLogic.Entities.Reports
{
    public class ContactedListing
    {
        public int Ranking { get; set; }
        public long ListingId { get; set; }
        public string Make { get; set; }
        public string SellingPriceDisplay { get; set; }
        public string MileageDisplay { get; set; }
        public int TotalContactsAmount { get; set; }
    }
}
