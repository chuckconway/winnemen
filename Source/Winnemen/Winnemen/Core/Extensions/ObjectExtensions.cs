namespace Winnemen.Core.Extensions
{
    public static class ObjectExtensions
    {

        /// <summary>
        /// Determines whether the specified value is primative.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <returns><c>true</c> if the specified value is primative; otherwise, <c>false</c>.</returns>
        public static bool IsPrimitive(this object val)
        {
            return (val is string || !val.GetType().IsClass);
        }
    }
}
