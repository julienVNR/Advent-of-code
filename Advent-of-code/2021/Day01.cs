using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day01
    {
        public static int Part1(string inputs)
        {
            return inputs.Split(Environment.NewLine).Select(x => Convert.ToInt32(x)).Pairwise((a,b) => b > a).Count(x => x);
            //document.getElementsByTagName("pre")[0].innerText.split('\n').filter(x=>x.length>0).map(function(item,i,arr){return parseInt(item) > parseInt(arr[i-1])}).filter(x=>x).length
        }

        public static int Part2(string inputs)
        {
            return inputs.Split(Environment.NewLine).Select(x => Convert.ToInt32(x)).Window(3).Select(x => x.Sum()).Pairwise((a, b) => b > a).Count(x=>x);
            //document.getElementsByTagName("pre")[0].innerText.split('\n').filter(x=>x.length>0).map(function(item,i,arr){return parseInt(item) + parseInt(arr[i-1]) + parseInt(arr[i-2])}).map(function(item,i,arr){return item > arr[i-1]}).filter(x=>x).length
        }
    }
}
