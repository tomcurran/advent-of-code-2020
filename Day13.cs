using System;
using System.IO;
using System.Linq;

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

        public static void Part2()
        {
            var buses = File.ReadAllLines("day13-input-example.txt")
                .ElementAt(1)
                .Split(",")
                .Select((bus, index) => (bus: bus == "x" ? default : int.Parse(bus), delta: index))
                .Where(bus => bus.bus != default);

            var time = 1L;
            while (true)
            {
                if (buses.All(bus => (time + bus.delta) % bus.bus == 0))
                {
                    break;
                }
                time++;
            }
            Console.WriteLine(time);
        }
    }
}
