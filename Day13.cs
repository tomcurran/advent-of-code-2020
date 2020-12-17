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
            var buses = File.ReadAllLines("day13-input.txt")
                .ElementAt(1)
                .Split(",")
                .Select((bus, index) => (bus: bus == "x" ? default : int.Parse(bus), delta: index))
                .Where(bus => bus.bus != default);

            var index = 1;
            long timeIncrement = buses.ElementAt(0).bus;
            long time = 0;
            while (true)
            {
                time += timeIncrement;
                var bus = buses.ElementAt(index);
                if ((time + bus.delta) % bus.bus == 0)
                {
                    timeIncrement *= bus.bus;
                    if (++index >= buses.Count())
                    {
                        break;
                    }
                }
            }
            Console.WriteLine(time);
        }
    }
}
