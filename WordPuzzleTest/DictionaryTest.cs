using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WordPuzzle;

namespace WordPuzzleTest
{
	[TestClass]
	public class DictionaryTest
	{
		[TestMethod]
		public void Dictionary_WordIsNotInDictionary_False()
		{
			// prepare
			var mockLogger = new Mock<ILogger>();
			var wordDictionary = new WordDictionary(mockLogger.Object);

			// execute
			wordDictionary.LoadDictionaryFile("");
			var result = wordDictionary.CheckIfWordExsits("1234");

			// assert
			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void Dictionary_WordIsInDictionary_True()
		{
			// prepare
			var mockLogger = new Mock<ILogger>();
			var wordDictionary = new WordDictionary(mockLogger.Object);

			// execute
			wordDictionary.LoadDictionaryFile("");
			var result = wordDictionary.CheckIfWordExsits("test");

			// assert
			Assert.AreEqual(true, result);
		}

		[TestMethod]
		public void Dictionary_DictionaryFilePathDoesNotExist_False()
		{
			// prepare
			var mockLogger = new Mock<ILogger>();
			var wordDictionary = new WordDictionary(mockLogger.Object);

			// execute
			var result = wordDictionary.LoadDictionaryFile("test");

			// assert
			Assert.AreEqual(false, result);
		}

		[TestMethod]
		public void Dictionary_UseDefaultDictionaryFile_True()
		{
			// prepare
			var mockLogger = new Mock<ILogger>();
			var wordDictionary = new WordDictionary(mockLogger.Object);

			// execute
			var result = wordDictionary.LoadDictionaryFile("");

			// assert
			Assert.AreEqual(true, result);
		}

	}
}
