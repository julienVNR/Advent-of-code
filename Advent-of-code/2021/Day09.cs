using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day09
    {

        private static IList<(int x, int y)> _neighbors(int x, int y, int maxX, int maxY)
        {
            List<(int x, int y)> neighours = new List<(int x, int y)>();
            if (x != 0)
                neighours.Add((x - 1, y));
            if (y != 0)
                neighours.Add((x, y - 1));
            if (x + 1 < maxX)
                neighours.Add((x + 1, y));
            if (y + 1 < maxY)
                neighours.Add((x, y + 1));
            return neighours;
        }

        public static List<(int, int)> LowPoints(int[][] matrix)
        {
            List<(int, int)> points = new List<(int, int)>();
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    bool isMin = true;
                    foreach ((int x, int y) prox in _neighbors(i, j, matrix.Length, matrix[i].Length))
                    {
                        if (matrix[prox.x][prox.y] <= matrix[i][j])
                        {
                            isMin = false;
                            break;
                        }
                    }
                    if (isMin)
                        points.Add((i, j));
                }
            }
            return points;
        }

        public static int Part1(string input)
        {
            int[][] matrix = input.Split(Environment.NewLine).Select(x => x.Select(y => (int)Char.GetNumericValue(y)).ToArray()).ToArray();
            return LowPoints(matrix).Sum(x => matrix[x.Item1][x.Item2] + 1);
        }

        public static int Part2(string input)
        {
            int[][] matrix = input.Split(Environment.NewLine).Select(x => x.Select(y => (int)Char.GetNumericValue(y)).ToArray()).ToArray();
            List<(int, int)> points = LowPoints(matrix);
            return points.Select(x => GetBasinSize(x, matrix)).OrderByDescending(x => x).Take(3).Aggregate((a,x) => a*x);
        }
        
        public static int GetBasinSize((int, int) start, int[][] matrix)
        {
            List<(int, int)> points = new List<(int, int)>();
            points.Add(start);
            List<(int, int)> seen = new List<(int, int)>();
            while(points.Count() != 0)
            {
                start = points[0];
                seen.Add(start);
                points.AddRange(_neighbors(start.Item1, start.Item2, matrix.Length, matrix[0].Length).Where(x=>matrix[x.x][x.y] != 9 && !seen.Contains(x) && !points.Contains(x)).ToList());
                points.RemoveAt(0);
            }
            return seen.Count();
        }
    }
}
