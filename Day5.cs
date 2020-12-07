using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    static class Day5
    {
        public static void Part1()
        {
            var seatIds = File.ReadAllLines("day5-input.txt")
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
            Console.WriteLine(seatIds.Max());
        }
    }
}
