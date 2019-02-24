using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WordPuzzle;

namespace WordPuzzleTest
{
	[TestClass]
	public class WordSearchTest
	{
		[TestMethod]
		public void WordPuzzle_SolvePuzzle_2()
		{
			// prepare
			var mockLogger = new Mock<ILogger>();
			var wordDictionary = new WordDictionary(mockLogger.Object);
			var wordSearch = new WordSearch(wordDictionary, mockLogger.Object);

			// execute
			wordDictionary.LoadDictionaryFile("");
			wordSearch._firstWord = "span";
			wordSearch._lastWord = "spot";
			var results = wordSearch.GetFastestRoute();

			// assert
			Assert.AreEqual(2, results.Count - 1);
		}

		[TestMethod]
		public void WordPuzzle_SolvePuzzle_4()
		{
			// prepare
			var mockLogger = new Mock<ILogger>();
			var wordDictionary = new WordDictionary(mockLogger.Object);
			var wordSearch = new WordSearch(wordDictionary, mockLogger.Object);

			// execute
			wordDictionary.LoadDictionaryFile("");
			wordSearch._firstWord = "span";
			wordSearch._lastWord = "text";
			var results = wordSearch.GetFastestRoute();

			// assert
			Assert.AreEqual(4, results.Count - 1);
		}

		[TestMethod]
		public void WordPuzzle_SolvePuzzle_5()
		{
			// prepare
			var mockLogger = new Mock<ILogger>();
			var wordDictionary = new WordDictionary(mockLogger.Object);
			var wordSearch = new WordSearch(wordDictionary, mockLogger.Object);

			// execute
			wordDictionary.LoadDictionaryFile("");
			wordSearch._firstWord = "cool";
			wordSearch._lastWord = "span";
			var results = wordSearch.GetFastestRoute();

			// assert
			Assert.AreEqual(5, results.Count - 1);
		}

	}
}
