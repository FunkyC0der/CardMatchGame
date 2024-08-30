using System;
using System.Collections.Generic;

namespace CardMatchGame
{
  public static class Extensions
  {
    public static void Shuffle<T>(this IList<T> ts)
    {
      int count = ts.Count;
      int last = count - 1;
      
      for (var i = 0; i < last; ++i) {
        int r = UnityEngine.Random.Range(i, count);
        (ts[i], ts[r]) = (ts[r], ts[i]);
      }
    }
    
    public static string ToMinutesAndSeconds(this float time) =>
      TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
  }
}