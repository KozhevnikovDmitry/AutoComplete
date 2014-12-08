using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace AutoComplete
{
    /// <summary>
    /// Static service, that completes input prefixes by provided vocabulary.
    /// </summary>
    public static class AutoComplete
    {
        /// <summary>
        /// Returns list of complete sets for prefixes by vocabulary.
        /// </summary>
        /// <param name="vocabulary">Vocabulary with words of completion</param>
        /// <param name="prefixes">Prefixes to complete</param>
        /// <param name="completeAmount">Amount of the completion set</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>
        /// Prints completion sets to console.
        /// Completes prefixes in certain order.
        /// </remarks>
        public static List<string> Complete(string[] vocabulary, string[] prefixes, int completeAmount)
        {
            if (vocabulary == null)
                throw new ArgumentNullException("vocabulary");

            if (prefixes == null)
                throw new ArgumentNullException("prefixes");

            if (completeAmount < 0)
                throw new ArgumentOutOfRangeException("completeAmount");


            var index = BuildIndex(vocabulary, completeAmount);
            
            // results accumulator
            var results = new List<string>();

            // for every prefix in certain order
            foreach (var prefix in prefixes)
            {
                // if prefix is presented in index
                if (index.ContainsKey(prefix))
                {
                    // take complete from index
                    var complete = index[prefix];
                    results.Add(complete);

                    // the slowest operation. Without it all flies.
                    Console.WriteLine(complete);
                }
            }

            return results;
        }

        /// <summary>
        /// Returns index for vocabulary
        /// </summary>
        /// <param name="vocabulary">Source vocabulary</param>
        /// <param name="completeAmount">Amount of the completion set</param>
        /// <remarks>
        /// Method builds some kind of string index for vocabulary. 
        /// That index has word as a key, and words completion(as single string) set as a value.
        /// </remarks>
        private static Dictionary<string, string> BuildIndex(string[] vocabulary, int completeAmount)
        {
            // index accumulator
            var vocabularyIndex = new List<KeyValuePair<string, string>>();

            // maximum word's length in vocabulary
            var wordMaxLength = vocabulary.Select(t => t.Length).Max();

            // for every possible word's length in vocabulary
            for (int len = 1; len <= wordMaxLength; len++)
            {
                // add to index completion set for words of 'len' length
                vocabularyIndex.AddRange(vocabulary.Where(t => t.Length >= len)
                                                   .GroupBy(t => t.Substring(0, len))
                                                   .ToDictionary(t => t.Key, t => t.Take(completeAmount)
                                                                                   .Aggregate(string.Empty, (acc, s) => acc + s + "\r\n")));
            }

            return vocabularyIndex.ToDictionary(t => t.Key, t => t.Value);
        }
    }
}
