using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day12
    {
        public static int Part1(string input) => Compute(input, false);
        public static int Part2(string input) => Compute(input, true);

        static int Compute(string input, bool part2)
        {
            var map = GetMap(input);

            int explore(string currentCave, ImmutableHashSet<string> visitedCaves, bool allVisited)
            {

                if (currentCave == "end")
                    return 1;

                var nbPath = 0;
                foreach (var cave in map[currentCave])
                {
                    var isBigCave = cave.ToUpper() == cave;
                    var seen = visitedCaves.Contains(cave);

                    if (!seen || isBigCave)
                        nbPath += explore(cave, visitedCaves.Add(cave), allVisited);
                    else if (part2 && !isBigCave && cave != "start" && !allVisited)
                        nbPath += explore(cave, visitedCaves, true);
                }
                return nbPath;
            }

            return explore("start", ImmutableHashSet.Create<string>("start"), false);
        }

        static Dictionary<string, string[]> GetMap(string input)
        {
            var paths =
                from line in input.Split(Environment.NewLine)
                let parts = line.Split("-")
                let caveA = parts[0]
                let caveB = parts[1]
                from connection in new[] { (From: caveA, To: caveB), (From: caveB, To: caveA) }
                select connection;

            // grouped by "from":
            return (
                from p in paths
                group p by p.From into g
                select g
            ).ToDictionary(g => g.Key, g => g.Select(path => path.To).ToArray());
        }
    }
}
