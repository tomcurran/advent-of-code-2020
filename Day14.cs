using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    static class Day14
    {
        public static void Part1()
        {
            var input = File.ReadAllText("day14-input.txt");
            var regex = new Regex(@"mask = (?<mask>.+)|mem\[(?<address>\d+)\] = (?<value>\d+)");
            var memory = new Dictionary<int, ulong>();
            ulong zerosMask = 0, onesMask = 0;
            foreach (Match match in regex.Matches(input))
            {
                if (!string.IsNullOrEmpty(match.Groups["mask"].Value))
                {
                    var mask = match.Groups["mask"].Value;
                    zerosMask = Convert.ToUInt64(mask.Replace('X', '0'), 2);
                    onesMask = Convert.ToUInt64(mask.Replace('X', '1'), 2);
                }
                else
                {
                    var address = int.Parse(match.Groups["address"].Value);
                    var value = ulong.Parse(match.Groups["value"].Value);
                    value |= zerosMask;
                    value &= onesMask;
                    memory[address] = value;
                }
            }

            var sumInMemory = memory.Values.Aggregate((x, y) => x + y);
            Console.WriteLine(sumInMemory);
        }
    }
}
