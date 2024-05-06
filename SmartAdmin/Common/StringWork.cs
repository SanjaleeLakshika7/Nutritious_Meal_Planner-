using System;
using System.Collections.Generic;

namespace Common
{
    public static class StringWork
    {
        public static string GetSimpleDT(this DateTime instance)
        {
            string returnString = instance.Year.ToString();
            returnString += "-" + instance.Month.ToString().PadLeft(2, '0');
            returnString += "-" + instance.Day.ToString().PadLeft(2, '0');

            returnString += " " + instance.Hour.ToString().PadLeft(2, '0');
            returnString += ":" + instance.Minute.ToString().PadLeft(2, '0');
            returnString += ":" + instance.Second.ToString().PadLeft(2, '0');

            return returnString;
        }

        public static string ToCurrency(this double instance)
        {
            string returnString = instance > 0 ? instance.ToString("N") : "-";

            return returnString;
        }

        public static string ToShortCurrency(this double instance)
        {
            string returnString = string.Format("{0:#,0.####}", instance);

            return returnString;
        }

        public static string ToBlank(this string instance)
        {
            return string.IsNullOrEmpty(instance) ? "" : instance;
        }

        public static string ToCommaString(this List<string> instance)
        {
            return string.Join(",", instance);
        }

        public static string ToShort(this string instance, int MaxSize)
        {
            var len = instance.Length;
            if(len > MaxSize)
            {
                return instance.Substring(0, MaxSize) + "...";
            }
            else
            {
                return instance;
            }
        }

    }
}
