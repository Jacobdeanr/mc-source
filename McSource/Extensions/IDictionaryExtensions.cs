using System.Collections.Generic;
using System.Reflection;

namespace McSource.Extensions
{
  public static class IDictionaryExtensions
  {
    public static bool TryGetValue<TKey, TValue, T>(this IDictionary<TKey, TValue> self, TKey key, out T value)
    {
      if (self.TryGetValue(key, out var result) && result is T cast)
      {
        value = cast;
        return true;
      }
      
      value = default!;
      return false;
    }
  }
}