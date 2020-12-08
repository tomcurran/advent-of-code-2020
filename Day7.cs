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
                input =>
                {
                    var bagsRegex = new Regex(@"( (?<bag>[a-z ]+) bags?)+");
                    var bagsMatches = bagsRegex.Matches(input.Split(" bags contain ")[1]);
                    var bagContents = bagsMatches.Select(x => x.Groups["bag"].Value).SkipWhile(x => x == "other");
                    return bagContents;
                });

            var myBag = "shiny gold";
            var bagsContainingMyBag = bags.Count(bag => BagContainsMyBag(bags, myBag, bag.Value));
            Console.WriteLine(bagsContainingMyBag);

            bool BagContainsMyBag(Dictionary<string, IEnumerable<string>> allBags, string myBag, IEnumerable<string> bagContents)
                => bagContents.Contains(myBag) || bagContents.Any(bag => BagContainsMyBag(allBags, myBag, allBags[bag]));
        }
    }
}
