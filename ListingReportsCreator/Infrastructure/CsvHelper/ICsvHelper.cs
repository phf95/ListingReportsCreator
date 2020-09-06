using ListingReportsCreator.BusinessLogic.Entities;
using System.Collections.Generic;

namespace ListingReportsCreator.Infrastructure.CsvHelper
{
    public interface ICsvHelper
    {
        List<ContactEntity> CsvToContactList();
        List<ListingEntity> CsvToListingList();
        
    }
}
