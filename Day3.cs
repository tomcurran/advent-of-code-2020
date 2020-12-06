using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    static class Day3
    {
        public static void Part1()
        {
            const int right = 3;
            const int down = 1;
            var inputs = File.ReadAllLines("day3-input.txt");
            var inputColumns = inputs.First().Length;
            var column = right;
            var row = down;
            var trees = 0;
            while (row < inputs.Length)
            {
                var coord = inputs[row].ElementAt(column % inputColumns);
                if (coord == '#')
                {
                    trees++;
                }
                row += down;
                column += right;
            }
            Console.WriteLine(trees);
        }
    }
}
