using System;

namespace Winnemen.Core.Extensions
{
    public static class BusinessExtensions
    {
        public static bool GreaterThanZero(this int value)
        {
            return value.GreaterThan(0);
        }

        public static bool GreaterThanOrEqualToZero(this int integer)
        {
            return integer.GreaterThanOrEqual(0);
        }

        public static bool GreaterThan(this int integer, int value)
        {
            return integer > value;
        }

        public static bool GreaterThanOrEqual(this int integer, int value)
        {
            return integer >= value;
        }

        public static bool LessThanZero(this int value)
        {
            return value.LessThan(0);
        }

        public static bool LessThanOrEqualToZero(this int integer)
        {
            return integer.LessThanOrEqual(0);
        }

        public static bool LessThan(this int integer, int value)
        {
            return integer > value;
        }

        public static bool LessThanOrEqual(this int integer, int value)
        {
            return integer >= value;
        }

        /// <summary>
        /// Determines whether the specified value is null.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns><c>true</c> if the specified value is null; otherwise, <c>false</c>.</returns>
        public static bool IsNull(this object val)
        {
            return val == null;
        }

        /// <summary>
        /// Determines whether [is not null] [the specified value]. 
        /// If it is not null, then the Func is executed.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns><c>true</c> if [is not null] [the specified value]; otherwise, <c>false</c>.</returns>
        public static T IsNull<T>(this T val, Func<T> result) where T : class
        {
            if (val == null)
            {
                return result();
            }

            return val;
        }

        public static void IsNull<T>(this T val, Action result) where T : class
        {
            if (val == null)
            {
                result();
            }

            //return val;
        }

        /// <summary>
        /// Determines whether [is not null] [the specified value].
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns><c>true</c> if [is not null] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsNotNull(this object val)
        {
            return val != null;
        }

        /// <summary>
        /// Determines whether [is not null] [the specified value]. 
        /// If it is not null, then the Func is executed.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns><c>true</c> if [is not null] [the specified value]; otherwise, <c>false</c>.</returns>
        public static T IsNotNull<T>(this T val, Func<T> result) where T : class
        {
            if (val != null)
            {
                return result();
            }

            return null; // is always null.
        }

        public static string ToMoney(this decimal o)
        {
            return o.ToString(@"$#,##0.00;$\(#,##0.00\)");
        }

        /// <summary>
        /// Determines whether [is not null] [the specified value]. 
        /// If it is not null, then the Func is executed.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns><c>true</c> if [is not null] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsNotNull<T>(this T val, Action result) where T : class
        {
            if (val != null)
            {
                result();
                return true;
            }

            return false; // is always null.
        }
    }
}
