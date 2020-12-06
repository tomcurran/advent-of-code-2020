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
            var trees = GetTrees(right, down);
            Console.WriteLine(trees);
        }

        public static void Part2()
        {
            var directions = new []
            {
                (right: 1, down: 1),
                (right: 3, down: 1),
                (right: 5, down: 1),
                (right: 7, down: 1),
                (right: 1, down: 2),
            };
            var trees = directions
                .Select(direction => GetTrees(direction.right, direction.down))
                .Aggregate(1L, (x, y) => x * y);
            Console.WriteLine(trees);
        }

        private static int GetTrees(int right, int down)
        {
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
            return trees;
        }
    }
}
