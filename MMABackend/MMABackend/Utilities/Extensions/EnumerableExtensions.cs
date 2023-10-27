using System.Collections.Generic;
using System.Linq;

namespace MMABackend.Utilities.Extensions
{
    public static partial class EnumerableExtensions
    {
        public static bool NotContains<T>(this IEnumerable<T> collection, T target) => !collection.Contains(target);
    }
}