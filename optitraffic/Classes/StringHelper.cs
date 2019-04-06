using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace optitraffic.Classes
{
    public class StringHelper
    {
        /// <summary>
        /// Turns the first character of the given string to uppercase.
        /// </summary>
        /// <param name="str">String to operate upon.</param>
        /// <remarks>
        /// Idea taken from https://www.c-sharpcorner.com/blogs/first-letter-in-uppercase-in-c-sharp1/
        /// </remarks>
        /// <returns></returns>
        public static string CapitalizeFirstChar(string str)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;

            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }
    }
}