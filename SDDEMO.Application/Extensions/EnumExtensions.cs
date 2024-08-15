using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Enumlarda bulunan Description stringi döner.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToDescriptionString<T>(this T val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        /// <summary>
        /// Enumlarda bulunan Description stringi döner.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static float ToFloat<T>(this T val)
        {
            if (val.GetType() == typeof(double))
            {
                return float.Parse(val.ToString().Replace('.', ','));
            }
            return 0;
        }

        /// <summary>
        /// Enumlarda bulunan Description stringi döner.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static double To7DecimalPlaces<T>(this T val)
        {
            if (val.GetType() == typeof(double))
            {
                string formattedNumber = Double.Parse(val.ToString()).ToString("F7");
                return Double.Parse(formattedNumber);
            }
            return 0;
        }
    }
}
