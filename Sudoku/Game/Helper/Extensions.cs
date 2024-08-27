using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Game.Helper
{
    public static class Extensions
    {
        public static IList<T> AsShuffled<T>(this IList<T> list)
        {
            var newList = new List<T>(list);

            for (int i = newList.Count() - 1; i > 0; i--)
            {
                var j = RandomNumberGenerator.GetInt32(0, i);

                T temp = newList[i];
                newList[i] = newList[j];
                newList[j] = temp;
            }

            return newList;
        }

        public static IEnumerable<int> AllIndexesOf(this string str, string searchstring)
        {
            int minIndex = str.IndexOf(searchstring);
            while (minIndex != -1)
            {
                yield return minIndex;
                minIndex = str.IndexOf(searchstring, minIndex + searchstring.Length);
            }
        }

        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }
    }
}
