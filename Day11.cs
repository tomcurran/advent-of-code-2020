using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    static class Day11
    {
        public static void Part1()
        {
            var inputs = File.ReadAllLines("day11-input.txt");
            var rows = inputs.Length;
            var columns = inputs.First().Length;

            var seats = inputs.Select(a => a.ToArray()).ToArray();
            var preiousSeats = new char[seats.Length][];

            do
            {
                preiousSeats = seats.Select(a => a.ToArray()).ToArray();
                for (var row = 0; row < rows; row++)
                {
                    for (var column = 0; column < columns; column++)
                    {
                        var previousSeat = preiousSeats[row][column];
                        var countAdjacentOccupied = CountAdjacentOccupied(preiousSeats, row, column);
                        if (previousSeat == 'L' && countAdjacentOccupied == 0)
                        {
                            seats[row][column] = '#';
                        }
                        else if (previousSeat == '#' && countAdjacentOccupied >= 4)
                        {
                            seats[row][column] = 'L';
                        }
                        else
                        {
                            seats[row][column] = preiousSeats[row][column];
                        }
                    }
                }
            } while (!Equal(seats, preiousSeats));

            Console.WriteLine(CountOccupied(seats));
        }

        private static int CountOccupied(char[][] seats)
        {
            var rows = seats.Length;
            var columns = seats.First().Length;
            var count = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (seats[row][column] == '#')
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private static int CountAdjacentOccupied(char[][] seats, int row, int column)
        {
            var columns = seats[0].Length;
            var rows = seats.Length;
            var count = 0;

            // up left
            if (row - 1 >= 0 && column - 1 >= 0 && seats[row - 1][column - 1] == '#')
            {
                count++;
            }

            // up
            if (row - 1 >= 0 && seats[row - 1][column] == '#')
            {
                count++;
            }

            // up right
            if (row - 1 >= 0 && column + 1 < columns && seats[row - 1][column + 1] == '#')
            {
                count++;
            }

            // right
            if (column + 1 < columns && seats[row][column + 1] == '#')
            {
                count++;
            }

            // down right
            if (row + 1 < rows && column + 1 < columns && seats[row + 1][column + 1] == '#')
            {
                count++;
            }

            // down
            if (row + 1 < rows && seats[row + 1][column] == '#')
            {
                count++;
            }

            // down left
            if (row + 1 < rows && column - 1 >= 0 && seats[row + 1][column - 1] == '#')
            {
                count++;
            }

            // left
            if (column - 1 >= 0 && seats[row][column - 1] == '#')
            {
                count++;
            }

            return count;
        }

        private static bool Equal(char[][] first, char[][] second)
        {
            var rows = first.Length;
            var columns = first.First().Length;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (first[row][column] != second[row][column])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
