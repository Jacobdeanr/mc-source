using System.Collections.Generic;

namespace McSource.Logging.Extensions
{
  /// <summary>
  /// Extensions for the <see cref="Dictionary{TKey,TValue}"/> type
  /// </summary>
  public static class DictionaryExtensions
  {
    /// <summary>
    /// Values the or default.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">       The key.</param>
    /// <returns></returns>
    public static TValue ValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
    {
#pragma warning disable CS8603
      return dictionary.TryGetValue(key, out var value) ? value : default;
#pragma warning restore CS8603
    }
  }
}