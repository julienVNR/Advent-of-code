using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code._2021
{
    public static class Day05
    {

        private static int x1(string[] a) => int.Parse(a[0].Split(",")[0]);
        private static int y1(string[] a) => int.Parse(a[0].Split(",")[1]);
        private static int x2(string[] a) => int.Parse(a[1].Split(",")[0]);
        private static int y2(string[] a) => int.Parse(a[1].Split(",")[1]);

        public static string Part1(string input)
        {
            return Compute(input);
        }

        public static string Part2(string input)
        {
            return Compute(input, true);
        }
        static string Compute(string input, bool diagonals = false)
        {
            return input
            .Split(Environment.NewLine)
            .Where(s => !string.IsNullOrEmpty(s))
            .Select(s => s.Split(" -> "))
            .Select(a => (x1(a), y1(a), x2(a), y2(a)))
            .Where(t => diagonals || t.Item1 == t.Item3 || t.Item2 == t.Item4)
            .SelectMany(t => Enumerable
                .Range(0, Math.Max(Math.Abs((int)(t.Item1 - t.Item3)), Math.Abs((int)(t.Item2 - t.Item4))) + 1)
                .Select(i => (
                    t.Item1 > t.Item3 ? t.Item3 + i : t.Item1 < t.Item3 ? t.Item3 - i : t.Item3,
                    t.Item2 > t.Item4 ? t.Item4 + i : t.Item2 < t.Item4 ? t.Item4 - i : t.Item4)))
            .GroupBy(coo => coo)
            .Count(coo => coo.Count() >= 2)
            .ToString();
        }


    }
}
