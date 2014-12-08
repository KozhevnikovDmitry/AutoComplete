using System;
using System.Linq;
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
            Assert.Throws<ArgumentNullException>(() => AutoComplete.Complete(null, new string[0], 0));
        }

        [Test]
        public void Complete_PrefixesIsNull_Fails_Test()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => AutoComplete.Complete(new string[0], null, 0));
        }
        
        [Test]
        public void Complete_CompleteAmountIsNegative_Fails_Test()
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => AutoComplete.Complete(new string[0], new string[0], -1));
        }

        [Test]
        public void Complete_ConcatCompletesInCertainOrderOfWords_Test()
        {
            // Arrange
            var words = new[] { "AAA", "AA", "A" };
            var prefixes = new[] { "A" };

            // Act
            var completes = AutoComplete.Complete(words, prefixes, 3);

            // Assert
            Assert.AreEqual(completes[0], "AAA\r\nAA\r\nA\r\n");
        }

        [Test]
        public void Complete_TakeOnlyThatStartsWithPrefix_Test()
        {
            // Arrange
            var words = new[] { "A", "AA", "B" };
            var prefixes = new[] { "B" };

            // Act
            var completes = AutoComplete.Complete(words, prefixes, 2);

            // Assert
            Assert.AreEqual(completes[0], "B\r\n");
        }

        [Test]
        public void Complete_CompletesInCertainOrderOfPrefixes_Test()
        {
            // Arrange
            var words = new[] { "A", "AA", "B", "BB" };
            var prefixes = new[] { "B", "A" };

            // Act
            var completes = AutoComplete.Complete(words, prefixes, 2);

            // Assert
            Assert.AreEqual(completes.First(), "B\r\nBB\r\n");
            Assert.AreEqual(completes.Last(), "A\r\nAA\r\n");
        }

        [Test]
        public void Complete_DoNotCompleteTooLongPrefixes_Test()
        {
            // Arrange
            var words = new[] { "A", "AA", "AAA" };
            var prefixes = new[] { "AAAA" };

            // Act
            var completes = AutoComplete.Complete(words, prefixes, 2);

            // Assert
            Assert.IsEmpty(completes);
        }
        
        [Test]
        public void Complete_DoNotCompletePrefixesMissingInVocabulary_Test()
        {
            // Arrange
            var words = new[] { "A", "AA", "AAA" };
            var prefixes = new[] { "ZZZ" };

            // Act
            var completes = AutoComplete.Complete(words, prefixes, 2);

            // Assert
            Assert.IsEmpty(completes);
        }

        [Test]
        public void Complete_TakeOnlyCompleteCount_Test()
        {
            // Arrange
            var words = new[] {"A", "AA", "AAA"};
            var prefixes = new[] {"A"};

            // Act
            var completes = AutoComplete.Complete(words, prefixes, 2);

            // Assert
            Assert.AreEqual(completes[0], "A\r\nAA\r\n");
        }

        [Test]
        public void Complete_TakeOnlyCompleteCountOrLess_Test()
        {
            // Arrange
            var words = new[] { "A", "AA", "AAA" };
            var prefixes = new[] { "A" };

            // Act
            var completes = AutoComplete.Complete(words, prefixes, 4);

            // Assert
            Assert.AreEqual(completes[0], "A\r\nAA\r\nAAA\r\n");
        }

        [Test]
        public void Complete_CompleteRepeatPrefixexAsWell_Test()
        {
            // Arrange
            var words = new[] { "A", "AA", "B", "BB"};
            var prefixes = new[] { "A", "B", "A", "B" };

            // Act
            var completes = AutoComplete.Complete(words, prefixes, 2);

            // Assert
            Assert.AreEqual(completes.Count, 4);
            Assert.AreEqual(completes[0], "A\r\nAA\r\n");
            Assert.AreEqual(completes[1], "B\r\nBB\r\n");
            Assert.AreEqual(completes[2], "A\r\nAA\r\n");
            Assert.AreEqual(completes[3], "B\r\nBB\r\n");
        }
    }
}
