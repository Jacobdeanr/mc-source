using System.Collections;
using System.Collections.Generic;

namespace McSource.Extensions
{
  public static class ICollectionExtensions
  {
    public static void AddRange<T>(this ICollection<T> self, IEnumerable<T> enumerable)
    {
      foreach (var e in enumerable)
      {
        self.Add(e);
      }
    }
  }
}