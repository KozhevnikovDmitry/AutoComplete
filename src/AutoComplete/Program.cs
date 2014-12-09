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
                // parse input lines
                var parsedInput = InputParser.Parse();

                // print first 10 completes for prefix 
                AutoComplete.Complete(parsedInput.Item1, parsedInput.Item2, 10);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
