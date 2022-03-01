using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eltin_Buchard_Keller_Algorithm
{
    static public class DistanceMetric
    {
        static public int CalculateLeeDistance(int[] source, int[] target)
        {
            if (source.Length != target.Length)
                throw new Exception("Lee distance string comparisons must be of equal length.");

            // Iterate both arrays simultaneously, summing absolute value of difference at each position
            return source
                .Zip(target, (v1, v2) => new { v1, v2 })
                .Sum(m => Math.Abs(m.v1 - m.v2));
        }

        static public int CalculateHammingDistance(byte[] source, byte[] target)
        {
            if (source.Length != target.Length)
                throw new Exception("Hamming distance string comparisons must be of equal length.");

            // Iterate both arrays simultaneously, summing count of bit differences of each byte
            return source.Zip(target, (v1, v2) => new { v1, v2 }).Sum(m =>
            {
                int d = 0;
                int v = m.v1 ^ m.v2; // XOR values to find all dissimilar bits

                // Count number of set bits
                while (v > 0)
                {
                    ++d;
                    v &= (v - 1);
                }
                return d;
            });
        }

        static public int CalculateLevenshteinDistance(string source, string target)
        {
            int[,] distance; // distance matrix
            int n; // length of first string
            int m; // length of second string
            int i; // iterates through first string
            int j; // iterates through second string
            char s_i; // ith character of first string
            char t_j; // jth character of second string
            int cost; // cost

            // Step 1
            n = source.Length;
            m = target.Length;
            if (n == 0)
                return m;
            if (m == 0)
                return n;
            distance = new int[n + 1, m + 1];

            // Step 2
            for (i = 0; i <= n; i++)
                distance[i, 0] = i;
            for (j = 0; j <= m; j++)
                distance[0, j] = j;

            // Step 3
            for (i = 1; i <= n; i++)
            {
                s_i = source[i - 1];

                // Step 4
                for (j = 1; j <= m; j++)
                {
                    t_j = target[j - 1];

                    // Step 5
                    if (s_i == t_j)
                        cost = 0;
                    else
                        cost = 1;

                    // Step 6
                    distance[i, j] =
                        Math.Min(
                            Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                            distance[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return distance[n, m];
        }
    }
}
