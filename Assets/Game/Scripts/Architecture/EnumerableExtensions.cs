using System.Collections.Generic;
using System.Linq;

namespace Architecture {
  public static class EnumerableExtensions {
    public static IEnumerable<T> MyNotNull<T>(this IEnumerable<T?> enumerable) where T : class {
      return enumerable.Where(i => i is not null).OfType<T>();
    }
  }
}
