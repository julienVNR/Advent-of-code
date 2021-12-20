using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code._2021
{
    public static class Day17
    {
        public class Target
        {
            public int MinX;
            public int MaxX;
            public int MinY;
            public int MaxY;

            public Target(string minX, string maxX, string minY, string maxY)
            {
                MinX = int.Parse(minX);
                MaxX = int.Parse(maxX);
                MinY = int.Parse(minY);
                MaxY = int.Parse(maxY);
            }

            public bool Touched(int x, int y) {
                (int x, int y) currentPos = (0, 0);
                while(currentPos.x <= MaxX && currentPos.y >= MinY)
                {
                    currentPos = (currentPos.x + x, currentPos.y + y);
                    if(x > 0)
                        x--;
                    y--;
                    if (currentPos.x <= MaxX && currentPos.x >= MinX && currentPos.y >= MinY && currentPos.y <= MaxY)
                        return true;
                }
                return false;
            }
        }

        public static int Part1(string input)
        {
            Regex r = new Regex(@"x=(-?\d+)..(-?\d+), y=(-?\d+)..(-?\d+)");
            Match m = r.Match(input);
            int currentYPosition = 0;

            for (int currentYSpeed = (int.Parse(m.Groups[3].Value) * -1) - 1; currentYSpeed != 0; currentYSpeed--)
            {
                currentYPosition += currentYSpeed;
            }
            return currentYPosition;
        }

        public static int Part2(string input)
        {
            Regex r = new Regex(@"x=(-?\d+)..(-?\d+), y=(-?\d+)..(-?\d+)");
            Match m = r.Match(input);
            Target t = new Target(m.Groups[1].Value, m.Groups[2].Value, m.Groups[3].Value, m.Groups[4].Value);
            List<(int, int)> touched = new();
            for (int i = t.MaxX; i >= 0; i--)
            {
                for (int j = t.MinY; j <= Math.Abs(t.MinY); j++)
                {
                    if (t.Touched(i, j))
                        touched.Add((i, j));
                }
            }
            return touched.Count;
        }
    }
}
