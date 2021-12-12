using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day11
    {
        private static IList<(int x, int y)> _neighbors(int x, int y, int maxX, int maxY)
        {
            List<(int x, int y)> neighours = new();
            if (x != 0) neighours.Add((x - 1, y));
            if (y != 0) neighours.Add((x, y - 1));
            if (x + 1 < maxX) neighours.Add((x + 1, y));
            if (y + 1 < maxY) neighours.Add((x, y + 1));
            if (x != 0 && y != 0) neighours.Add((x - 1, y - 1));
            if (x != 0 && y+1 < maxY) neighours.Add((x - 1, y + 1));
            if (x+1 < maxX && y + 1 < maxY) neighours.Add((x + 1, y + 1));
            if (x + 1 < maxX && y != 0) neighours.Add((x + 1, y - 1));
            return neighours;
        }


        public static int Part1(string input)
        {
            int[][] matrix = input.Split(Environment.NewLine).Select(x => x.Select(y => (int)Char.GetNumericValue(y)).ToArray()).ToArray();
            int flash = 0;
            for (int i = 1; i <= 100; i++)
            {
                for (int x = 0; x < matrix.Length; x++)
                {
                    for (int y = 0; y < matrix[x].Length; y++)
                    {
                        matrix[x][y]++;
                    }
                }
                for (int x = 0; x < matrix.Length; x++)
                {
                    for (int y = 0; y < matrix[x].Length; y++)
                    {
                        if (matrix[x][y] > 9)
                            flash += Compute(x, y, matrix);
                    }
                }
            }
            return flash;
        }

        public static int Part2(string input)
        {
            int[][] matrix = input.Split(Environment.NewLine).Select(x => x.Select(y => (int)Char.GetNumericValue(y)).ToArray()).ToArray();
            
            int day = 0;
            int flash = 0;
            do
            {

                day++;
                flash = 0;
                for (int x = 0; x < matrix.Length; x++)
                {
                    for (int y = 0; y < matrix[x].Length; y++)
                    {
                        matrix[x][y]++;
                    }
                }
                for (int x = 0; x < matrix.Length; x++)
                {
                    for (int y = 0; y < matrix[x].Length; y++)
                    {
                        if (matrix[x][y] > 9)
                            flash = +Compute(x, y, matrix);
                    }
                }
            } while (matrix.Length * matrix[0].Length != flash);
            return day;
        }

        public static int Compute(int x,int y, int[][] matrix)
        {
            int flash = 1;
            matrix[x][y] = 0;
            foreach ((int x,int y) adj in _neighbors(x, y, matrix.Length, matrix[0].Length)){
                if(matrix[adj.x][adj.y] != 0)
                    matrix[adj.x][adj.y]++;
                if (matrix[adj.x][adj.y] > 9)
                {
                    flash+=Compute(adj.x, adj.y, matrix);
                }
            }
            return flash;
        }
    }
}
