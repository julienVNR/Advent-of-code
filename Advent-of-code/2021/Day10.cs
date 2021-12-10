using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day10
    {

        public static char Complement(char c) => c switch
        {
            '[' => ']',
            '<' => '>',
            '(' => ')',
            '{' => '}'
        };

        public static int ScorePart1(char c) => c switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137
        };

        public static int Part1(string input)
        {
            string[] inputs = input.Split(Environment.NewLine);
            int points = 0;
            foreach(string line in inputs)
            {
                Stack<char> brackets = new();
                foreach(char c in line)
                {
                    switch (c)
                    {
                        case '<':
                        case '(':
                        case '[':
                        case '{':
                            brackets.Push(c);
                            break;
                        default:
                            char expected = brackets.Pop();
                            if(c != Complement(expected))
                            {
                                points += ScorePart1(c);
                                goto NextInput;
                            }
                            break;
                    }
                }
                NextInput:;
            }
            return points;
        }

        public static int ScorePart2(char c) => c switch
        {
            '(' => 1,
            '[' => 2,
            '{' => 3,
            '<' => 4
        };

        public static long Part2(string input)
        {
            string[] inputs = input.Split(Environment.NewLine);
            List<long> incomplete = new();
            foreach (string line in inputs)
            {
                Stack<char> brackets = new();
                foreach (char c in line)
                {
                    switch (c)
                    {
                        case '<':
                        case '(':
                        case '[':
                        case '{':
                            brackets.Push(c);
                            break;
                        default:
                            if (c != Complement(brackets.Pop()))
                                goto NextInput;
                            break;
                    }
                }
                long score = 0;
                do
                {
                    score *= 5;
                    score += ScorePart2(brackets.Pop());
                } while (brackets.Count > 0);
                incomplete.Add(score);

                NextInput:;
            }
            int midlle = (int)Math.Ceiling(incomplete.Count / 2.0)-1;
            return incomplete.OrderBy(x=>x).ElementAt(midlle);
        }
    }
}
