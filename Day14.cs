using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    static class Day14
    {
        public static void Part1()
        {
            var input = File.ReadAllText("day14-input.txt");
            var regex = new Regex(@"mask = (?<mask>.+)|mem\[(?<address>\d+)\] = (?<value>\d+)");
            var memory = new Dictionary<int, ulong>();
            ulong zerosMask = 0, onesMask = 0;
            foreach (Match match in regex.Matches(input))
            {
                if (!string.IsNullOrEmpty(match.Groups["mask"].Value))
                {
                    var mask = match.Groups["mask"].Value;
                    zerosMask = Convert.ToUInt64(mask.Replace('X', '0'), 2);
                    onesMask = Convert.ToUInt64(mask.Replace('X', '1'), 2);
                }
                else
                {
                    var address = int.Parse(match.Groups["address"].Value);
                    var value = ulong.Parse(match.Groups["value"].Value);
                    value |= zerosMask;
                    value &= onesMask;
                    memory[address] = value;
                }
            }

            var sumInMemory = memory.Values.Aggregate((x, y) => x + y);
            Console.WriteLine(sumInMemory);
        }

        public static void Part2()
        {
            var input = File.ReadAllText("day14-input.txt");
            var regex = new Regex(@"mask = (?<mask>.+)|mem\[(?<address>\d+)\] = (?<value>\d+)");

            var memory = new Dictionary<ulong, ulong>();
            ulong rulesMask = 0;
            IEnumerable<int> floatingIndices = null;
            IEnumerable<string> floatingRange = null;

            foreach (Match match in regex.Matches(input))
            {
                if (!string.IsNullOrEmpty(match.Groups["mask"].Value))
                {
                    var mask = match.Groups["mask"].Value;
                    floatingIndices = mask
                        .Select((x, index) => x == 'X' ? index : -1)
                        .Where(x => x != -1);
                    floatingRange = Enumerable.Range(0, (int)Math.Pow(2, floatingIndices.Count()))
                        .Select(x => Convert.ToString((long)x, 2).PadLeft(floatingIndices.Count(), '0'));
                    rulesMask = Convert.ToUInt64(mask.Replace('X', '0'), 2);
                }
                else
                {
                    var address = ulong.Parse(match.Groups["address"].Value);
                    var value = ulong.Parse(match.Groups["value"].Value);

                    address |= rulesMask;

                    var partialAddress = Convert.ToString((long)address, 2).PadLeft(36, '0');
                    var memoryAddresses = floatingRange.Select(x =>
                    {
                        var partialAddressCharacters = partialAddress.ToCharArray();
                        for (var i = 0; i < floatingIndices.Count(); i++)
                        {
                            partialAddressCharacters[floatingIndices.ElementAt(i)] = x[i];
                        }
                        return new string(partialAddressCharacters);
                    }).Select(x => Convert.ToUInt64(x, 2));

                    foreach (var memoryAddress in memoryAddresses)
                    {
                        memory[memoryAddress] = value;
                    }
                }
            }

            var sumInMemory = memory.Values.Aggregate((x, y) => x + y);
            Console.WriteLine(sumInMemory);
        }

        public static void Part2NotWorking()
        {
            var input = File.ReadAllText("day14-input.txt");
            var regex = new Regex(@"mask = (?<mask>.+)|mem\[(?<address>\d+)\] = (?<value>\d+)");

            var memory = new Dictionary<ulong, ulong>();
            ulong rulesMask = 0, floatingMask = 0;
            IEnumerable<ulong> floatingMasks = null;

            foreach (Match match in regex.Matches(input))
            {
                if (!string.IsNullOrEmpty(match.Groups["mask"].Value))
                {
                    var mask = match.Groups["mask"].Value;
                    rulesMask = Convert.ToUInt64(mask.Replace('X', '0'), 2);
                    floatingMask = Convert.ToUInt64(mask.Replace('1', '0').Replace('X', '1'), 2);

                    var floatingRange = mask.Reverse()
                        .Select((x, index) => x == 'X' ? index : -1)
                        .Where(index => index != -1)
                        .Select((x, index) => ((ulong)Math.Pow(2, index), (ulong)(Math.Pow(2, x) - Math.Pow(2, index))));

                    floatingMasks = Enumerable.Range(0, (int)Math.Pow(2, floatingRange.Count()))
                        .Select(x =>
                        {
                            var range = floatingRange.Where(y => (ulong)x >= y.Item1);
                            if (!range.Any())
                            {
                                return (ulong)0;
                            }
                            return range.Select(y => y.Item2).Aggregate((x, y) => x + y) + (ulong)x;
                        })
                        .Select(x => x & floatingMask);
                }
                else
                {
                    var address = ulong.Parse(match.Groups["address"].Value);
                    var value = ulong.Parse(match.Groups["value"].Value);

                    address |= rulesMask;
                    address &= ~floatingMask;
                    var memoryAddresses = floatingMasks.Select(x => address | x);

                    foreach (var memoryAddress in memoryAddresses)
                    {
                        memory[memoryAddress] = value;
                    }
                }
            }

            var sumInMemory = memory.Values.Aggregate((x, y) => x + y);
            Console.WriteLine(sumInMemory);
        }
    }
}
