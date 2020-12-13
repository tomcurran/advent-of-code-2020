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

            char[,] preiousSeats;
            char[,] seats = new char[rows, columns];
            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    seats[row, column] = inputs.ElementAt(row).ElementAt(column);
                }
            }

            do
            {
                preiousSeats = (char[,])seats.Clone();
                for (var row = 0; row < rows; row++)
                {
                    for (var column = 0; column < columns; column++)
                    {
                        var previousSeat = preiousSeats[row, column];
                        var countAdjacentOccupied = CountAdjacentOccupied(preiousSeats, row, column);
                        if (previousSeat == 'L' && countAdjacentOccupied == 0)
                        {
                            seats[row, column] = '#';
                        }
                        else if (previousSeat == '#' && countAdjacentOccupied >= 4)
                        {
                            seats[row, column] = 'L';
                        }
                        else
                        {
                            seats[row, column] = preiousSeats[row, column];
                        }
                    }
                }
                
            } while (!seats.Cast<char>().SequenceEqual(preiousSeats.Cast<char>()));

            var occupied = seats.Cast<char>().Count(seat => seat == '#');
            Console.WriteLine(occupied);
        }

        private static int CountAdjacentOccupied(char[,] seats, int row, int column)
        {
            var rows = seats.GetLength(0);
            var columns = seats.GetLength(1);
            var count = 0;

            // up left
            if (row - 1 >= 0 && column - 1 >= 0 && seats[row - 1, column - 1] == '#')
            {
                count++;
            }

            // up
            if (row - 1 >= 0 && seats[row - 1, column] == '#')
            {
                count++;
            }

            // up right
            if (row - 1 >= 0 && column + 1 < columns && seats[row - 1, column + 1] == '#')
            {
                count++;
            }

            // right
            if (column + 1 < columns && seats[row, column + 1] == '#')
            {
                count++;
            }

            // down right
            if (row + 1 < rows && column + 1 < columns && seats[row + 1, column + 1] == '#')
            {
                count++;
            }

            // down
            if (row + 1 < rows && seats[row + 1, column] == '#')
            {
                count++;
            }

            // down left
            if (row + 1 < rows && column - 1 >= 0 && seats[row + 1, column - 1] == '#')
            {
                count++;
            }

            // left
            if (column - 1 >= 0 && seats[row, column - 1] == '#')
            {
                count++;
            }

            return count;
        }
    }
}
