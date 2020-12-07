using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    static class Day6
    {
        public static void Part1()
        {
            var input = File.ReadAllText("day6-input.txt");
            var answers = Regex.Split(input, @"\n\n");
            var yesAnswers = answers.Sum(answer => string.Join("", answer.Replace("\n", string.Empty).Distinct()).Length);
            Console.WriteLine(yesAnswers);
        }

        public static void Part2()
        {
            var input = File.ReadAllText("day6-input.txt");
            var answers = Regex.Split(input, @"\n\n");
            var yesAnswers = answers.Sum(answer => {
                var uniqueLetters = string.Join("", answer.Replace("\n", string.Empty).Distinct());
                var individualAnswers = answer.Split("\n").Where(x => !string.IsNullOrEmpty(x));
                return uniqueLetters.Count(letter => individualAnswers.All(individualAnswer => individualAnswer.Contains(letter)));
            });
            Console.WriteLine(yesAnswers);
        }
    }
}
