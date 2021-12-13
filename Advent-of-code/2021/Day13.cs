using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day13
    {
        public static int Part1(string input) => Solver(input);
        public static int Part2(string input) => Solver(input, true);

        public static int Solver(string input,bool excuteAllFold = false)
        {
            List<(int x, int y)> paper = input.Split(Environment.NewLine).Where(x => !x.StartsWith("fold") && x.Length > 0).Select(x => (int.Parse(x.Split(',')[0]), int.Parse(x.Split(',')[1]))).ToList();
            List<string> folds = input.Split(Environment.NewLine).Where(x => x.StartsWith("fold")).ToList();
            if (!excuteAllFold)
                folds = folds.Take(1).ToList();
            foreach(string fold in folds)
            {
                string[] instruction = fold.Replace("fold along ","").Split('=');
                int foldIndex = int.Parse(instruction[1]);
                paper = paper.Select(x => x.Fold(instruction[0],foldIndex)).Distinct().ToList();
            }

            if (excuteAllFold)
                paper.Print();
            else
                return paper.Count();
            return 0;

        }

        public static (int,int) Fold(this (int x, int y) dot, string direction, int foldIndex)
        {
            if (direction == "x" && dot.x > foldIndex)
                dot.x = foldIndex * 2 - dot.x;
            else if (direction == "y" && dot.y > foldIndex)
                dot.y = foldIndex * 2 - dot.y;
            return dot;
        }

        public static void Print(this List<(int x, int y)> paper)
        {
            for (int i = 0; i <= paper.Max(x => x.y); i++)
            {
                Console.WriteLine("");
                for (int j = 0; j <= paper.Max(x => x.x); j++)
                {
                    if (paper.Contains((j, i)))
                        Console.Write("▓");
                    else
                        Console.Write(" ");
                }
            }

            Console.WriteLine();
        }
    }

}
