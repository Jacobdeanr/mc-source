using System.Collections.Generic;

namespace McSource.Common.Extensions
{
  // ReSharper disable once InconsistentNaming
  public static class IDictionaryExtensions
  {
    public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key)
    {
#pragma warning disable 8603
      
      return self.TryGetValue(key, out var result)
        ? result
        : default;

#pragma warning restore 8603
    }

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