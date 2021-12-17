using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day15
    {
        private static IList<(int x, int y)> _neighbors(int x, int y, int maxX, int maxY)
        {
            List<(int x, int y)> neighours = new();
            if (x + 1 < maxX) neighours.Add((x + 1, y));
            if (y + 1 < maxY) neighours.Add((x, y + 1));
            if (y > 0) neighours.Add((x, y - 1));
            if (x > 0) neighours.Add((x -1 , y));
            return neighours;
        }
        public static int Part1(string input)
        {
            int[][] matrix = input.Split(Environment.NewLine).Select(x => x.Select(y => (int)Char.GetNumericValue(y)).ToArray()).ToArray();
            return Dijkstra(matrix,(matrix.Length - 1, matrix[0].Length - 1));
        }

        public static int Dijkstra(int[][] matrix,(int,int) to)
        {
            Dictionary<(int, int), (int, List<(int, int)>)> Distances = new();
            List<(int, int)> Done = new();
            Dictionary<(int, int), int> Parcours = new();
            Parcours.Add((0, 0), 0);
            Distances.Add((0, 0), (0, new List<(int, int)>() { (0, 0) }));
            while (Parcours.Count > 0)
            {
                var currentPos = Parcours.OrderBy(x => x.Value).First();
                int dist = Distances.GetValueOrDefault(currentPos.Key, (0,new())).Item1;
                
                foreach (var neighbour in _neighbors(currentPos.Key.Item1, currentPos.Key.Item2, matrix.Length, matrix[0].Length).Where(x => !Done.Contains(x) && !Parcours.ContainsKey(x)))
                {
                    int currentDist = dist + matrix[neighbour.x][neighbour.y];
                    if (Distances.GetValueOrDefault(neighbour, (int.MaxValue,new())).Item1 >= currentDist)
                    {
                        List<(int, int)> last = new(Distances[currentPos.Key].Item2);
                        last.Add(neighbour);
                        Distances[neighbour] = (currentDist, last);
                    }

                    Parcours[neighbour] = currentDist;
                }
                Parcours.Remove(currentPos.Key);
                Done.Add(currentPos.Key);
            }
            return Distances[to].Item1;
        }

        public static int Part2(string input)
        {
            int[][] matrix = input.Split(Environment.NewLine).Select(x => x.Select(y => (int)Char.GetNumericValue(y)).ToArray()).ToArray();
            int size = matrix.Length;
            int[][] completeMap = new int[5 * size][];

            for (int i = 0; i < completeMap.Length; i++)
            {
                completeMap[i] = new int[5 * matrix[0].Length];
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    for (int d = 0; d < 5; d++)
                    {
                        for (int q = 0; q < 5; q++)
                        {
                            int inc = matrix[i][j] + d + q;
                            if (inc > 9)
                                inc = (inc) % 9;
                            completeMap[i + d * size][j + matrix[0].Length * q] = inc;
                        }
                    }
                }
            }
            return Dijkstra(completeMap, (completeMap.Length - 1, completeMap[0].Length - 1));
        }
    }
}
