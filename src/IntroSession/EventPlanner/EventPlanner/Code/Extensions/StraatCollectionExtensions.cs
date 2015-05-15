using System;
using System.Collections.Generic;
using System.Text;
using EventPlanner.Models;

namespace EventPlanner.Code.Extensions
{
    public static class StraatCollectionExtensions
    {
        public static string ToKommaSeparatedString(this ICollection<Straat> straten)
        {
            if (straten == null)
                return string.Empty;

            var sb = new StringBuilder();

            foreach (var straat in straten)
            {
                sb.Append(string.Format("{0}, ", straat.Straatnaam));
            }

            return sb.ToString().Substring(0, Math.Max(sb.Length - 2, 0));
        }
    }
}