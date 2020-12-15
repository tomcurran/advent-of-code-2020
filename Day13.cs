using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    static class Day13
    {
        public static void Part1()
        {
            var inputs = File.ReadAllLines("day13-input.txt");
            var startTime = int.Parse(inputs.ElementAt(0));
            var buses = inputs.ElementAt(1)
                            .Split(",")
                            .Where(bus => bus != "x")
                            .Select(bus => int.Parse(bus));
            var time = startTime;
            int bus;
            while (true)
            {
                bus = buses.FirstOrDefault(bus => time % bus == 0);
                if (bus != default)
                {
                    break;
                }
                time++;
            }
            var waitingTime = time - startTime;
            Console.WriteLine(waitingTime * bus);
        }
    }
}
