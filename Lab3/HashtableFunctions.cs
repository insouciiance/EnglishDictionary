using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public static class HashtableFunctions
    {
        public static int SumFunction<T>(T o)
        {
            string hash = o.ToString().ToLowerInvariant();

            long sum = 0;
            long mul = 1;
            for (int i = 0; i < hash.Length; i++)
            {
                mul = (i % 4 == 0) ? 1 : mul * 256;
                sum += hash[i] * mul;
            }

            return (int)sum;

        }
    }
}
