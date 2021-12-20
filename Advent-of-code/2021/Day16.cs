using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_of_code._2021
{
    public static class Day16
    {
        public static string HexaToBin(this char c) => c switch
        {
            '0' => "0000",
            '1' => "0001",
            '2' => "0010",
            '3' => "0011",
            '4' => "0100",
            '5' => "0101",
            '6' => "0110",
            '7' => "0111",
            '8' => "1000",
            '9' => "1001",
            'A' => "1010",
            'B' => "1011",
            'C' => "1100",
            'D' => "1101",
            'E' => "1110",
            'F' => "1111"
        };


        public static int Part1(string input) => CreatePackage(input).VersionId;
        public static long Part2(string input) => CreatePackage(input).Value;

        static Packet CreatePackage(string input)
        {
            string binary = string.Empty;
            input.ForEach(c => binary += c.HexaToBin());

            return new Packet(binary);
        }

        class Packet
        {
            List<Packet> Subpacket;
            string Body;
            public string Rest;
            int PackageId;
            public long Value
            {
                get
                {
                    switch (PackageId)
                    {
                        case 4:
                            return _value;
                        case 0:
                            return Subpacket.Sum(x => x.Value);
                        case 1:
                            return Subpacket.Aggregate((long)1, (acc, val) => acc * val.Value);
                        case 2:
                            return Subpacket.Min(x => x.Value);
                        case 3:
                            return Subpacket.Max(x => x.Value);
                        case 5:
                            return Subpacket[0].Value > Subpacket[1].Value ? 1 : 0;
                        case 6:
                            return Subpacket[0].Value < Subpacket[1].Value ? 1 : 0;
                        case 7:
                            return Subpacket[0].Value == Subpacket[1].Value ? 1 : 0;
                        default:
                            throw new InvalidOperationException();
                    }
                }
            }
            long _value;
            public int VersionId
            {
                get
                {
                    var self = Convert.ToInt32(Body.Substring(0, 3), 2);
                    return self+Subpacket.Sum(x=>x.VersionId);
                }
            }

            public Packet(string input)
            {
                Body = string.Empty;
                this.PackageId = Convert.ToInt32(input.Substring(3, 3), 2);
                Subpacket = new();
                if (this.PackageId == 4)
                {
                    int currentPointer = 6;
                    string nbr = string.Empty;
                    char c;
                    do
                    {
                        string crtInput = input.Substring(currentPointer, 5);
                        c = crtInput[0];
                        nbr += crtInput.Substring(1, 4);
                        currentPointer += 5;
                    } while (c != '0');
                    _value = Convert.ToInt64(nbr, 2);
                    this.Rest = input.Substring(currentPointer);
                    this.Body = input.Substring(0, currentPointer);
                }
                else
                {
                    char I = input[6];
                    int nextBody = 15;
                    if (I == '1')
                        nextBody = 11;
                    this.Body = input.Substring(0,7 + nextBody);
                    int subPatternLentgh = Convert.ToInt32(input.Substring(7, nextBody), 2);
                    if(nextBody == 11)
                    {
                        string rest = input.Substring(18);
                        for(int i = 0;i < subPatternLentgh;i++)
                        {
                            Packet p = new Packet(rest);
                            Subpacket.Add(p);
                            rest = p.Rest;
                            this.Body += p.Body;
                        }
                        this.Rest = rest;
                    }
                    else
                    {
                        string rest = input.Substring(22);
                        int subLentgh = 0;
                        while(subLentgh != subPatternLentgh)
                        {
                            Packet p = new Packet(rest);
                            Subpacket.Add(p);
                            rest = p.Rest;
                            this.Body += p.Body;
                            subLentgh += p.Body.Length;
                        }
                        this.Rest = rest;
                    }

                }
            }
        }
    }
}
