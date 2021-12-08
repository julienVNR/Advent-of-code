using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day08
    {
        public static int Part1(string input)
        {
            string[] inputs = input.Split(Environment.NewLine);
            List<string> clock = inputs.SelectMany(x => x.Split(" | ")[1].Split(' ')).ToList();
            int[] known = new int[] { 2, 3, 4, 7 };
            return clock.Count(x => known.Contains(x.Length));
        }

        public static bool Segment(this string input, string search) => search.All(x => input.Contains(x));

        public static string Pick(this List<string> input,Func<string,bool> Predicate)
        {
            var s = input.First(Predicate);
            input.Remove(s);
            return new String(s.OrderBy(x=>x).ToArray());
        }

        public static long Part2(string input)
        {
            List<string> entries = input.Split(Environment.NewLine).ToList();
            int res = 0;
            foreach (string entry in entries)
            {
                List<string> signals = entry.Split(" | ")[0].Split(' ').ToList();
                string[] numbers = new string[10];
                numbers[1] = signals.Pick(x => x.Length == 2);
                numbers[4] = signals.Pick(x => x.Length == 4);
                numbers[7] = signals.Pick(x => x.Length == 3);
                numbers[8] = signals.Pick(x => x.Length == 7);
                numbers[3] = signals.Pick(x => x.Length == 5 && x.Segment(numbers[7]));
                numbers[9] = signals.Pick(x => x.Length == 6 && x.Segment(numbers[3]));
                numbers[6] = signals.Pick(x => x.Length == 6 && !x.Segment(numbers[1]));
                numbers[5] = signals.Pick(x => x.Length == 5 && numbers[6].Segment(x));
                numbers[0] = signals.Pick(x => x.Length == 6);
                numbers[2] = signals.Pick(x=>x.Length == 5);
                string[] clock = entry.Split(" | ")[1].Split(' ');
                
                int i = 1000;
                foreach(string number in clock)
                {
                    res += i * Array.FindIndex(numbers, x => new String(number.OrderBy(y => y).ToArray()) == x);
                    i /= 10;
                }
            }
            return res;
        }
    }
}
