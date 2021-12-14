using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day14
    {
        public static Dictionary<string, char> Transformation(string input) => input.Split(Environment.NewLine).Skip(2).Select(x => x.Split(" -> ")).ToDictionary(x => x[0], x => x[1][0]);

        public static long Part1(string input)
        {
            var transformations = Transformation(input);
            var start = input.Split(Environment.NewLine)[0];
            for(int i = 0; i < 10; i++)
            {
                var step = start.Pairwise((a, b) => string.Format("{0}{1}", a, transformations[""+a+b]));
                start = String.Join("",step)+start.Last();
            }

            var t = from molecule in start
                    group molecule by molecule into grp
                    select  grp.Count();
            return t.Max() - t.Min();
        }

        public static long Part2(string input)
        {
            var transformations = Transformation(input);
            var start = input.Split(Environment.NewLine)[0];

            //Init dictionnaries of pair and
            var polymer = start.Window(2).Select(x=>x[0]+""+x[1]).GroupBy(x => x).ToDictionary(x => x.Key, x => (long)x.Count());
            var molecules = start.GroupBy(x => x).ToDictionary(x => x.Key, x => (long)x.Count());

            for (int i = 0; i < 40; i++)
            {
                Dictionary<string, long> newPolymer = new();
                foreach (var currentPair in polymer.Keys.ToList())
                {
                    var currentPairCount = polymer[currentPair];
                    var insert = transformations[currentPair];

                    var leftKey = currentPair[0] + insert.ToString();
                    var rightKey = insert.ToString() + currentPair[1];

                    newPolymer[leftKey] = newPolymer.GetValueOrDefault(leftKey, 0) + currentPairCount;
                    newPolymer[rightKey] = newPolymer.GetValueOrDefault(rightKey, 0) + currentPairCount;
                    molecules[insert] = molecules.GetValueOrDefault(insert,0) + currentPairCount;
                }
                polymer = newPolymer.ToDictionary(x => x.Key, x => x.Value);
            }
            return molecules.Max(x => x.Value) - molecules.Min(x => x.Value);
        }
    }
}
