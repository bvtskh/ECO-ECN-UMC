using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECO_DX_For_PUR.DATA.Bussiness.Common
{
    public static class Utils
    {
        public static DateTime? ConvertDate(string date)
        {
            DateTime result;
            if (date != null)
            {
                if (DateTime.TryParse(date.ToString(), out result))
                {
                    return result;
                }
            }
            return null;
        }

        public static string ConvertDate(DateTime? dateTime)
        {
            DateTime result;
            if (dateTime != null)
            {
                if (DateTime.TryParse(dateTime.ToString(), out result))
                {
                    return result.Date.ToString("MM/dd/yyyy");
                }
            }
            return null;
        }
        public static DateTime? ConvertDate(DateTime dateTime)
        {
            DateTime result;
            if (dateTime != null)
            {
                if (DateTime.TryParse(dateTime.ToString(), out result))
                {
                    return result;
                }
            }
            return null;
        }
        public static int? ConvertINT(object value)
        {

            int result;
            if (value != null)
            {
                if (int.TryParse(value.ToString(), out result))
                {
                    return result;
                }
            }
            return null;
        }
        public static int? ConvertINT(string value)
        {
            int result;
            if (value != null)
            {
                if (int.TryParse(value.ToString(), out result))
                {
                    return result;
                }
            }
            return null;
        }
        public static double? convertDouble(object value)
        {
            double result;
            if (value != null)
            {
                if (double.TryParse(value.ToString(), out result))
                {
                    return result;
                }
            }
            return null;
        }
        public static string ConvertString(object str)
        {
            if (str != null)
            {
                return str.ToString().Trim();
            }
            return null;
        }

        public static DateTime? ConvertDate(object date)
        {
            DateTime result;
            if (date != null)
            {
                if (DateTime.TryParse(date.ToString(), out result))
                {
                    return result;
                }
            }
            return null;
        }
    }
}
