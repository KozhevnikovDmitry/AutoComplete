using System;
using System.IO;
using NUnit.Framework;

namespace AutoComplete.Tests
{
    [TestFixture]
    public class AutoCompleteTests
    {
        [Test]
        public void Complete_VocabularyIsNull_Fails_Test()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => AutoComplete.Complete(null, new string[0], 0, new StringWriter()));
        }

        [Test]
        public void Complete_PrefixesIsNull_Fails_Test()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => AutoComplete.Complete(new string[0], null, 0, new StringWriter()));
        }

        [Test]
        public void Complete_CompleteAmountIsNegative_Fails_Test()
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => AutoComplete.Complete(new string[0], new string[0], -1, new StringWriter()));
        }

        [Test]
        public void Complete_ConcatCompletesInCertainOrderOfWords_Test()
        {
            // Arrange
            var words = new[] { "AAA", "AA", "A" };
            var prefixes = new[] { "A" };
            var completes = new StringWriter();

            // Act
            AutoComplete.Complete(words, prefixes, 3, completes);

            // Assert
            Assert.AreEqual(completes.ToString(), "AAA\r\nAA\r\nA\r\n\r\n");
        }

        [Test]
        public void Complete_TakeOnlyThatStartsWithPrefix_Test()
        {
            // Arrange
            var words = new[] { "A", "AA", "B" };
            var prefixes = new[] { "B" };
            var completes = new StringWriter();

            // Act
            AutoComplete.Complete(words, prefixes, 2, completes);

            // Assert
            Assert.AreEqual(completes.ToString(), "B\r\n\r\n");
        }

        [Test]
        public void Complete_CompletesInCertainOrderOfPrefixes_Test()
        {
            // Arrange
            var words = new[] { "A", "AA", "B", "BB" };
            var prefixes = new[] { "B", "A" };
            var completes = new StringWriter();

            // Act
            AutoComplete.Complete(words, prefixes, 2, completes);

            // Assert
            Assert.AreEqual(completes.ToString(), "B\r\nBB\r\n" + "\r\n" + "A\r\nAA\r\n\r\n");
        }

        [Test]
        public void Complete_DoNotCompleteTooLongPrefixes_Test()
        {
            // Arrange
            var words = new[] { "A", "AA", "AAA" };
            var prefixes = new[] { "AAAA" };
            var completes = new StringWriter();

            // Act
            AutoComplete.Complete(words, prefixes, 2, completes);

            // Assert
            Assert.IsEmpty(completes.ToString());
        }

        [Test]
        public void Complete_DoNotCompletePrefixesMissingInVocabulary_Test()
        {
            // Arrange
            var words = new[] { "A", "AA", "AAA" };
            var prefixes = new[] { "ZZZ" };
            var completes = new StringWriter();

            // Act
            AutoComplete.Complete(words, prefixes, 2, completes);

            // Assert
            Assert.IsEmpty(completes.ToString());
        }

        [Test]
        public void Complete_TakeOnlyCompleteCount_Test()
        {
            // Arrange
            var words = new[] { "A", "AA", "AAA" };
            var prefixes = new[] { "A" };
            var completes = new StringWriter();

            // Act
            AutoComplete.Complete(words, prefixes, 2, completes);

            // Assert
            Assert.AreEqual(completes.ToString(), "A\r\nAA\r\n\r\n");
        }

        [Test]
        public void Complete_TakeOnlyCompleteCountOrLess_Test()
        {
            // Arrange
            var words = new[] { "A", "AA", "AAA" };
            var prefixes = new[] { "A" };
            var completes = new StringWriter();

            // Act
            AutoComplete.Complete(words, prefixes, 4, completes);

            // Assert
            Assert.AreEqual(completes.ToString(), "A\r\nAA\r\nAAA\r\n\r\n");
        }

        [Test]
        public void Complete_CompleteRepeatPrefixexAsWell_Test()
        {
            // Arrange
            var words = new[] { "A", "AA", "B", "BB" };
            var prefixes = new[] { "A", "B", "A", "B" };
            var completes = new StringWriter();

            // Act
            AutoComplete.Complete(words, prefixes, 2, completes);

            // Assert
            Assert.AreEqual(completes.ToString(), "A\r\nAA\r\n" + "\r\n" + "B\r\nBB\r\n" + "\r\n" + "A\r\nAA\r\n" + "\r\n" + "B\r\nBB\r\n" + "\r\n");
        }
    }
}
