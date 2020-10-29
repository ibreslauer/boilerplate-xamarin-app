using System;

namespace Boilerplate.Common.Extensions
{
    public static class DecimalExtensions
    {
        public static int GetDecimalPlaces(this decimal dec)
        {
            dec = Math.Abs(dec);
            dec -= (int)dec;
            var decimalPlaces = 0;
            while (dec > 0)
            {
                decimalPlaces++;
                dec *= 10;
                dec -= (int)dec;
            }
            return decimalPlaces;
        }

        public static decimal FloorToDecimalPlaces(this decimal value, double decimals)
        {
            var multiplier = Convert.ToDecimal(Math.Pow(10d, decimals));
            return Math.Floor(value * multiplier) / multiplier;
        }
    }
}
