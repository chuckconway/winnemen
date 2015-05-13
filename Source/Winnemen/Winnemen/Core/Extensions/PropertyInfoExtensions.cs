using System;
using System.Linq;
using System.Reflection;

namespace Winnemen.Core.Extensions
{
    public static class PropertyInfoExtensions
    {
        /// <summary> A PropertyDescriptor extension method that gets an attribute. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="property"> The information. </param>
        /// <returns> The attribute&lt; t&gt; </returns>
        public static T GetAttribute<T>(this PropertyInfo property) where T : Attribute
        {
            var ofType = property.GetCustomAttributes().OfType<T>().FirstOrDefault();
            return ofType;
        }

    }
}
