using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    static class Day10
    {
        public static void Part1()
        {
            var numbers = File.ReadAllLines("day10-input.txt")
                .Select(input => long.Parse(input))
                .Prepend(0)
                .OrderBy(x => x);

            var oneCount = 0;
            var threeCount = 1;

            for (var i = 0; i < numbers.Count() - 1; i++)
            {
                var diff = numbers.ElementAt(i + 1) - numbers.ElementAt(i);
                if (diff == 1)
                {
                    oneCount++;
                }
                else if (diff == 3)
                {
                    threeCount++;
                }
            }

            Console.WriteLine(oneCount * threeCount);
        }

        public static void Part2()
        {
            var numbers = File.ReadAllLines("day10-input-example.txt")
                .Select(input => long.Parse(input))
                .Prepend(0)
                .OrderByDescending(x => x);

            Console.WriteLine(Sum(numbers, numbers.Count(), 0));

            static long Sum(IOrderedEnumerable<long> numbers, int numbersCount, int index)
            {
                if (index == numbersCount - 1)
                {
                    return 1;
                }

                var sumNumberPlus1 = 0L;
                var sumNumberPlus2 = 0L;
                var sumNumberPlus3 = 0L;
                var currentNumber = numbers.ElementAt(index);

                if (index + 1 < numbersCount && currentNumber - numbers.ElementAt(index + 1) >= 1 && currentNumber - numbers.ElementAt(index + 1) <= 3)
                {
                    sumNumberPlus1 = Sum(numbers, numbersCount, index + 1);
                }

                if (index + 2 < numbersCount && currentNumber - numbers.ElementAt(index + 2) >= 1 && currentNumber - numbers.ElementAt(index + 2) <= 3)
                {
                    sumNumberPlus2 = Sum(numbers, numbersCount, index + 2);
                }

                if (index + 3 < numbersCount && currentNumber - numbers.ElementAt(index + 3) >= 1 && currentNumber - numbers.ElementAt(index + 3) <= 3)
                {
                    sumNumberPlus3 = Sum(numbers, numbersCount, index + 3);
                }

                return sumNumberPlus1 + sumNumberPlus2 + sumNumberPlus3;
            }
        }
    }
}
