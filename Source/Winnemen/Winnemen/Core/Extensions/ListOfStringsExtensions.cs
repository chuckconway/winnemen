using System.Collections.Generic;
using System.Linq;

namespace Winnemen.Core.Extensions
{
    public static class ListOfStringsExtensions
    {
        public static string SentencifyList(this List<string> names, string conjunction)
        {
            if (names.Count > 1)
            {
                names[names.Count - 1] = conjunction + " " + names.Last();
            }

            string m = string.Join(", ", names);
            return m;
        }
    }
}
