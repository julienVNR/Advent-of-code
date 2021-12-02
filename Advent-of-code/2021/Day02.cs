using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day02
    {
        public static int Part1(string inputs)
        {
            return inputs.Split(Environment.NewLine)
                .Select(x => (Direction: x.Split(' ')[0], Step: int.Parse(x.Split(' ')[1])))
                .Aggregate((H: 0, D: 0), (agg, comm) =>
                {
                    switch (comm.Direction)
                    {
                        case "forward":
                            agg.H += comm.Step;
                            break;
                        case "up":
                            agg.D -= comm.Step;
                            break;
                        case "down":
                            agg.D += comm.Step;
                            break;
                    }
                    return agg;
                }, (agg) => agg.H * agg.D);
        }

        //JS Solution Part1
        /*
            var input = document.getElementsByTagName("pre")[0].innerHTML.split("\n");
            var submarine = {
                hor: 0,
                depth: 0
            }
            input.map(function(element) {
                    insruction = element.split(" ")
                    switch (insruction[0]) {
                    case "forward": submarine.hor += Number(insruction[1]); break;
                    case "up": submarine.depth -= Number(insruction[1]); break;
                    case "down": submarine.depth += Number(insruction[1]); break;
                    }
                })
            console.log(submarine.hor * submarine.depth)
          
         * */

        public static int Part2(string inputs)
        {
            return inputs.Split(Environment.NewLine)
                .Select(x => (Direction: x.Split(' ')[0], Step: int.Parse(x.Split(' ')[1])))
                .Aggregate((H: 0, D: 0, A : 0), (agg, comm) =>
                {
                    switch (comm.Direction)
                    {
                        case "forward":
                            agg.H += comm.Step;
                            agg.D += comm.Step * agg.A;
                            break;
                        case "up":
                            agg.A -= comm.Step;
                            break;
                        case "down":
                            agg.A += comm.Step;
                            break;
                    }
                    return agg;
                }, (agg) => agg.H * agg.D);
        }

        /* JS Solution Part2
            var input = document.getElementsByTagName("pre")[0].innerHTML.split("\n");
            var submarine = {
                hor: 0,
                depth: 0,
                aim : 0
            }
            input.map(function(element) {
                    insruction = element.split(" ")
                    switch (insruction[0]) {
                    case "forward": submarine.hor += Number(insruction[1]); submarine.depth += Number(insruction[1]) * submarine.aim; break;
                    case "up": submarine.aim -= Number(insruction[1]); break;
                    case "down": submarine.aim += Number(insruction[1]); break;
                    }
                })
            console.log(submarine.hor * submarine.depth)
        */
    }
}
