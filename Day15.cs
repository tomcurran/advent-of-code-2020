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

        public static void Part2()
        {
            var inputs = File.ReadAllText("day15-input.txt")
                .Split(',')
                .Select(int.Parse);
            var spoken = inputs
                .Select((input, index) => new { Input = input, Index = index })
                .ToDictionary(x => x.Input, x => (lastTurn: x.Index + 1,  secondLastTurn: -1));
            var lastSpoken = inputs.Last();

            for (var turn = spoken.Count + 1; turn <= 30000000; turn++)
            {
                var (lastSpokenLastTurn, lastSpokenSecondLastTurn) = spoken[lastSpoken];
                var speek = lastSpokenSecondLastTurn == -1 ? 0 : lastSpokenLastTurn - lastSpokenSecondLastTurn;
                if (!spoken.ContainsKey(speek))
                {
                    spoken[speek] = (-1, -1);
                }
                var (speekLastTurn, speekSecondLastTurn) = spoken[speek];
                spoken[speek] = (turn, speekLastTurn);
                lastSpoken = speek;
            }

            Console.WriteLine(lastSpoken);
        }
    }
}
