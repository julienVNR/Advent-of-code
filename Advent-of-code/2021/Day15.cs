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
            return Dijkstra(matrix);
        }

        public static int Dijkstra(int[][] matrix)
        {
            Dictionary<(int, int), int> Distances = new Dictionary<(int, int), int>();
            List<(int, int)> Done = new();
            SortedList<(int, int), int> Parcours = new();
            Parcours.Add((0, 0), 0);
            Distances.Add((0, 0), 0);
            while (Parcours.Count > 0)
            {
                var currentPos = Parcours.First();
                int dist = Distances.GetValueOrDefault(currentPos.Key, 0);
                foreach (var neighbour in _neighbors(currentPos.Key.Item1, currentPos.Key.Item2, matrix.Length, matrix[0].Length).Where(x => !Done.Contains(x)))
                {
                    int currentDist = dist + matrix[neighbour.x][neighbour.y];
                    if (Distances.GetValueOrDefault(neighbour, int.MaxValue) > currentDist)
                        Distances[neighbour] = currentDist;
                    Parcours[neighbour] = currentDist;
                    if (neighbour == (matrix.Length - 1, matrix.Length - 1))
                        break;
                }
                Parcours.RemoveAt(0);
                Done.Add(currentPos.Key);
            }
            return Distances[(matrix.Length - 1, matrix[0].Length - 1)];
        }

        public static int Part2(string input)
        {
            int[][] matrix = input.Split(Environment.NewLine).Select(x => x.Select(y => (int)Char.GetNumericValue(y)).ToArray()).ToArray();
            int size = matrix.Length;
            int[][] completeMap = new int[5 * size][];

            for (int i = 0; i < completeMap.Length; i++)
            {
                completeMap[i] = new int[5 * size];
            }
            for (int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    for(int d = 0;d < 5; d++)
                    {
                        for (int q = 0; q < 5; q++)
                        {
                            int inc = matrix[i][j] + d + q;
                            if (inc > 9)
                                inc = (inc) % 9;
                            completeMap[i + size * d][j+ q * size] = inc;
                            completeMap[i + q * size][j + size * d] = inc;
                        }
                    }
                }
            }
            return Dijkstra(completeMap);
        }
    }
}
