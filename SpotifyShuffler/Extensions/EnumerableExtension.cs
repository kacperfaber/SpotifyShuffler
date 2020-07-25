using System;
using System.Collections.Generic;

namespace SpotifyShuffler
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T t in enumerable) action(t);

            return enumerable;
        }
    }
}