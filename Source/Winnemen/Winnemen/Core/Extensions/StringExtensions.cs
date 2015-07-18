using System;
using System.Text;

namespace Winnemen.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Ifs the null to empty string.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns>System.String.</returns>
        public static string IfNullToEmptyString(this string val, bool addSpacing = false)
        {
            if (string.IsNullOrEmpty(val))
            {
                return string.Empty;
            }

            if (addSpacing)
            {
                return string.Format(" {0} ", val);
            }

            return val;
        }

        /// <summary>
        /// Adds the spacing.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns>System.String.</returns>
        public static string AddSpacing(this string val)
        {
            if (!string.IsNullOrEmpty(val))
            {
                return string.Format(" {0} ", val);
            }

            return val;
        }

        /// <summary>
        /// Trailings the space.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns>System.String.</returns>
        public static string TrailingSpace(this string val)
        {
            if (!string.IsNullOrEmpty(val))
            {
                return string.Format("{0} ", val);
            }

            return val;
        }

        /// <summary>
        /// Returns the right portion of the string for the specified length
        /// </summary>
        public static string Right(this string @string, int length)
        {
            if (length <= 0 || @string.Length == 0)
            {
                return string.Empty;
            }
            if (@string.Length <= length)
            {
                return @string;
            }
            return @string.Substring(@string.Length - length, length);
        }

        /// <summary>
        /// Returns the left portion of the string for the specified length
        /// </summary>
        public static string Left(this string @string, int length)
        {
            if (length <= 0 || @string.Length == 0)
            {
                return string.Empty;
            }
            if (@string.Length <= length)
            {
                return @string;
            }
            return @string.Substring(0, length);
        }

        /// <summary>
        /// Converts to base64 string.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <returns></returns>
        public static string ConvertToBase64(this string val)
        {
            byte[] bytes = Encoding.Default.GetBytes(val);
            string basedString = Convert.ToBase64String(bytes);

            return basedString;
        }

        /// <summary>
        /// Froms the base64 string.
        /// </summary>
        /// <param name="val">The val.</param>
        /// <returns></returns>
        public static string FromBase64String(this string val)
        {
            byte[] bytes = Convert.FromBase64String(val);
            string cleanString = Encoding.Default.GetString(bytes);

            return cleanString;
        }

        public static void IsNullOrEmpty(this string val, Action action)
        {
            if (string.IsNullOrEmpty(val))
            {
                action();
            }
        }

        public static void IsNotNullOrEmpty(this string val, Action action)
        {
            if (!string.IsNullOrEmpty(val))
            {
                action();
            }
        }

        public static string SpiltOnCaptialLetters(this string s)
        {
            var builder = new StringBuilder();
            foreach (var c in s)
            {
                if (char.IsUpper(c) && builder.Length > 0)
                {
                    builder.Append(' ');
                }

                builder.Append(c);
            }
            
            return builder.ToString();
        }
    }

}
