using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day06
    {
        /// <summary>
        /// Naive solution
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int Part1(string input)
        {
            List<int> lanterns = input.Split(',').Select(x => int.Parse(x)).ToList();
            for(int i = 1; i <= 80; i++)
            {
                int birth = lanterns.Count(x => x == 0);
                lanterns.RemoveAll(x => x == 0);
                lanterns = lanterns.Select(x => x - 1).ToList();
                lanterns.AddRange(Enumerable.Range(0, birth).Select(x => 8));
                lanterns.AddRange(Enumerable.Range(0, birth).Select(x => 6));
            }
            return lanterns.Count();
        }

        public static long Part2(string input)
        {
            List<int> lanterns = input.Split(',').Select(x => int.Parse(x)).ToList();
            long babys = lanterns.Count();
            long[] WeekPlanning = new long[7];
            foreach (int lantern in lanterns)
            {
                WeekPlanning[lantern]++;
            }

            var NextWeekPlaning = new long[7];
            for (int d = 0; d < 256; d++)
            {
                int dayOfWeek = d % 7;
                var current = WeekPlanning[dayOfWeek];
                NextWeekPlaning[(dayOfWeek + 2) % 7] += current;
                babys += current;

                //Reset planning with wext week
                WeekPlanning[dayOfWeek] += NextWeekPlaning[dayOfWeek];
                NextWeekPlaning[dayOfWeek] = 0;
            }
            return babys;
        }
    }
}
