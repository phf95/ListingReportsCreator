using ListingReportsCreator.BusinessLogic.Services;

namespace ListingReportsCreator
{
    public class StartUp
    {
        private readonly IListingReports _listingReport;

        public StartUp(IListingReports listingReport)
        {
            _listingReport = listingReport;
        }

        public void Run() {
            BusinessLogic.Entities.Reports.ListingReport report = _listingReport.CalculateAutoScoutListingReport();
            report.RenderReport();
        }
    }
}
