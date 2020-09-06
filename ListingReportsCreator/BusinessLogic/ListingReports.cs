using ListingReportsCreator.BusinessLogic.Entities.Reports;
using ListingReportsCreator.BusinessLogic.Extensions;
using ListingReportsCreator.BusinessLogic.Keys;
using ListingReportsCreator.BusinessLogic.Services;
using ListingReportsCreator.Infrastructure.CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ListingReportsCreator.BusinessLogic
{
    public class ListingReports : IListingReports
    {
        private readonly ICsvHelper _csvHelper;

        public ListingReports(ICsvHelper csvHelper)
        {
            _csvHelper = csvHelper;
        }

        public ListingReport CalculateAutoScoutListingReport()
        {
            List<Entities.ContactEntity> contacts = _csvHelper.CsvToContactList();
            List<Entities.ListingEntity> listings = _csvHelper.CsvToListingList();

            return new ListingReport
            {
                AveragePriceReport = this.CalculateAveragePriceReport(30, contacts, listings),
                AvailableCarsPercentualDistributionReport = this.CalculateAvailableCarsPercentualDistributionReport(listings),
                AverageListingSellingPriceReport = this.CalculateAverageListingSellingPriceReport(listings),
                ContactedListingsPerMonthReport = this.CalculateContactedListingsPerMonthReport(contacts, listings),
            };
        }

        public AveragePriceReport CalculateAveragePriceReport(int percentage, List<Entities.ContactEntity> contacts, List<Entities.ListingEntity> listings)
        {
            if (percentage <= 0) return null;
            if (listings == null || !listings.Any()) return null;
            if (contacts == null || !contacts.Any()) return null;

            IEnumerable<IGrouping<long, Entities.ContactEntity>> contactsPerListing = contacts.GroupBy(x => x.ListingId).OrderByDescending(y => y.Count());

            var listingsNumberToTake = Math.Round((double)(contactsPerListing.Count() * percentage / 100)) >= 1 ? (int)Math.Round((double)(contactsPerListing.Count() * percentage / 100)) : 1;

            contactsPerListing = contactsPerListing.Take(listingsNumberToTake);

            List<long> listingIds = contactsPerListing.Select(x => x.Key).ToList();

            try
            {
                List<double> prices = listingIds.Select(x => listings.FirstOrDefault(y => y.Id == x).Price).ToList();

                return new AveragePriceReport
                {
                    AveragePriceDisplay = prices.Average().PriceFormat(),
                    MostContactedListingsPercentageDisplay = percentage.PercentageFormat()
                };
            }
            catch (NullReferenceException e)
            {
                throw new Exception($"CalculateAveragePriceReport. This listing id doesn't exist on listings. Check the files listings.csv and contacts.csv", e);
            }
        }

        public AverageListingSellingPriceReport CalculateAverageListingSellingPriceReport(List<Entities.ListingEntity> listings)
        {
            if (listings == null || !listings.Any())
                return GenerateEmptyAverageListingSellingPriceReport();

            AverageListingSellingPriceReport result = new AverageListingSellingPriceReport
            {
                AverageListingSellingPriceList = listings.GroupBy(x => x.SellerType).Select(y =>
                            new AverageListingSellingPrice { SellerType = y.Key, AverageAmountDisplay = y.Average(z => z.Price).PriceFormat() }).ToList()
            };

            if (result.AverageListingSellingPriceList.Count() < 3)
                CompleteAverageListingSellingPriceReport(result);

            return result;
        }

        public AvailableCarsPercentualDistributionReport CalculateAvailableCarsPercentualDistributionReport(List<Entities.ListingEntity> listings)
        {
            if (listings == null || !listings.Any())
                return null;

            return new AvailableCarsPercentualDistributionReport
            {
                AvailableCarsPercentualDistributionList = listings.GroupBy(x => x.Make).OrderByDescending(y => y.Count()).Select(z =>
                            new AvailableCarsPercentualDistribution { Make = z.Key, Distribution = ((double)z.Count() / listings.Count() * 100).PercentageFormat() }).ToList()
            };
        }

        public ContactedListingsPerMonthReport CalculateContactedListingsPerMonthReport(List<Entities.ContactEntity> contacts, List<Entities.ListingEntity> listings)
        {
            if (listings == null || !listings.Any()) return null;
            if (contacts == null || !contacts.Any()) return null;

            var contactsPerMonth = contacts.GroupBy(x => new { x.ContactDate.Month, x.ContactDate.Year }).OrderBy(y => y.Key.Year).ThenBy(z => z.Key.Month);

            List<ContactedListingList> contactedListingListPerMonthAux = new List<ContactedListingList>();
            List<ContactedListing> contactedListingListAux = new List<ContactedListing>();

            foreach (var monthContacts in contactsPerMonth)
            {
                List<IGrouping<long, Entities.ContactEntity>> monthContactsPerListing = monthContacts.GroupBy(x => x.ListingId).OrderByDescending(y => y.Count()).Take(5).ToList();

                int ranking = 1;
                foreach (var listingContact in monthContactsPerListing)
                {
                    try
                    {
                        var listing = listings.FirstOrDefault(x => x.Id == listingContact.Key);

                        contactedListingListAux.Add(new ContactedListing
                        {
                            ListingId = listing.Id,
                            Make = listing.Make,
                            MileageDisplay = listing.Mileage.MileageFormat(),
                            Ranking = ranking,
                            SellingPriceDisplay = listing.Price.PriceFormat(),
                            TotalContactsAmount = listingContact.Count()
                        });
                        ranking++;
                    }
                    catch (NullReferenceException e)
                    {
                        throw new Exception($"CalculateContactedListingsPerMonthReport. This listing id doesn't exist on listings. Check the files listings.csv and contacts.csv", e);
                    }
                }
                ranking = 1;

                contactedListingListPerMonthAux.Add(new ContactedListingList { Listings = contactedListingListAux.ToList(), MonthDisplay = $"{monthContacts.Key.Month}.{monthContacts.Key.Year}" });
                contactedListingListAux.Clear();
            }

            return new ContactedListingsPerMonthReport { ListingLists = contactedListingListPerMonthAux };
        }

        #region Private Methods
        private static void CompleteAverageListingSellingPriceReport(AverageListingSellingPriceReport result)
        {
            if (!result.AverageListingSellingPriceList.Exists(x => x.SellerType == SellerTypeCodes.Private))
            {
                result.AverageListingSellingPriceList.Add(new AverageListingSellingPrice { SellerType = SellerTypeCodes.Private, AverageAmountDisplay = 0.PriceFormat() });
            }
            else if (!result.AverageListingSellingPriceList.Exists(x => x.SellerType == SellerTypeCodes.Dealer))
            {
                result.AverageListingSellingPriceList.Add(new AverageListingSellingPrice { SellerType = SellerTypeCodes.Dealer, AverageAmountDisplay = 0.PriceFormat() });
            }
            else if (!result.AverageListingSellingPriceList.Exists(x => x.SellerType == SellerTypeCodes.Other))
            {
                result.AverageListingSellingPriceList.Add(new AverageListingSellingPrice { SellerType = SellerTypeCodes.Other, AverageAmountDisplay = 0.PriceFormat() });
            }
        }

        private static AverageListingSellingPriceReport GenerateEmptyAverageListingSellingPriceReport()
        {
            var resultList = new List<AverageListingSellingPrice>();
            resultList.Add(new AverageListingSellingPrice { SellerType = SellerTypeCodes.Private, AverageAmountDisplay = 0.PriceFormat() });
            resultList.Add(new AverageListingSellingPrice { SellerType = SellerTypeCodes.Dealer, AverageAmountDisplay = 0.PriceFormat() });
            resultList.Add(new AverageListingSellingPrice { SellerType = SellerTypeCodes.Other, AverageAmountDisplay = 0.PriceFormat() });
            return new AverageListingSellingPriceReport { AverageListingSellingPriceList = resultList };
        }
        #endregion
    }
}
