using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    static class Day16
    {
        public static void Part1()
        {
            var inputs = File.ReadAllText("day16-input.txt")
                .Split(Environment.NewLine + Environment.NewLine);
            var fields = new Regex(@"(?<field>.+): (?<lowerStart>\d+)-(?<lowerEnd>\d+) or (?<upperStart>\d+)-(?<upperEnd>\d+)")
                .Matches(inputs[0])
                .Select(x => (field: x.Groups["field"].Value, lowerStart: int.Parse(x.Groups["lowerStart"].Value), lowerEnd: int.Parse(x.Groups["lowerEnd"].Value), upperStart: int.Parse(x.Groups["upperStart"].Value), upperEnd: int.Parse(x.Groups["upperEnd"].Value)));
            var myTickets = inputs[1].Split(Environment.NewLine)[1].Split(',').Select(int.Parse);
            var nearbyTickets = inputs[2].Split(Environment.NewLine).Skip(1).Select(x => x.Split(',').Select(int.Parse));

            var errorRate = nearbyTickets
                .SelectMany(x => x)
                .Where(nearbyTicket => !fields.Any(field => (nearbyTicket >= field.lowerStart && nearbyTicket <= field.lowerEnd) || (nearbyTicket >= field.upperStart && nearbyTicket <= field.upperEnd)))
                .Sum();

            Console.WriteLine(errorRate);
        }
    }
}
