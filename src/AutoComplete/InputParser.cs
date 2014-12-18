using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoComplete
{
    /// <summary>
    /// Static service, that parse input lines to vocabulary and prefixes.
    /// </summary>
    /// <remarks>
    /// During parsing vocabulary is being ordered by word length descending and than by alphabetical order.
    /// It is small preparation for completion algorythm.
    /// </remarks>
    public static class InputParser
    {
        /// <summary>
        /// Parses input lines and returns a pair of arrays of strings, that represent ordered vocabulary(Item1) and prefixes for completion(Item2).
        /// </summary>
        /// <param name="reader">Reader of inputs</param>
        /// <exception cref="ArgumentException">Input lines is null or empty</exception>
        public static Tuple<string[], string[]> Parse(TextReader reader)
        {
            // parse amount of vocabulary
            var vocabularyAmount = Convert.ToInt32(reader.ReadLine());

            // parse the vocabulary with frequencies
            var vocabularyFrequencies = new Dictionary<string, int>();
            for (int i = 0; i < vocabularyAmount; i++)
            {
                var freq = reader.ReadLine().Split(' ');
                vocabularyFrequencies[freq[0]] = Convert.ToInt32(freq[1]);
            }

            // parse amount of prefixes
            int prefixAmount = Convert.ToInt32(reader.ReadLine());

            // parse prefixes
            var prefixes = new List<string>();
            for (int i = 0; i < prefixAmount; i++)
            {
                prefixes.Add(reader.ReadLine());
            }

            // order vocabulary by word length descending and than by alphabetical order. Take only words
            var words = vocabularyFrequencies.OrderByDescending(t => t.Value).ThenBy(t => t.Key).Select(t => t.Key).ToArray();

            // results
            return new Tuple<string[], string[]>(words, prefixes.ToArray());
        }
    }
}