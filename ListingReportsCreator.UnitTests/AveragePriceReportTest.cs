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
    public class AveragePriceReportTest
    {
        private IListingReports _listingReports = new ListingReports(new CsvHelper());

        [TestMethod, Description("Calculate the average price of 30% most contacted with valid listings")]
        public void StandardInputTest()
        {
            List<ListingEntity> listingList = GenerateListingsInputStandardTest();
            List<ContactEntity> contactList = GenerateContactsInputStandardTest();

            AveragePriceReport expectedResult = GenerateExpectedResultOnStandardTest();

            AveragePriceReport result = _listingReports.CalculateAveragePriceReport(30, contactList, listingList);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod, Description("Calculate the average price of 30% most contacted when 30% of listings is less than 1")]
        public void LessThanOneTest()
        {
            List<ListingEntity> listingList = GenerateListingsInputLessThanOneTest();
            List<ContactEntity> contactList = GenerateContactsInputLessThanOneTest();

            AveragePriceReport expectedResult = GenerateExpectedResultOnLessThanOneTest();

            AveragePriceReport result = _listingReports.CalculateAveragePriceReport(30, contactList, listingList);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod, Description("Calculate the average price of 30% most contacted with inconsistent data. Try to get a listing that doesn't exist by listing_id")]
        public void InconsistentDataTest()
        {
            List<ListingEntity> listingList = GenerateListingsInconsistentDataTest();
            List<ContactEntity> contactList = GenerateContactsInconsistentDataTest();

            Assert.ThrowsException<Exception>(() =>_listingReports.CalculateAveragePriceReport(30, contactList, listingList));
        }

        #region Private Methods
        private List<ContactEntity> GenerateContactsInputStandardTest()
        {
            List<ContactEntity> contacts = new List<ContactEntity>();
           
            for(int i = 0; i < 1000; i++)
            {
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020,1,1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020,1,1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020,1,1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020,1,1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020,1,1) });

                contacts.Add(new ContactEntity { ListingId = 1020, ContactDate = new DateTime(2020,1,1) });
                contacts.Add(new ContactEntity { ListingId = 1020, ContactDate = new DateTime(2020,1,1) });
                contacts.Add(new ContactEntity { ListingId = 1020, ContactDate = new DateTime(2020,1,1) });

                contacts.Add(new ContactEntity { ListingId = 1015, ContactDate = new DateTime(2020,1,1) });
                contacts.Add(new ContactEntity { ListingId = 1015, ContactDate = new DateTime(2020,1,1) });

                contacts.Add(new ContactEntity { ListingId = 1021, ContactDate = new DateTime(2020,1,1) });

                contacts.Add(new ContactEntity { ListingId = 1001, ContactDate = new DateTime(2020,1,1) });

                contacts.Add(new ContactEntity { ListingId = 1002, ContactDate = new DateTime(2020,1,1) });

                contacts.Add(new ContactEntity { ListingId = 1005, ContactDate = new DateTime(2020,1,1) });

                contacts.Add(new ContactEntity { ListingId = 1007, ContactDate = new DateTime(2020,1,1) });

                contacts.Add(new ContactEntity { ListingId = 1009, ContactDate = new DateTime(2020,1,1) });

                contacts.Add(new ContactEntity { ListingId = 1008, ContactDate = new DateTime(2020,1,1) });
            }

            return contacts;
        }

        private AveragePriceReport GenerateExpectedResultOnStandardTest()
        {
            return new AveragePriceReport { AveragePriceDisplay = "€ 23333,33", MostContactedListingsPercentageDisplay = "30%" };
        }

        private List<ListingEntity> GenerateListingsInputStandardTest()
        {
            List<ListingEntity> listingList = new List<ListingEntity>();

            listingList.Add(new ListingEntity { Id = 1000, Make = "BMW", Mileage = 2000, Price = 20000, SellerType = "private" });
            listingList.Add(new ListingEntity { Id = 1020, Make = "BMW", Mileage = 2000, Price = 15000, SellerType = "private" });
            listingList.Add(new ListingEntity { Id = 1015, Make = "BMW", Mileage = 2000, Price = 35000, SellerType = "private" });
            listingList.Add(new ListingEntity { Id = 1021, Make = "BMW", Mileage = 2000, Price = 40000, SellerType = "private" });
            listingList.Add(new ListingEntity { Id = 1001, Make = "BMW", Mileage = 2000, Price = 40000, SellerType = "private" });
            listingList.Add(new ListingEntity { Id = 1002, Make = "BMW", Mileage = 2000, Price = 40000, SellerType = "private" });
            listingList.Add(new ListingEntity { Id = 1005, Make = "BMW", Mileage = 2000, Price = 40000, SellerType = "private" });
            listingList.Add(new ListingEntity { Id = 1007, Make = "BMW", Mileage = 2000, Price = 40000, SellerType = "private" });
            listingList.Add(new ListingEntity { Id = 1009, Make = "BMW", Mileage = 2000, Price = 40000, SellerType = "private" });
            listingList.Add(new ListingEntity { Id = 1008, Make = "BMW", Mileage = 2000, Price = 40000, SellerType = "private" });

            return listingList;
        }

        private List<ContactEntity> GenerateContactsInputLessThanOneTest()
        {
            List<ContactEntity> contacts = new List<ContactEntity>();

            for (int i = 0; i < 1000; i++)
            {
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 1, 1) });

                contacts.Add(new ContactEntity { ListingId = 1020, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1020, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1020, ContactDate = new DateTime(2020, 1, 1) });
            }

            return contacts;
        }

        private AveragePriceReport GenerateExpectedResultOnLessThanOneTest()
        {
            return new AveragePriceReport { AveragePriceDisplay = "€ 20000,-", MostContactedListingsPercentageDisplay = "30%" };
        }

        private List<ListingEntity> GenerateListingsInputLessThanOneTest()
        {
            List<ListingEntity> listingList = new List<ListingEntity>();

            listingList.Add(new ListingEntity { Id = 1000, Make = "BMW", Mileage = 2000, Price = 20000, SellerType = "private" });
            listingList.Add(new ListingEntity { Id = 1020, Make = "BMW", Mileage = 2000, Price = 15000, SellerType = "private" });

            return listingList;
        }

        private List<ContactEntity> GenerateContactsInconsistentDataTest()
        {
            List<ContactEntity> contacts = new List<ContactEntity>();

            for (int i = 0; i < 1000; i++)
            {
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 1, 1) });

                contacts.Add(new ContactEntity { ListingId = 1020, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1020, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1020, ContactDate = new DateTime(2020, 1, 1) });
            }

            return contacts;
        }

        private List<ListingEntity> GenerateListingsInconsistentDataTest()
        {
            List<ListingEntity> listingList = new List<ListingEntity>();

            listingList.Add(new ListingEntity { Id = 1020, Make = "BMW", Mileage = 2000, Price = 15000, SellerType = "private" });

            return listingList;
        }

        #endregion
    }
}
