using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoComplete
{
    /// <summary>
    /// Static service, that completes input prefixes by provided vocabulary.
    /// </summary>
    public static class AutoComplete
    {
        /// <summary>
        /// Writes completions for prefixes with provided writer.
        /// </summary>
        /// <param name="vocabulary">Vocabulary with words of completion</param>
        /// <param name="prefixes">Prefixes to complete</param>
        /// <param name="completeAmount">Amount of the completion set</param>
        /// <param name="writer">Writer for completion results</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <remarks>
        /// Prints completion sets to console.
        /// Completes prefixes in certain order.
        /// </remarks>
        public static void Complete(string[] vocabulary, string[] prefixes, int completeAmount, TextWriter writer)
        {
            if (vocabulary == null)
                throw new ArgumentNullException("vocabulary");

            if (prefixes == null)
                throw new ArgumentNullException("prefixes");

            if (completeAmount < 0)
                throw new ArgumentOutOfRangeException("completeAmount");

            // build index for vocaulary
            var index = BuildIndex(vocabulary, completeAmount);
            
            // write completion
            WriteCompletions(index, prefixes, writer);
        }

        /// <summary>
        /// Writes completions for prefixes with provided writer.
        /// </summary>
        /// <param name="index">Vocabulary index</param>
        /// <param name="prefixes">Prefixes to complete</param>
        /// <param name="writer">Writer for completion results</param>
        /// <remarks>
        /// Prints completion sets to console.
        /// Completes prefixes in certain order.
        /// </remarks>
        internal static void WriteCompletions(Dictionary<string, string> index, string[] prefixes, TextWriter writer)
        {
            // for every prefix in certain order
            foreach (var prefix in prefixes)
            {
                if (index.ContainsKey(prefix))
                {
                    writer.WriteLine(index[prefix]);
                }
            }
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
        internal static Dictionary<string, string> BuildIndex(string[] vocabulary, int completeAmount)
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
