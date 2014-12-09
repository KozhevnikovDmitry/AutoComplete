using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoComplete
{
    /// <summary>
    /// Statiñ service, that read from console and parse input lines to vocabulary and prefixes.
    /// </summary>
    /// <remarks>
    /// During parsing vocabulary is being ordered by word length descending and than by alphabetical order.
    /// It is small preparation for completion algorythm.
    /// </remarks>
    public static class InputParser
    {
        /// <summary>
        /// Reads and parses input lines and returns a pair of arrays of strings, that represent ordered vocabulary(Item1) and prefixes for completion(Item2).
        /// </summary>
        /// <exception cref="ArgumentException">Input lines is null or empty</exception>
        public static Tuple<string[], string[]> Parse()
        {
            var vocabularyAmount = Convert.ToInt32(Console.ReadLine());

            var vocabularyFrequencies = new Dictionary<string, int>();
            for (int i = 0; i < vocabularyAmount; i++)
            {
                var wordStr = Console.ReadLine();
                var split = wordStr.Split(' ');
                vocabularyFrequencies[split[0]] = Convert.ToInt32(split[1]);
            }

            var prefixAmount = Convert.ToInt32(Console.ReadLine());

            var prefixes = new List<string>();

            for (int i = 0; i < prefixAmount; i++)
            {
                prefixes.Add(Console.ReadLine());
            }

            // NOTE: vocabulary is ordred by frequency descending and that by alphabetical order
            // method returns only words, but not the frequencies
            string[] words =
                vocabularyFrequencies.OrderByDescending(t => t.Value).ThenBy(t => t.Key).Select(t => t.Key).ToArray();

            return new Tuple<string[], string[]>(words, prefixes.ToArray());
        }
    }
}