using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    static class Day12
    {
        public static void Part1()
        {
            var inputs = File.ReadAllText("day12-input.txt");
            var instructions = new Regex(@"(?<action>.)(?<value>\d+)")
                .Matches(inputs)
                .Select(x => (action: x.Groups["action"].Value, value: int.Parse(x.Groups["value"].Value)));

            var facing = 0;
            var eastWest = 0;
            var northSouth = 0;

            foreach (var (action, value) in instructions)
            {
                if (action == "N" || (action == "F" && facing == 270))
                {
                    northSouth += value;
                }
                else if (action == "S" || (action == "F" && facing == 90))
                {
                    northSouth -= value;
                }
                else if (action == "E" || (action == "F" && facing == 0))
                {
                    eastWest += value;
                }
                else if (action == "W" || (action == "F" && facing == 180))
                {
                    eastWest -= value;
                }
                else if (action == "L")
                {
                    facing = (360 + (facing - value)) % 360;
                }
                else if (action == "R")
                {
                    facing = (facing + value) % 360;
                }
            }

            var manhattanDistance = Math.Abs(eastWest) + Math.Abs(northSouth);
            Console.WriteLine(manhattanDistance);
        }
    }
}
