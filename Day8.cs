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

        public static void Part2()
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

            for (var insturctionIndex = 0; insturctionIndex < instructions.Count(); insturctionIndex++)
            {
                if (instructions.ElementAt(insturctionIndex).operation == "acc")
                {
                    continue;
                }

                var (terminated, accumulator) = ExecuteInstructions(instructions, insturctionIndex);
                if (terminated)
                {
                    Console.WriteLine(accumulator);
                    break;
                }
            }

            static (bool terminated, int accumulator) ExecuteInstructions(IEnumerable<(string operation, string sign, int number)> instructions, int changeInstructionIndex)
            {
                var accumulator = 0;
                var visited = new HashSet<int>();
                for (var i = 0; i < instructions.Count(); i++)
                {
                    if (visited.Contains(i))
                    {
                        return (false, accumulator);
                    }
                    else
                    {
                        visited.Add(i);
                    }
                    var (operation, sign, number) = instructions.ElementAt(i);
                    if (operation == "acc")
                    {
                        if (sign == "+")
                        {
                            accumulator += number;
                        }
                        else if (sign == "-")
                        {
                            accumulator -= number;
                        }
                    }
                    else if ((operation == "jmp" && i != changeInstructionIndex)
                        || (operation == "nop" && i == changeInstructionIndex))
                    {
                        if (sign == "+")
                        {
                            i += number - 1;
                        }
                        else if (sign == "-")
                        {
                            i -= number + 1;
                        }
                        if (i < 0)
                        {
                            return (false, accumulator);
                        }
                    }
                }
                return (true, accumulator);
            }
        }
    }
}
