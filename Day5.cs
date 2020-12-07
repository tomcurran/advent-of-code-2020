using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    static class Day5
    {
        public static void Part1()
        {
            var seatIds = GetSeatIds();
            Console.WriteLine(seatIds.Max());
        }

        public static void Part2()
        {
            var seatIds = GetSeatIds();
            var mySeatId = Enumerable.Range(seatIds.Min(), seatIds.Max())
                .Except(seatIds)
                .Single(seat => seatIds.Contains(seat - 1) && seatIds.Contains(seat + 1));
            Console.WriteLine(mySeatId);
        }

        private static IEnumerable<int> GetSeatIds()
        {
            return File.ReadAllLines("day5-input.txt")
                .Select(input =>
                {
                    var rowStart = 0;
                    var rowEnd = 127;
                    var columnStart = 0;
                    var columnEnd = 7;
                    foreach (var partition in input)
                    {
                        if (partition == 'F')
                        {
                            rowEnd -= (rowEnd - rowStart + 1) / 2;
                        }
                        else if (partition == 'B')
                        {
                            rowStart += (rowEnd - rowStart + 1) / 2;
                        }
                        else if (partition == 'L')
                        {
                            columnEnd -= (columnEnd - columnStart + 1) / 2;
                        }
                        else if (partition == 'R')
                        {
                            columnStart += (columnEnd - columnStart + 1) / 2;
                        }
                    }

                    return (rowStart * 8) + columnStart;
                });
        }
    }
}
