using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;

namespace AutoComplete.Tests
{
    [TestFixture]
    public class AcceptanceTests
    {
        [Test]
        public void Control_Simple_Test()
        {
            // Arrange
            var autoComplete = new Process();
            autoComplete.StartInfo.FileName = "AutoComplete.exe";
            autoComplete.StartInfo.UseShellExecute = false;
            autoComplete.StartInfo.RedirectStandardInput = true;
            autoComplete.StartInfo.RedirectStandardOutput = true;

            // Act
            autoComplete.Start();
            var stdin = autoComplete.StandardInput;
            var stdout = autoComplete.StandardOutput;
            stdin.WriteLine("5");
            stdin.WriteLine("kare 10");
            stdin.WriteLine("kanojo 20");
            stdin.WriteLine("karetachi 1");
            stdin.WriteLine("korosu 7");
            stdin.WriteLine("sakura 3");
            stdin.WriteLine("3");
            stdin.WriteLine("k");
            stdin.WriteLine("ka");
            stdin.WriteLine("kar");
            var result = stdout.ReadToEnd();

            // Assert
            Assert.AreEqual(result,
                "kanojo\r\nkare\r\nkorosu\r\nkaretachi\r\n\r\nkanojo\r\nkare\r\nkaretachi\r\n\r\nkare\r\nkaretachi\r\n\r\n");
        }

        [Test]
        public void Perfomance_Test()
        {
            // Arrange
            var inputLines = File.ReadAllLines("test.in");
            var autoComplete = new Process();
            autoComplete.StartInfo.FileName = "AutoComplete.exe";
            autoComplete.StartInfo.UseShellExecute = false;
            autoComplete.StartInfo.RedirectStandardInput = true;
            autoComplete.StartInfo.RedirectStandardOutput = true;

            // Act
            autoComplete.Start();
            var stdin = autoComplete.StandardInput;
            var stdout = autoComplete.StandardOutput;
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            foreach (var inputLine in inputLines)
            {
                stdin.WriteLine(inputLine);
            }
            var result = stdout.ReadToEnd();
            autoComplete.WaitForExit();

            // Assert
            Console.WriteLine("RunTime " + stopWatch.ElapsedMilliseconds);
            Assert.That(stopWatch.Elapsed.TotalSeconds < 10);
        }

        [Test]
        public void Correctness_Test()
        {
            // Arrange
            var correctResult = File.ReadAllText("test.out");
            var inputLines = File.ReadAllLines("test.in");
            var autoComplete = new Process();
            autoComplete.StartInfo.FileName = "AutoComplete.exe";
            autoComplete.StartInfo.UseShellExecute = false;
            autoComplete.StartInfo.RedirectStandardInput = true;
            autoComplete.StartInfo.RedirectStandardOutput = true;

            // Act
            autoComplete.Start();
            var stdin = autoComplete.StandardInput;
            var stdout = autoComplete.StandardOutput;
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var inputLine in inputLines)
            {
                stdin.WriteLine(inputLine);
            }
            var result = stdout.ReadToEnd();
            autoComplete.WaitForExit();

            // Assert
            Assert.AreEqual(result, correctResult);
            Console.WriteLine("RunTime " + stopWatch.ElapsedMilliseconds);
        }
    }
}