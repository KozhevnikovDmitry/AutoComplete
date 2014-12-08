using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoComplete
{
    /// <summary>
    /// Statiñ service, that parse input lines to vocabulary and prefixes.
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
        /// <exception cref="ArgumentException">Input lines is null or empty</exception>
        /// <exception cref="UnexpectedInputFormatException">Input lines has unexpected format</exception>
        /// <exception cref="InconsistentInputDataException">Input lines has unexpected format or inconsistent data</exception>
        public static Tuple<string[], string[]> Parse(string[] inputLines)
        {
            if (inputLines == null || inputLines.Length == 0)
            {
                throw new ArgumentException("inputLines");
            }

            // parsing amounts
            int vocabularyAmount = GetVocabularyAmount(inputLines);
            int prefixAmount = GetPrefixAmount(inputLines, vocabularyAmount);

            // parsing vocabulary and prefixes
            var words = GetVocabulary(inputLines, vocabularyAmount);
            var prefixes = GetPrefixes(inputLines, prefixAmount, vocabularyAmount);

            return new Tuple<string[], string[]>(words, prefixes);
        }

        /// <summary>
        /// Returns the amount of the vocabulary.
        /// </summary>
        /// <param name="inputLines">Input lines for parsing</param>
        private static int GetVocabularyAmount(string[] inputLines)
        { 
            int vocabularyAmount;
            if (!int.TryParse(inputLines[0], out vocabularyAmount) || vocabularyAmount <= 0)
            {
                throw new UnexpectedInputFormatException();
            }

            return vocabularyAmount;
        }

        /// <summary>
        /// Returns the amount of the prefixes for completion.
        /// </summary>
        /// <param name="inputLines">Input lines for parsing</param>
        /// <param name="vocabularyAmount">Amount of the vacubulary</param>
        private static int GetPrefixAmount(string[] inputLines, int vocabularyAmount)
        {
            int prefixAmount;

            // input lines length must be more then declared vocabulary amount + 2 to contain prefixes
            if (inputLines.Length < vocabularyAmount + 2)
            {
                throw new InconsistentInputDataException();
            }

            if (!int.TryParse(inputLines[vocabularyAmount + 1], out prefixAmount) || prefixAmount <= 0)
            {
                throw new UnexpectedInputFormatException();
            }

            // input lines length must be equal to declared vocabulary amount + prefixes amount + 2
            if (inputLines.Length != vocabularyAmount + prefixAmount + 2)
            {
                throw new InconsistentInputDataException();
            }

            return prefixAmount;
        }
        
        /// <summary>
        /// Returns the ordred vocabulary.
        /// </summary>
        /// <param name="inputLines">Input lines for parsing</param>
        /// <param name="vocabularyAmount">Amount of the vacubulary</param>
        private static string[] GetVocabulary(string[] inputLines, int vocabularyAmount)
        {
            var vocabularyFrequencies = new Dictionary<string, int>();
            for (int i = 1; i < vocabularyAmount + 1; i++)
            {
                var split = inputLines[i].Split(' ');

                if (split.Length != 2)
                {
                    throw new UnexpectedInputFormatException();
                }

                int freq;

                if (!int.TryParse(split[1], out freq) || freq < 0)
                {
                    throw new UnexpectedInputFormatException();
                }

                vocabularyFrequencies[split[0]] = freq;
            }

            // real vocabulary amount must be equal to declared
            if (vocabularyFrequencies.Count != vocabularyAmount)
            {
                throw new InconsistentInputDataException();
            }

            // NOTE: vocabulary is ordred by frequency descending and that by alphabetical order
            // method returns only words, but not the frequencies
            return vocabularyFrequencies.OrderByDescending(t => t.Value).ThenBy(t => t.Key).Select(t => t.Key).ToArray();
        }

        /// <summary>
        /// Returns the array of prefixes for completion.
        /// </summary>
        /// <param name="inputLines">Input lines for parsing</param>
        /// <param name="prefixesAmount">Amount of prefixes</param>
        /// <param name="vocabularyAmount">Amount of the vacubulary</param>
        private static string[] GetPrefixes(string[] inputLines, int prefixesAmount, int vocabularyAmount)
        {
            var prefixes = new List<string>();

            for (int i = vocabularyAmount + 2; i < vocabularyAmount + 2 + prefixesAmount; i++)
            {
                prefixes.Add(inputLines[i]);
            }

            // real prefixes amount must be equal to declared
            if (prefixes.Count != prefixesAmount)
            {
                throw new InconsistentInputDataException();
            }

            return prefixes.ToArray();
        }
    }

    /// <summary>
    /// The exception, that throws from <see cref="InputParser"/>, when input lines has unexpected format.
    /// </summary>
    public class UnexpectedInputFormatException : ApplicationException
    {
        public UnexpectedInputFormatException()
            : base("Input lines has unexpected format")
        {
            
        }
    }

    /// <summary>
    /// The exception, that throws from <see cref="InputParser"/>, when input lines contains inconsistent data for expcected format.
    /// </summary>
    public class InconsistentInputDataException : ApplicationException
    {
        public InconsistentInputDataException()
            : base("Input lines contains inconsistent data for expcected format")
        {

        }
    }
}