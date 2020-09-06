using Microsoft.VisualStudio.TestTools.UnitTesting;
using ListingReportsCreator.BusinessLogic;
using ListingReportsCreator.BusinessLogic.Services;
using ListingReportsCreator.Infrastructure.CsvHelper;
using System.Collections.Generic;
using ListingReportsCreator.BusinessLogic.Entities;
using System;
using ListingReportsCreator.BusinessLogic.Keys;
using ListingReportsCreator.BusinessLogic.Entities.Reports;
using FluentAssertions;

namespace AutoScout24Listing.UnitTests
{
    [TestClass]
    public class AverageListingSellingPriceTest
    {
        private IListingReports _listingReports = new ListingReports(new CsvHelper());

        [TestMethod, Description("Test 20M listings with all kind of seller types and with valid prices")]
        public void StandardInputTest()
        {
            List<ListingEntity> listingList = GenerateInputStandardTest();
            AverageListingSellingPriceReport expectedResult = GenerateExpectedResultOnStandardTest();

            AverageListingSellingPriceReport result = _listingReports.CalculateAverageListingSellingPriceReport(listingList);

            result.Should().BeEquivalentTo(expectedResult);
        }
        
        [TestMethod, Description("List without dealer listings. Report must show its average is € 0,-")]
        public void NoDealerListingsInputTest()
        {
            List<ListingEntity> listingList = GenerateInputNoDealerTest();
            AverageListingSellingPriceReport expectedResult = GenerateExpectedResultOnNoDealerListingsTest();

            AverageListingSellingPriceReport result = _listingReports.CalculateAverageListingSellingPriceReport(listingList);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod, Description("Test empty or null listing list")]
        public void NullOrEmptyListingListInputTest()
        {
            List<ListingEntity> listingList = null;
            AverageListingSellingPriceReport expectedResult = GenerateExpectedResultOnEmptyOrNullListingsTest();

            AverageListingSellingPriceReport result = _listingReports.CalculateAverageListingSellingPriceReport(listingList);

            result.Should().BeEquivalentTo(expectedResult);
        }

        #region Private Methods
        private static AverageListingSellingPriceReport GenerateExpectedResultOnStandardTest()
        {
            List<AverageListingSellingPrice> averageListingSellingPriceList = new List<AverageListingSellingPrice>();
            averageListingSellingPriceList.Add(new AverageListingSellingPrice { SellerType = SellerTypeCodes.Private, AverageAmountDisplay = "€ 35000,-" });
            averageListingSellingPriceList.Add(new AverageListingSellingPrice { SellerType = SellerTypeCodes.Dealer, AverageAmountDisplay = "€ 50000,-" });
            averageListingSellingPriceList.Add(new AverageListingSellingPrice { SellerType = SellerTypeCodes.Other, AverageAmountDisplay = "€ 60000,-" });

            return new AverageListingSellingPriceReport { AverageListingSellingPriceList = averageListingSellingPriceList };
        }

        private static AverageListingSellingPriceReport GenerateExpectedResultOnNoDealerListingsTest()
        {
            List<AverageListingSellingPrice> averageListingSellingPriceList = new List<AverageListingSellingPrice>();
            averageListingSellingPriceList.Add(new AverageListingSellingPrice { SellerType = SellerTypeCodes.Private, AverageAmountDisplay = "€ 35000,-" });
            averageListingSellingPriceList.Add(new AverageListingSellingPrice { SellerType = SellerTypeCodes.Other, AverageAmountDisplay = "€ 60000,-" });
            averageListingSellingPriceList.Add(new AverageListingSellingPrice { SellerType = SellerTypeCodes.Dealer, AverageAmountDisplay = "€ 0,-" });

            return new AverageListingSellingPriceReport { AverageListingSellingPriceList = averageListingSellingPriceList };
        }

        private static AverageListingSellingPriceReport GenerateExpectedResultOnEmptyOrNullListingsTest()
        {
            List<AverageListingSellingPrice> averageListingSellingPriceList = new List<AverageListingSellingPrice>();
            averageListingSellingPriceList.Add(new AverageListingSellingPrice { SellerType = SellerTypeCodes.Private, AverageAmountDisplay = "€ 0,-" });
            averageListingSellingPriceList.Add(new AverageListingSellingPrice { SellerType = SellerTypeCodes.Other, AverageAmountDisplay = "€ 0,-" });
            averageListingSellingPriceList.Add(new AverageListingSellingPrice { SellerType = SellerTypeCodes.Dealer, AverageAmountDisplay = "€ 0,-" });

            return new AverageListingSellingPriceReport { AverageListingSellingPriceList = averageListingSellingPriceList };
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

        private static List<ListingEntity> GenerateInputNoDealerTest()
        {
            List<ListingEntity> listingList = new List<ListingEntity>();
            for (int i = 0; i < 15; i++)
            {
                listingList.Add(new ListingEntity { Id = i, Make = "BMW", Mileage = 2000, Price = 40000, SellerType = "private" });
                listingList.Add(new ListingEntity { Id = i, Make = "Mercedes", Mileage = 2000, Price = 30000, SellerType = "private" });
            }

            for (int i = 40; i < 100; i++)
            {
                listingList.Add(new ListingEntity { Id = i, Make = "BMW", Mileage = 2000, Price = 20000, SellerType = "other" });
                listingList.Add(new ListingEntity { Id = i, Make = "BMW", Mileage = 2000, Price = 100000, SellerType = "other" });
            }

            return listingList;
        }
        #endregion
    }
}

