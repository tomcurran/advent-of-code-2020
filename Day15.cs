using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    static class Day15
    {
        public static void Part1()
        {
            var spoken = File.ReadAllText("day15-input.txt")
                .Split(',')
                .Select(int.Parse)
                .ToList();

            for (var i = spoken.Count + 1; i <= 2020; i++)
            {
                var lastSpoken = spoken.Last();
                if (spoken.Count(x => x == lastSpoken) == 1)
                {
                    spoken.Add(0);
                }
                else
                {
                    var lastIndex = spoken.LastIndexOf(lastSpoken);
                    var secondLastIndex = spoken.LastIndexOf(lastSpoken, lastIndex - 1);
                    spoken.Add(lastIndex - secondLastIndex);
                }
            }

            Console.WriteLine(spoken.Last());
        }
    }
}
