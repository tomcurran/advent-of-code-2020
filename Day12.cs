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

        public static void Part2()
        {
            var inputs = File.ReadAllText("day12-input.txt");
            var instructions = new Regex(@"(?<action>.)(?<value>\d+)")
                .Matches(inputs)
                .Select(x => (action: x.Groups["action"].Value, value: int.Parse(x.Groups["value"].Value)));

            var shipEastWest = 0;
            var shipNorthSouth = 0;
            var waypointEastWest = 10;
            var waypointNorthSouth = 1;

            foreach (var (action, value) in instructions)
            {
                if (action == "N")
                {
                    waypointNorthSouth += value;
                }
                else if (action == "S")
                {
                    waypointNorthSouth -= value;
                }
                else if (action == "E")
                {
                    waypointEastWest += value;
                }
                else if (action == "W")
                {
                    waypointEastWest -= value;
                }
                else if (action == "F")
                {
                    shipNorthSouth += value * waypointNorthSouth;
                    shipEastWest += value * waypointEastWest;
                }
                else if (action == "L")
                {
                    var angle = value * (Math.PI / 180);
                    var x = waypointEastWest;
                    var y = waypointNorthSouth;
                    waypointEastWest = (int)Math.Round((x * Math.Cos(angle)) - (y * Math.Sin(angle)));
                    waypointNorthSouth = (int)Math.Round(x * Math.Sin(angle) + (y * Math.Cos(angle)));
                }
                else if (action == "R")
                {
                    var angle = value * (Math.PI / 180);
                    var x = waypointEastWest;
                    var y = waypointNorthSouth;
                    waypointEastWest = (int)Math.Round((x * Math.Cos(angle)) + (y * Math.Sin(angle)));
                    waypointNorthSouth = (int)Math.Round(-x * Math.Sin(angle) + (y * Math.Cos(angle)));
                }
            }

            var manhattanDistance = Math.Abs(shipEastWest) + Math.Abs(shipNorthSouth);
            Console.WriteLine(manhattanDistance);
        }
    }
}
