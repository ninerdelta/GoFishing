using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
  // TODO: (matt) Write some tests to check that this is doing what we want
  public static class RandomProbability
  {
    static int FindCeil(int[] arr, int r, int l, int h)
    {
      int mid;
      while (l < h)
      {
        mid = l + ((h - l) >> 1);
        if (r > arr[mid])
        {
          l = mid + 1;
        }
        else
        {
          h = mid;
        }
      }

      return (arr[l] >= r) ? l : -1;
    }

		// REFERENCE: somewhere on GeeksForGeeks
		// generates an array based on random probabilities
    public static int Rand(int[] arr, int[] freq, int n)
    {
      int[] prefix = new int[n];
      prefix[0] = freq[0];

      for (int i = 1; i < n; ++i)
      {
        prefix[i] = prefix[i - 1] + freq[i];
      }

      int r = ((int)(Random.value * 1000) % prefix[n - 1]) + 1;

      int indexc = FindCeil(prefix, r, 0, n - 1);

      return arr[indexc];
    }
  }
}