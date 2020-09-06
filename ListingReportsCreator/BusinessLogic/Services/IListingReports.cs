using ListingReportsCreator.BusinessLogic.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListingReportsCreator.BusinessLogic.Services
{
    public interface IListingReports
    {
        ListingReport CalculateAutoScoutListingReport();
        AveragePriceReport CalculateAveragePriceReport(int percentage, List<Entities.ContactEntity> contacts, List<Entities.ListingEntity> listings);
        AvailableCarsPercentualDistributionReport CalculateAvailableCarsPercentualDistributionReport(List<Entities.ListingEntity> listings);
        AverageListingSellingPriceReport CalculateAverageListingSellingPriceReport(List<Entities.ListingEntity> listings);
        ContactedListingsPerMonthReport CalculateContactedListingsPerMonthReport(List<Entities.ContactEntity> contacts, List<Entities.ListingEntity> listings);
    }
}
