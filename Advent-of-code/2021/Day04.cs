using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day04
    {

        public static (List<Grid>,int[]) ParseInput(string input)
        {
            string[] inputs = input.Split(Environment.NewLine);
            int[] draw = inputs[0].Split(',').Select(x => int.Parse(x)).ToArray();
            List<Grid> grids = new List<Grid>();
            for (int i = 2; i < inputs.Skip(2).Count(); i += 6)
            {
                string[] rows = inputs.Skip(i).Take(5).ToArray();
                Entry[,] entries = new Entry[5, 5];
                for (int x = 0; x < rows.Count(); x++)
                {
                    string[] row = rows[x].Trim().Replace("  ", " ").Split(' ');
                    for (int y = 0; y < row.Count(); y++)
                    {
                        entries[x, y] = new Entry(int.Parse(row[y]));
                    }
                }
                grids.Add(new Grid(entries));
            }
            return (grids, draw);
        }
        public static int Part1(string input)
        {
            (List<Grid>,int[]) Init =  ParseInput(input);

            foreach(int nb in Init.Item2)
            {
                foreach(Grid g in Init.Item1)
                {
                    g.Mark(nb);
                    int res = g.HasWon();
                    if (res != -1)
                        return res * nb;
                }
            }

            return 0;
        }

        public static int Part2(string input)
        {
            (List<Grid>, int[]) Init = ParseInput(input);
            int i = 0;
            int final = 0;
            while (Init.Item1.Count(g=>!g.won) > 0)
            {
                int nb = Init.Item2[i];
                foreach (Grid g in Init.Item1.Where(g=>!g.won))
                {
                    g.Mark(nb);
                    int res = g.HasWon();
                    if (res != -1)
                        final = res * nb;
                }
                i++;
            }
            return final;
        }
    }

    public class Grid
    {
        public Entry[,] grid;
        public bool won;
        public Grid(Entry[,] entries)
        {
            grid = entries;
            won = false;
        }

        public void Mark(int number)
        {
            foreach(Entry e in grid)
            {
                if (e.Value == number)
                {
                    e.Marked = true;
                    break;
                }
            }
        }

        public int HasWon()
        {
            for(int i = 0; i < 5; i++)
            {
                if (GetColumn(grid, i).All(x => x.Marked) || GetRow(grid, i).All(x => x.Marked))
                {
                    var query = from Entry item in grid
                                where !item.Marked
                                select item.Value;
                    won = true;
                    return query.Sum();
                }

            }
            return -1;
        }

        public Entry[] GetColumn(Entry[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        public Entry[] GetRow(Entry[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
    }

    public class Entry
    {
        public int Value;
        public bool Marked;
        public Entry(int number)
        {
            this.Value = number;
            Marked = false;
        }
    }
}
