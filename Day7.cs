using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    static class Day7
    {
        public static void Part1()
        {
            var inputs = File.ReadAllLines("day7-input.txt");
            var bags = inputs.ToDictionary(
                input => input.Split(" bags contain ")[0],
                input => new Regex(@"( (?<bag>[a-z ]+) bags?)+")
                            .Matches(input.Split(" bags contain ")[1])
                            .Select(x => x.Groups["bag"].Value)
                            .SkipWhile(x => x == "other"));

            var myBag = "shiny gold";
            var bagsContainingMyBag = bags.Count(bag => BagContainsMyBag(bags, myBag, bag.Value));
            Console.WriteLine(bagsContainingMyBag);

            bool BagContainsMyBag(Dictionary<string, IEnumerable<string>> bags, string myBag, IEnumerable<string> bagContents)
                => bagContents.Contains(myBag) || bagContents.Any(bag => BagContainsMyBag(bags, myBag, bags[bag]));
        }

        public static void Part2()
        {
            var inputs = File.ReadAllLines("day7-input.txt");
            var bags = inputs.ToDictionary(
                input => input.Split(" bags contain ")[0],
                input => new Regex(@"((?<quantity>\d) (?<bag>[a-z ]+) bags?)+")
                            .Matches(input.Split(" bags contain ")[1])
                            .Select(x => (quantity: int.Parse(x.Groups["quantity"].Value), bag: x.Groups["bag"].Value)));

            var myBag = "shiny gold";
            var myBagContents = bags[myBag];
            var myBagsTotal = GetBagsInside(bags, myBagContents);
            Console.WriteLine(myBagsTotal);

            int GetBagsInside(Dictionary<string, IEnumerable<(int quantity, string bag)>> bags, IEnumerable<(int quantity, string bag)> bagContents)
                => bagContents.Sum(bag => bag.quantity + (bag.quantity * GetBagsInside(bags, bags[bag.bag])));
        }
    }
}
