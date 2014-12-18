using System;
using System.IO;

namespace AutoComplete
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // parse input lines
                var parsedInput = InputParser.Parse(Console.In);
                
                // generate completions
                AutoComplete.Complete(parsedInput.Item1, parsedInput.Item2, 10, Console.Out);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
