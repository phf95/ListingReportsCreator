using ListingReportsCreator.BusinessLogic;
using ListingReportsCreator.BusinessLogic.Entities;
using ListingReportsCreator.BusinessLogic.Entities.Reports;
using ListingReportsCreator.BusinessLogic.Services;
using ListingReportsCreator.Infrastructure.CsvHelper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AutoScout24Listing.UnitTests
{
    [TestClass]
    public class ContactedListingsPerMonthTest
    {
        private IListingReports _listingReports = new ListingReports(new CsvHelper());

        [TestMethod, Description("Calculate Contacted listings per month with valid data")]
        public void StandardInputTest()
        {
            List<ListingEntity> listingList = GenerateListingsInputStandardTest();
            List<ContactEntity> contactList = GenerateContactsInputStandardTest();

            ContactedListingsPerMonthReport expectedResult = GenerateExpectedResultOnStandardTest();

            ContactedListingsPerMonthReport result = _listingReports.CalculateContactedListingsPerMonthReport(contactList, listingList);

            result.Should().BeEquivalentTo(expectedResult);
        }

        [TestMethod, Description("Calculate Contacted listings per month with inconsistent data")]
        public void InconsistentInputTest()
        {
            List<ListingEntity> listingList = GenerateListingsInputInconsistentTest();
            List<ContactEntity> contactList = GenerateContactsInputInconsistentTest();

            Assert.ThrowsException<Exception>(() => _listingReports.CalculateContactedListingsPerMonthReport(contactList, listingList));
        }

        [TestMethod, Description("Calculate Contacted listings per month with null or empty inputs")]
        public void nullOrEmptyInputTest()
        {
            List<ListingEntity> listingList = null;
            List<ContactEntity> contactList = null;

            var result = _listingReports.CalculateContactedListingsPerMonthReport(contactList, listingList);

            Assert.AreEqual(result, null);
        }
        #region Private Methods
        private ContactedListingsPerMonthReport GenerateExpectedResultOnStandardTest()
        {
            List<ContactedListingList> reportLists = new List<ContactedListingList>();

            var month1Listing = new List<ContactedListing>
            {
                new ContactedListing { ListingId = 1001, Make = "Audi", MileageDisplay = "52000 KM", Ranking = 1, SellingPriceDisplay = "€ 15000,-", TotalContactsAmount = 5000 },
                new ContactedListing { ListingId = 1006, Make = "Opel", MileageDisplay = "2890 KM", Ranking = 2, SellingPriceDisplay = "€ 40000,-", TotalContactsAmount = 3000 },
                new ContactedListing { ListingId = 1002, Make = "Toyota", MileageDisplay = "32000 KM", Ranking = 3, SellingPriceDisplay = "€ 35000,-", TotalContactsAmount = 2000 },
                new ContactedListing { ListingId = 1000, Make = "BMW", MileageDisplay = "200 KM", Ranking = 4, SellingPriceDisplay = "€ 20000,-", TotalContactsAmount = 1000 },
                new ContactedListing { ListingId = 1005, Make = "Porsche", MileageDisplay = "7000 KM", Ranking = 5, SellingPriceDisplay = "€ 90000,-", TotalContactsAmount = 1000 }
            };
            ContactedListingList month1 = new ContactedListingList
            {
                MonthDisplay = "1.2020",
                Listings = month1Listing
            };
            reportLists.Add(month1);

            var month2Listing = new List<ContactedListing>
            {
                new ContactedListing { ListingId = 1004, Make = "VW", MileageDisplay = "2040 KM", Ranking = 1, SellingPriceDisplay = "€ 50000,-", TotalContactsAmount = 9000 },
                new ContactedListing { ListingId = 1000, Make = "BMW", MileageDisplay = "200 KM", Ranking = 2, SellingPriceDisplay = "€ 20000,-", TotalContactsAmount = 4000 },
                new ContactedListing { ListingId = 1001, Make = "Audi", MileageDisplay = "52000 KM", Ranking = 3, SellingPriceDisplay = "€ 15000,-", TotalContactsAmount = 2000 },
                new ContactedListing { ListingId = 1006, Make = "Opel", MileageDisplay = "2890 KM", Ranking = 4, SellingPriceDisplay = "€ 40000,-", TotalContactsAmount = 2000 },
                new ContactedListing { ListingId = 1002, Make = "Toyota", MileageDisplay = "32000 KM", Ranking = 5, SellingPriceDisplay = "€ 35000,-", TotalContactsAmount = 1000 }
            };
            var month2 = new ContactedListingList
            {
                MonthDisplay = "2.2020",
                Listings = month2Listing
            };
            reportLists.Add(month2);


            var month3Listing = new List<ContactedListing>
            {
                new ContactedListing { ListingId = 1002, Make = "Toyota", MileageDisplay = "32000 KM", Ranking = 1, SellingPriceDisplay = "€ 35000,-", TotalContactsAmount = 7000 },
                new ContactedListing { ListingId = 1005, Make = "Porsche", MileageDisplay = "7000 KM", Ranking = 2, SellingPriceDisplay = "€ 90000,-", TotalContactsAmount = 3000 },
                new ContactedListing { ListingId = 1000, Make = "BMW", MileageDisplay = "200 KM", Ranking = 3, SellingPriceDisplay = "€ 20000,-", TotalContactsAmount = 2000 },
                new ContactedListing { ListingId = 1006, Make = "Opel", MileageDisplay = "2890 KM", Ranking = 4, SellingPriceDisplay = "€ 40000,-", TotalContactsAmount = 2000 },
                new ContactedListing { ListingId = 1001, Make = "Audi", MileageDisplay = "52000 KM", Ranking = 5, SellingPriceDisplay = "€ 15000,-", TotalContactsAmount = 1000 }
            };
            var month3 = new ContactedListingList
            {
                MonthDisplay = "3.2020",
                Listings = month3Listing
            };
            reportLists.Add(month3);

            return new ContactedListingsPerMonthReport
            {
                ListingLists = reportLists
            };
        }

        private List<ContactEntity> GenerateContactsInputStandardTest()
        {
            List<ContactEntity> contacts = new List<ContactEntity>();

            for (int i = 0; i < 1000; i++)
            {
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 3, 1) });
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 3, 1) });

                contacts.Add(new ContactEntity { ListingId = 1001, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1001, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1001, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1001, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1001, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1001, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1001, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1001, ContactDate = new DateTime(2020, 3, 1) });

                contacts.Add(new ContactEntity { ListingId = 1002, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1002, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1002, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1002, ContactDate = new DateTime(2020, 3, 1) });
                contacts.Add(new ContactEntity { ListingId = 1002, ContactDate = new DateTime(2020, 3, 1) });
                contacts.Add(new ContactEntity { ListingId = 1002, ContactDate = new DateTime(2020, 3, 1) });
                contacts.Add(new ContactEntity { ListingId = 1002, ContactDate = new DateTime(2020, 3, 1) });
                contacts.Add(new ContactEntity { ListingId = 1002, ContactDate = new DateTime(2020, 3, 1) });
                contacts.Add(new ContactEntity { ListingId = 1002, ContactDate = new DateTime(2020, 3, 1) });
                contacts.Add(new ContactEntity { ListingId = 1002, ContactDate = new DateTime(2020, 3, 1) });

                contacts.Add(new ContactEntity { ListingId = 1004, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1004, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1004, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1004, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1004, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1004, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1004, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1004, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1004, ContactDate = new DateTime(2020, 2, 1) });

                contacts.Add(new ContactEntity { ListingId = 1005, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1005, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1005, ContactDate = new DateTime(2020, 3, 1) });
                contacts.Add(new ContactEntity { ListingId = 1005, ContactDate = new DateTime(2020, 3, 1) });
                contacts.Add(new ContactEntity { ListingId = 1005, ContactDate = new DateTime(2020, 3, 1) });

                contacts.Add(new ContactEntity { ListingId = 1006, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1006, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1006, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1006, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1006, ContactDate = new DateTime(2020, 2, 1) });
                contacts.Add(new ContactEntity { ListingId = 1006, ContactDate = new DateTime(2020, 3, 1) });
                contacts.Add(new ContactEntity { ListingId = 1006, ContactDate = new DateTime(2020, 3, 1) });
            }

            return contacts;
        }

        private List<ListingEntity> GenerateListingsInputStandardTest()
        {
            List<ListingEntity> listingList = new List<ListingEntity>();

            listingList.Add(new ListingEntity { Id = 1000, Make = "BMW", Mileage = 200, Price = 20000, SellerType = "private" });
            listingList.Add(new ListingEntity { Id = 1001, Make = "Audi", Mileage = 52000, Price = 15000, SellerType = "private" });
            listingList.Add(new ListingEntity { Id = 1002, Make = "Toyota", Mileage = 32000, Price = 35000, SellerType = "dealer" });
            listingList.Add(new ListingEntity { Id = 1004, Make = "VW", Mileage = 2040, Price = 50000, SellerType = "other" });
            listingList.Add(new ListingEntity { Id = 1005, Make = "Porsche", Mileage = 7000, Price = 90000, SellerType = "private" });
            listingList.Add(new ListingEntity { Id = 1006, Make = "Opel", Mileage = 2890, Price = 40000, SellerType = "private" });

            return listingList;
        }

        private List<ContactEntity> GenerateContactsInputInconsistentTest()
        {
            List<ContactEntity> contacts = new List<ContactEntity>();

            for (int i = 0; i < 1000; i++)
            {
                contacts.Add(new ContactEntity { ListingId = 1000, ContactDate = new DateTime(2020, 1, 1) });

                contacts.Add(new ContactEntity { ListingId = 1001, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1001, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1001, ContactDate = new DateTime(2020, 1, 1) });

                contacts.Add(new ContactEntity { ListingId = 1002, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1002, ContactDate = new DateTime(2020, 1, 1) });

                contacts.Add(new ContactEntity { ListingId = 1004, ContactDate = new DateTime(2020, 2, 1) });

                contacts.Add(new ContactEntity { ListingId = 1005, ContactDate = new DateTime(2020, 1, 1) });

                contacts.Add(new ContactEntity { ListingId = 1006, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1006, ContactDate = new DateTime(2020, 1, 1) });
                contacts.Add(new ContactEntity { ListingId = 1006, ContactDate = new DateTime(2020, 1, 1) });

            }
            return contacts;
        }

        private List<ListingEntity> GenerateListingsInputInconsistentTest()
        {
            List<ListingEntity> listingList = new List<ListingEntity>();

            listingList.Add(new ListingEntity { Id = 1000, Make = "BMW", Mileage = 200, Price = 20000, SellerType = "private" });
            listingList.Add(new ListingEntity { Id = 1002, Make = "Toyota", Mileage = 32000, Price = 35000, SellerType = "dealer" });
            listingList.Add(new ListingEntity { Id = 1004, Make = "VW", Mileage = 2040, Price = 50000, SellerType = "other" });
            listingList.Add(new ListingEntity { Id = 1005, Make = "Porsche", Mileage = 7000, Price = 90000, SellerType = "private" });
            listingList.Add(new ListingEntity { Id = 1006, Make = "Opel", Mileage = 2890, Price = 40000, SellerType = "private" });

            return listingList;
        }
        #endregion
    }
}
