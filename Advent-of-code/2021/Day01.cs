using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day01
    {
        public static int Part1(string inputs)
        {
            return inputs.Split(Environment.NewLine).Select(x => Convert.ToInt32(x)).Pairwise((a,b) => b > a).Count(x => x);
        }

        public static int Part2(string inputs)
        {
            return inputs.Split(Environment.NewLine).Select(x => Convert.ToInt32(x)).Window(3).Select(x => x.Sum()).Pairwise((a, b) => b > a).Count(x=>x);
        }
    }
}
