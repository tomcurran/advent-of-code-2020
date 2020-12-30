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

        public static void Part2()
        {
            var inputs = File.ReadAllText("day16-input.txt")
                .Split(Environment.NewLine + Environment.NewLine);
            var fields = new Regex(@"(?<field>.+): (?<lowerStart>\d+)-(?<lowerEnd>\d+) or (?<upperStart>\d+)-(?<upperEnd>\d+)")
                .Matches(inputs[0])
                .Select(x => (field: x.Groups["field"].Value, lowerStart: int.Parse(x.Groups["lowerStart"].Value), lowerEnd: int.Parse(x.Groups["lowerEnd"].Value), upperStart: int.Parse(x.Groups["upperStart"].Value), upperEnd: int.Parse(x.Groups["upperEnd"].Value)));
            var myTickets = inputs[1].Split(Environment.NewLine)[1].Split(',').Select(int.Parse);
            var nearbyTickets = inputs[2].Split(Environment.NewLine).Skip(1).Select(x => x.Split(',').Select(int.Parse));

            var validNearbyTickets = nearbyTickets
                .Where(nearbyTicket => nearbyTicket.All(ticket => fields.Any(field => (ticket >= field.lowerStart && ticket <= field.lowerEnd) || (ticket >= field.upperStart && ticket <= field.upperEnd))));

            var fieldPositions = fields.Select(field => (field: field.field, position: -1)).ToList();

            do
            {
                var matchedPositions = fieldPositions
                    .Where(field => field.position != -1)
                    .Select(x => x.position);
                var unmatchedPositions = Enumerable.Range(0, myTickets.Count())
                    .Except(matchedPositions);
                foreach (var unmatchedPosition in unmatchedPositions)
                {
                    var ticketsInPosition = validNearbyTickets.Select(x => x.ElementAt(unmatchedPosition))
                        .Append(myTickets.ElementAt(unmatchedPosition));
                    var unmatchedFields = fieldPositions
                        .Where(field => field.position == -1)
                        .Select(x => x.field);
                    var validFields = fields
                        .Where(x => unmatchedFields.Contains(x.field))
                        .Where(field => ticketsInPosition.All(ticket => (ticket >= field.lowerStart && ticket <= field.lowerEnd) || (ticket >= field.upperStart && ticket <= field.upperEnd)));
                    if (validFields.Count() == 1)
                    {
                        var matchedField = validFields.First().field;
                        var index = fieldPositions.FindIndex(x => x.field == matchedField);
                        fieldPositions[index] = (matchedField, unmatchedPosition);
                    }
                }
            } while (fieldPositions.Any(x => x.position == -1));

            var departurePositions = fieldPositions
                .Where(x => x.field.StartsWith("departure"))
                .Select(x => x.position);

            var result = myTickets
                .Where((x, i) => departurePositions.Contains(i))
                .Aggregate(1L, (x, y) => x * y);

            Console.WriteLine(result);
        }
    }
}
