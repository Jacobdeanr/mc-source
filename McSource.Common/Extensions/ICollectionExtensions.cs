using System.Collections.Generic;

namespace McSource.Common.Extensions
{
  // ReSharper disable once InconsistentNaming
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