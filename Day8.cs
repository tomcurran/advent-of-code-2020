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
            var instructions = GetInstructions();
            var accumulator = 0;
            var visited = new HashSet<int>();
            for (var i = 0; !visited.Contains(i) && i < instructions.Count(); i++)
            {
                visited.Add(i);
                var (operation, number) = instructions.ElementAt(i);
                if (operation == "acc")
                {
                    accumulator += number;
                }
                else if (operation == "jmp")
                {
                    i += number - 1;
                }
            }
            Console.WriteLine(accumulator);
        }

        public static void Part2()
        {
            var instructions = GetInstructions();
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

            static (bool terminated, int accumulator) ExecuteInstructions(IEnumerable<(string operation, int number)> instructions, int changeInstructionIndex)
            {
                var accumulator = 0;
                var visited = new HashSet<int>();
                for (var i = 0; i < instructions.Count(); i++)
                {
                    if (visited.Contains(i))
                    {
                        return (false, accumulator);
                    }
                    visited.Add(i);
                    var (operation, number) = instructions.ElementAt(i);
                    if (operation == "acc")
                    {
                        accumulator += number;
                    }
                    else if ((operation == "jmp" && i != changeInstructionIndex)
                        || (operation == "nop" && i == changeInstructionIndex))
                    {
                        i += number - 1;
                    }
                }
                return (true, accumulator);
            }
        }

        private static IEnumerable<(string operation, int number)> GetInstructions()
        {
            var regex = new Regex(@"(?<operation>[a-z]+) (?<number>[+-]\d+)");
            var instructions = File.ReadAllLines("day8-input.txt")
                .Select(input =>
                {
                    var match = regex.Match(input);
                    var operation = match.Groups["operation"].Value;
                    var number = int.Parse(match.Groups["number"].Value);
                    return (operation, number);
                });
            return instructions;
        }
    }
}
