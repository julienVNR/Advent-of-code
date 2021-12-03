using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day03
    {
        public static int Part1(string input)
        {
            string[] inputs = input.Split(Environment.NewLine);
            int inputLength = inputs.First().Length;
            int gamma = 0;
            int epsylon = 0;
            for(int i = 0;i < inputLength; i++)
            {
                int inc = (int) Math.Pow(2, inputLength - i - 1);
                if (inputs.Select(x => x[i]).OrderBy(x => x).ElementAt(inputs.Length / 2) == '1')
                    gamma += inc;
                else
                    epsylon += inc;
            }
            return gamma * epsylon;
        }

        public static int Part2(string input)
        {
            string[] inputs = input.Split(Environment.NewLine);
            
            int oxigen = Convert.ToInt32(Filter(inputs,true,0), 2);
            int CO2 = Convert.ToInt32(Filter(inputs, false,0), 2);
            return oxigen * CO2;
        }

        public static string Filter(string[] input,bool most,int index)
        {
            int nb1 = input.Count(x => x[index] == '1');
            int nb0 = input.Length - nb1;
            char remove = (nb1 >= nb0) ? '0' : '1';
            if(most)
                input = input.Where(x => x[index] != remove).ToArray();
            else
                input = input.Where(x => x[index] == remove).ToArray();
            index++;
            if (input.Length == 1)
                return input[0];
            else
                return Filter(input, most, index++);
        }

        /* Solution JS  part1
            a = document.getElementsByTagName("pre")[0].innerHTML.split("\n").map(s => parseInt(s, 2));
            gamma = 0; 
            for (let i = 1; i < (1 << 12); i <<= 1) {
	            // n & i : Et binaire : 1024 & 2048 = 0 | 2050 & 2048 = 2048
	            // a.reduce((c,n) => c + (n & i),0)
	
	            //00100 = 4
	            //11110 = 30
	            //10110 = 22
	            //10111 = 23
	            //10101 = 21
	            //01111 = 15
	            //00111 = 7
	            //11100 = 28
	            //10000 = 16
	            //11001 = 25
	            //00010 = 2
	            //01010 = 10

	            gamma += i* +(a.reduce((c, n) => c + (n & i), 0) > (a.length / 2)*i) 
            }
            //1010 = 10
            //0101 = 5 = (2^4)-1-10
    
            epsylon = (1 << 12) - 1 - gamma
            console.log(gamma* epsylon)
        */

        /**** Solution Part2 JS
         * 
         oxygen = document.getElementsByTagName("pre")[0].innerHTML.split("\n").map(s => parseInt(s, 2));
            dioxydeC = [...oxygen]; 
            for (let s = 11; s >= 0; s--) { 
	            i = 1 << s; 
	            if (oxygen.length > 1){
		            most = (i * +(oxygen.reduce((c, n) => c + (n & i), 0) * 2 >= oxygen.length * i))
		            // (n & i) ^ most Ou exclusif / XOR
		            // vrai & vrai = faux
		            // vrai & faux = vrai
		            // faux & faux = vrai
		            // faux & vrai = vrai
		            oxygen = oxygen.filter(n => !((n & i) ^ most)); 
	            }
	            if (dioxydeC.length > 1) {
		            less = (i * +(dioxydeC.reduce((c, n) => c + (n & i), 0) * 2 < dioxydeC.length * i))
		            dioxydeC = dioxydeC.filter(n => !((n & i) ^ less)); 
	            }
            }
            console.log(oxygen*dioxydeC)
         * 
         */

    }
}
