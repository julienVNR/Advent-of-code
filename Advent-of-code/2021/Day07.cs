using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day07
    {
        public static int Part1(string input)
        {
            int[] crabs = input.Split(',').Select(x => int.Parse(x)).ToArray();
            Array.Sort(crabs);

            int size = crabs.Length;
            int mid = size / 2;
            int median = (size % 2 != 0) ? crabs[mid] : (crabs[mid] + crabs[mid - 1]) / 2;
            return crabs.Sum(x => Math.Abs(x - median));
        }

        public static int Part2(string input)
        {
            int[] crabs = input.Split(',').Select(x => int.Parse(x)).ToArray();
            int moyenne = (int)Math.Round(crabs.Average());
            int minValue = crabs.Select(y => Enumerable.Range(1, Math.Abs(moyenne - y)).Sum()).Sum();
            int nextValue = crabs.Select(y => Enumerable.Range(1, Math.Abs(moyenne + 1 - y)).Sum()).Sum();
            int inc = 1;
            if (nextValue > minValue)
            {
                inc *= -1;
                nextValue = minValue;
            }
            else
                moyenne++;
            do
            {
                minValue = nextValue;
                moyenne += inc;
                nextValue = crabs.Select(y => Enumerable.Range(1, Math.Abs(moyenne - y)).Sum()).Sum();
            }
            while (minValue > nextValue);

            return minValue;
        }
    }
}
