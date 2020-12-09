using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    static class Day8
    {
        public static void Part1()
        {
            var regex = new Regex(@"(?<operation>[a-z]+) (?<sign>[+-])(?<number>\d+)");
            var instructions = File.ReadAllLines("day8-input.txt")
                .Select(input =>
                {
                    var match = regex.Match(input);
                    var operation = match.Groups["operation"].Value;
                    var sign = match.Groups["sign"].Value;
                    var number = int.Parse(match.Groups["number"].Value);
                    return (operation, sign, number);
                });

            var accumulator = 0;
            var visited = new HashSet<int>();
            for (var i = 0; !visited.Contains(i) && i < instructions.Count(); i++)
            {
                var (operation, sign, number) = instructions.ElementAt(i);
                if (operation == "acc")
                {
                    if (sign == "+")
                    {
                        accumulator += number;
                    }
                    else if(sign == "-")
                    {
                        accumulator -= number;
                    }
                }
                else if (operation == "jmp")
                {
                    if (sign == "+")
                    {
                        i += number - 1;
                    }
                    else if (sign == "-")
                    {
                        i -= number + 1;
                    }
                }
                visited.Add(i);
            }
            Console.WriteLine(accumulator);
        }
    }
}
