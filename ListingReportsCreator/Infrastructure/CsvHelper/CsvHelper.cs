using ListingReportsCreator.BusinessLogic.Entities;
using ListingReportsCreator.BusinessLogic.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ListingReportsCreator.Infrastructure.CsvHelper
{
    public class CsvHelper: ICsvHelper
    {
        public List<ContactEntity> CsvToContactList()
        {
            List<ContactEntity> contacts = new List<ContactEntity>();
            var path = String.Concat(Environment.CurrentDirectory,"\\contacts.csv");
            string[] lines = File.ReadAllLines(path);

            if (lines == null || !lines.Any())
                return null;

            lines = lines.Skip(1).ToArray();
            string[] aux;

            try
            {
                foreach (string line in lines)
                {
                    aux = line.Split(',');

                    contacts.Add(new ContactEntity
                    {
                        ListingId = long.Parse(aux[0]),
                        ContactDate = aux[1].DateFormat()
                    });
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error trying to convert csv file to contact entity list => Message: {e}");
            }
            return contacts;
        }

        public List<ListingEntity> CsvToListingList()
        {
            List<ListingEntity> listings = new List<ListingEntity>();
            var path = String.Concat(Environment.CurrentDirectory, "\\listings.csv");
            string[] lines = File.ReadAllLines(path);

            if (lines == null || !lines.Any())
                return null;

            lines = lines.Skip(1).ToArray();
            string[] aux;

            try
            {
                foreach (string line in lines)
                {
                    aux = line.Split(',');
                    listings.Add(new ListingEntity
                    {
                        Id = long.Parse(aux[0]),
                        Make = aux[1],
                        Price = int.Parse(aux[2]),
                        Mileage = int.Parse(aux[3]),
                        SellerType = aux[4]
                    });
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error trying to convert csv file to listing entity list => Message: {e}");
            }
            return listings;
        }
    }
}
