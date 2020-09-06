using System;
using System.Collections.Generic;
using System.Text;

namespace ListingReportsCreator.BusinessLogic.Entities.Reports
{
    public class ContactedListingList
    {
        public string MonthDisplay { get; set; }
        public List<ContactedListing> Listings { get; set; }
    }
}
