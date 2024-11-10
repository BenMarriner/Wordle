using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleEngine
{
    /// <summary>
    /// This class has been created to help with the algorithm in SetWordLetterColours().
    /// .NET's IEnumerable Zip function cannot create tuples with more than three elements.
    /// This class gets around this limitation
    /// </summary>
    public static class ZipExtensions
    {
        public static IEnumerable<(T1 Target, T2 Guess, T3 Discovered, T4 Colour)> ZipFour<T1, T2, T3, T4>
            (
            IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            IEnumerable<T4> fourth
            )
        {
            using (var e1 = first.GetEnumerator())
            using (var e2 = second.GetEnumerator())
            using (var e3 = third.GetEnumerator())
            using (var e4 = fourth.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext())
                {
                    yield return (e1.Current, e2.Current, e3.Current, e4.Current);
                }
            }
        }
    }
}
