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

        public static int GetSum(int nb) => (int)(nb * ((double)(nb + 1) / 2));

        public static int Part2(string input)
        {
            int[] crabs = input.Split(',').Select(x => int.Parse(x)).ToArray();
            int moyenne = (int)Math.Round(crabs.Average());
            int minValue = (int)crabs.Select(y => GetSum(Math.Abs(moyenne - y))).Sum();
            int nextValue = (int)crabs.Select(y => GetSum(Math.Abs(moyenne+1 - y))).Sum(); ;
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
                nextValue = crabs.Select(y => GetSum(Math.Abs(moyenne - y))).Sum();
            }
            while (minValue > nextValue);

            return minValue;
        }
    }
}
