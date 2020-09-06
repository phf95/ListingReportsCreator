using System;
using System.Collections.Generic;
using System.Text;

namespace ListingReportsCreator.BusinessLogic.Entities
{
    public class ListingEntity
    {
        public long Id { get; set; }
        public string Make { get; set; }
        public double Price { get; set; }
        public int Mileage { get; set; }
        public string SellerType { get; set; }
    }
}
