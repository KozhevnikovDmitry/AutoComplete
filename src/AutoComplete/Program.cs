using System;
using System.Collections.Generic;
using System.IO;

namespace AutoComplete
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // read input
                var inputLines = ReadInpupLines(args);

                // parse input lines
                var parsedInput = InputParser.Parse(inputLines);

                // print first 10 completes for prefix 
                AutoComplete.Complete(parsedInput.Item1, parsedInput.Item2, 10);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Reads and returns input lines
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        static string[] ReadInpupLines(string[] args)
        {
            // too many arguments
            if (args.Length > 1)
            {
                throw new ArgumentOutOfRangeException("args");
            }

            string[] inputLines;
            if (args.Length == 1)
            {
                if (!File.Exists(args[0]))
                {
                    throw new FileNotFoundException(args[0]);
                }

                // read from file
                inputLines = File.ReadAllLines(args[0]);
            }
            else
            {
                // read from keyboard
                inputLines = ReadFromKeyboard();
            }

            return inputLines;
        }

        /// <summary>
        /// Reads and returns all user's input lines from keyboard
        /// </summary>
        static string[] ReadFromKeyboard()
        {
            var inputs = new List<string>();
            var inputIsOver = false;
            while (!inputIsOver)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    inputIsOver = true;
                }
                else
                {
                    inputs.Add(input);
                }
            }
            return inputs.ToArray();
        }
    }
}
