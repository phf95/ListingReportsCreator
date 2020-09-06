using ListingReportsCreator.BusinessLogic;
using ListingReportsCreator.BusinessLogic.Entities;
using ListingReportsCreator.BusinessLogic.Entities.Reports;
using ListingReportsCreator.BusinessLogic.Services;
using ListingReportsCreator.Infrastructure.CsvHelper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoScout24Listing.UnitTests
{
    [TestClass]
    public class AvailableCarsPercentualDistributionTest
    {
        private IListingReports _listingReports = new ListingReports(new CsvHelper());

        [TestMethod, Description("Test with valid data")]
        public void StandardInputTest()
        {
            List<ListingEntity> listingList = GenerateInputStandardTest();
            AvailableCarsPercentualDistributionReport expectedResult = GenerateExpectedResultOnStandardTest();

            AvailableCarsPercentualDistributionReport result = _listingReports.CalculateAvailableCarsPercentualDistributionReport(listingList);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod, Description("Test with null or empty listings")]
        public void NullOrEmptyInputTest()
        {
            List<ListingEntity> listingList = null;
            AvailableCarsPercentualDistributionReport expectedResult = null;

            AvailableCarsPercentualDistributionReport result = _listingReports.CalculateAvailableCarsPercentualDistributionReport(listingList);

            result.Should().BeEquivalentTo(expectedResult);
        }

        #region Private Methods
        private AvailableCarsPercentualDistributionReport GenerateExpectedResultOnStandardTest()
        {
            List<AvailableCarsPercentualDistribution> list = new List<AvailableCarsPercentualDistribution>();
            list.Add(new AvailableCarsPercentualDistribution { Make = "BMW", Distribution = "67.5%" });
            list.Add(new AvailableCarsPercentualDistribution { Make = "Audi", Distribution = "25%" }); 
            list.Add(new AvailableCarsPercentualDistribution { Make = "Mercedes", Distribution = "7.5%" });

            return new AvailableCarsPercentualDistributionReport { AvailableCarsPercentualDistributionList = list };
        }

        private static List<ListingEntity> GenerateInputStandardTest()
        {
            List<ListingEntity> listingList = new List<ListingEntity>();
            for (int i = 0; i < 150; i++)
            {
                listingList.Add(new ListingEntity { Id = i, Make = "BMW", Mileage = 2000, Price = 40000, SellerType = "private" });
                listingList.Add(new ListingEntity { Id = i, Make = "Mercedes", Mileage = 2000, Price = 30000, SellerType = "private" });
            }

            for (int i = 150; i < 400; i++)
            {
                listingList.Add(new ListingEntity { Id = i, Make = "Audi", Mileage = 2000, Price = 20000, SellerType = "dealer" });
                listingList.Add(new ListingEntity { Id = i, Make = "Audi", Mileage = 2000, Price = 80000, SellerType = "dealer" });
            }

            for (int i = 400; i < 1000; i++)
            {
                listingList.Add(new ListingEntity { Id = i, Make = "BMW", Mileage = 2000, Price = 20000, SellerType = "other" });
                listingList.Add(new ListingEntity { Id = i, Make = "BMW", Mileage = 2000, Price = 100000, SellerType = "other" });
            }

            return listingList;
        }
        #endregion
    }
}
