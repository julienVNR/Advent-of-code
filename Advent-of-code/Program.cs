﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;

namespace Advent_of_code
{
    class Program
    {
        static readonly int Day = 25;
        static readonly int Start = 15;
        static void Main(string[] args)
        {
            (string, ResourceManager) choice = PrintChoice();
            for (int d = Start; d <= Day; d++)
            {
                string className = (d < 10) ? "0"+d.ToString() : d.ToString();
                Type t = Type.GetType(String.Format("Advent_of_code._{0}.Day{1}",choice.Item1,className));

                if (t != null)
                {
                    string input = choice.Item2.GetString("Input" + d);
                    Console.WriteLine("Jour " + d);
                    Console.WriteLine("╔════════╦═════════════════╦═══════════════════════════════╗");
                    for (int i = 1; i <= 2; i++)
                    {
                        string res;
                        MethodInfo method = t.GetMethod("Part" + i);
                        if (method != null)
                        {
                            Stopwatch st = new Stopwatch();
                            st.Start();
                            try
                            {
                                res = method.Invoke(t, new object[] { input }).ToString();
                            }catch(Exception)
                            {
                                res = "Error";
                            }
                            st.Stop();
                            Console.WriteLine("║ Part {0} ║ {1,-15} ║ Temps d'éxécution: {2,-10} ║", i, res, Math.Round(st.Elapsed.TotalSeconds, 5) + "s");
                        }
                        if (i == 1)
                            Console.WriteLine("╠════════╬═════════════════╬═══════════════════════════════╣");
                        GC.Collect();
                    }
                    Console.WriteLine("╚════════╩═════════════════╩═══════════════════════════════╝");
                }
            }
            Console.ReadKey();
        }

        public static (string,ResourceManager) PrintChoice()
        {
            while (true) {
                Console.WriteLine("Quelle année souhaité vous exécuter :");
                Console.WriteLine("1. 2020");
                Console.WriteLine("2. 2021");
                var key = Console.ReadKey();
                Console.WriteLine("");
                switch (key.Key)
                {
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.D1:
                        return ("2020",Resources.Input2020.ResourceManager);
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.D2:
                        return ("2021", Resources.Input2021.ResourceManager);
                }
                Console.WriteLine("Mauvaise entrée");
            }
        }
        
    }
    
}
