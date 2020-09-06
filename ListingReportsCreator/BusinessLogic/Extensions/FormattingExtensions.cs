using System;
using System.Collections.Generic;
using System.Text;

namespace ListingReportsCreator.BusinessLogic.Extensions
{
    public static class FormattingExtensions
    {
       public static string PriceFormat(this double priceAmout)
        {
            double decimals = Math.Round(priceAmout - Math.Truncate(priceAmout),2);
            return $"€ {(int)priceAmout},{(decimals > 0 ? ((int)(decimals * 100)).ToString() : "-")}";
        }

        public static string PriceFormat(this int priceAmout)
        {
            return $"€ {priceAmout},-";
        }

        public static string MileageFormat(this int mileageAmout)
        {
            return $"{mileageAmout} KM";
        }

        public static string PercentageFormat(this int amount)
        {
            return $"{amount}%";
        }

        public static string PercentageFormat(this double amount)
        {
            amount = Math.Round((Double)amount, 2);
            return $"{amount}%";
        }

        public static DateTime DateFormat(this string milliseconds)
        {
            return (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(milliseconds));
        }

        public static string MonthFormat(this DateTime date)
        {
            return $"{date.Month}.{date.Year}";
        }
    }
}
