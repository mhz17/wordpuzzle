using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordPuzzle.Model;

namespace WordPuzzle
{
    public class WordSearch
    {
        ILogger _logger;
        IWordDictionary _dictionary;
        public string _firstWord { get; set; }
        public string _lastWord { get; set; }
		public string _resultFilePath { get; set; }

		public WordSearch(IWordDictionary dictionary, ILogger logger)
        {
            _logger = logger;
            _dictionary = dictionary;
        }

        public void PlayGame()
        {

            _logger.WriteConsole(" == Provide first word == ");
            _firstWord = ProvideWord();
            _logger.WriteConsole("");

            _logger.WriteConsole(" == Provide last word  == ");
            _lastWord = ProvideWord();
            _logger.WriteConsole("");

			_logger.WriteConsole(" == Please Provide File Path To Save Results == ");
			_logger.WriteConsole(" == Leave it blank to use Results.txt in application file folder == ");
			_resultFilePath = ProvideResultFile();
			_logger.WriteConsole("");

			_logger.WriteConsole("-----------WORDS-----------");
            _logger.WriteConsole("First Word: " + _firstWord);
            _logger.WriteConsole("Last Word:  " + _lastWord);
            _logger.WriteConsole("---------------------------");
            _logger.WriteConsole("");

			Console.WriteLine(" - - - SOLVING PUZZLE - - - ");
			GetResults();
		}

		public string ProvideResultFile()
		{
			while (true)
			{
				var resultPath = Console.ReadLine();
				
				if (resultPath == "")
				{
					return null;
				}
				else
				{
					if (File.Exists(resultPath))
					{
						return resultPath;
					}
					else
					{
						_logger.WriteConsole("");
						_logger.WriteConsole($" !!! Please provide valid Result File !!! ");
						_logger.WriteConsole("");
						continue;
					}
				}
				
			}
		}

		public string ProvideWord()
        {
            while (true)
            {
                var word = Console.ReadLine();
                if (string.IsNullOrEmpty(word) || word.Length != 4)
                {

                    _logger.WriteConsole("");
                    _logger.WriteConsole($" !!! Please provide 4 letter word !!! ");
                    _logger.WriteConsole("");
                    _logger.WriteConsole($"== Provide another word ==");
                    continue;

                } else if (!_dictionary.CheckIfWordExsits(word))
                {

                    _logger.WriteConsole("");
                    _logger.WriteConsole($" !!! Word : {word} does not exist in supplied dictionary !!! ");
                    _logger.WriteConsole("");
                    _logger.WriteConsole($"== Provide another word ==");
                    continue;

                }

                return word;

            }
        }

		private void GetResults()
		{
			Console.SetCursorPosition(0, Console.CursorTop - 1);

			if (_firstWord == _lastWord)
			{
				_logger.WriteConsole("-----------RESULT----------");
				_logger.WriteConsole("- > First and Last Word are the same < -");
			}
			else
			{
				var wordsList = GetFastestRoute();
				_logger.WriteConsole("-----------RESULT----------");

				if (wordsList == null)
				{
					_logger.WriteConsole("- > No matching results < -");
				}
				else
				{
					foreach (var word in wordsList)
					{
						_logger.WriteConsole($" -> { word }");
					}

					try{
						_logger.SaveResults(wordsList, _resultFilePath);
					}
					catch(Exception ex)
					{
						_logger.WriteConsole("");
						_logger.LogMessageOrError(ex.Message);
						_logger.WriteConsoleError("Error Saving Result File");
					}

				}
			}

			_logger.WriteConsole("---------------------------");

		}

		public List<string> GetFastestRoute()
		{
			var wordsList = GetWordsTree();
			if (wordsList == null) { return null; };

			var returnResult = new List<string>();
			returnResult.Add(_lastWord);

			var countOfWords = wordsList.Last().Position - 1;
			var parent = wordsList.Last().Parent;

			while (countOfWords > 1)
			{
				var word = wordsList.Where(w => w.Text == parent && w.Position == countOfWords).FirstOrDefault();
				returnResult.Add(word.Text);
				parent = word.Parent;
				countOfWords--;
			};

			returnResult.Add(_firstWord);
			returnResult.Reverse();

			return returnResult;
		}

		private List<Word> GetWordsTree()
		{
			var wordsUsed = new List<string>();
			var returnList = new List<Word>();
			var wordQueue = new Queue<Word>();
			var dictionary = _dictionary.GetDictionary().Where(d => d.Length == _firstWord.Length);

			wordQueue.Enqueue(new Word { Text = _firstWord, Parent = null, Position = 1 });
			returnList.Add(new Word { Text = _firstWord, Parent = null, Position = 1 });

			while (wordQueue.Count > 0)
			{
				var currentWord = wordQueue.Dequeue();

				foreach (var word in dictionary)
				{
					if (wordsUsed.Contains(word)) { continue; };

					if (IsChildNode(currentWord.Text, word))
					{
						var childWord = new Word { Text = word, Parent = currentWord.Text, Position = currentWord.Position + 1 };
						wordQueue.Enqueue(childWord);
						returnList.Add(childWord);
						wordsUsed.Add(word);

						if (word == _lastWord) { return returnList; }
					}
				}
			};

			return null;
		}

		private bool IsChildNode(string firstWord, string secondWord)
		{
			var diffLettersCount = 0;
			var i = 0;
			var len = firstWord.Length;
			var firstWord_char = firstWord.ToCharArray();
			var secondWord_char = secondWord.ToCharArray();

			while (i < len) 
			{
				if (firstWord_char[i] != secondWord_char[i])
				{
					diffLettersCount++;
					if (diffLettersCount > 1) return false;
				}
				i++;
			};

			return (diffLettersCount == 1);
		}

    }
}
