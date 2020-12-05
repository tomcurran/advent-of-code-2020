using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    class Day1
    {
        static void Main(string[] args)
        {
            PartTwo();
        }
  
        static void PartOne()
        {
            var inputs = File.ReadAllLines("day1-input.txt")
                .Select(int.Parse)
                .ToHashSet();
            foreach (var input in inputs)
            {
                var find = 2020 - input;
                if (inputs.Contains(find))
                {
                    Console.WriteLine(input * find);
                    break;
                }
            }
        }

        static void PartTwo()
        {
            var inputs = File.ReadAllLines("day1-input.txt")
                .Select(int.Parse)
                .ToHashSet();
            foreach (var input in inputs)
            {
                foreach (var input2 in inputs)
                {
                    var find = 2020 - input - input2;
                    if (inputs.Contains(find))
                    {
                        Console.WriteLine(input * input2 * find);
                        return;
                    }
                }
            }
        }
    }
}
