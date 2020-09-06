using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListingReportsCreator.BusinessLogic.Entities.Reports
{
    public class ListingReport
    {
        public AveragePriceReport AveragePriceReport { get; set; }
        public AvailableCarsPercentualDistributionReport AvailableCarsPercentualDistributionReport { get; set; }
        public AverageListingSellingPriceReport AverageListingSellingPriceReport { get; set; }
        public ContactedListingsPerMonthReport ContactedListingsPerMonthReport { get; set; }

        public void RenderReport()
        {
            if (this.AverageListingSellingPriceReport != null && this.AverageListingSellingPriceReport.AverageListingSellingPriceList.Any())
            {
                Console.WriteLine("Average Listing Selling Price per Seller Type");
                ConsoleTable.From(this.AverageListingSellingPriceReport.AverageListingSellingPriceList).Write();
                Console.WriteLine(String.Empty);
            }
            if (this.AvailableCarsPercentualDistributionReport != null && this.AvailableCarsPercentualDistributionReport.AvailableCarsPercentualDistributionList.Any())
            {
                Console.WriteLine("Percentual distribution of available cars by Make");
                ConsoleTable.From(this.AvailableCarsPercentualDistributionReport.AvailableCarsPercentualDistributionList).Write();
                Console.WriteLine(String.Empty);
            }
            if (this.AveragePriceReport != null)
            {
                Console.WriteLine("Average price of the 30% most contacted listings");
                Console.WriteLine(String.Format("Average price ({0}): {1}", this.AveragePriceReport.MostContactedListingsPercentageDisplay, this.AveragePriceReport.AveragePriceDisplay));
                Console.WriteLine(String.Empty);
            }
            if (this.ContactedListingsPerMonthReport != null && this.ContactedListingsPerMonthReport.ListingLists.Any())
            {
                Console.WriteLine("The Top 5 most contacted listings per Month");
                this.ContactedListingsPerMonthReport.ListingLists.ForEach(x => { Console.WriteLine(x.MonthDisplay); ConsoleTable.From(x.Listings).Write(); });
            }
        }
    }
}
