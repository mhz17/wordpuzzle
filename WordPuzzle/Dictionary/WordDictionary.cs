using System;
using System.Collections.Generic;
using System.IO;

namespace WordPuzzle
{
	public class WordDictionary : IWordDictionary
	{

		public List<string> _listOfWords;
		ILogger _logger;

		public WordDictionary(ILogger logger)
		{
			_logger = logger;
		}

		public bool CheckIfWordExsits(string word)
		{
			if (!_listOfWords.Contains(word))
			{
				return false;
			}

			return true;
		}

		public List<string> GetDictionary()
		{
			return _listOfWords;
		}

		public bool LoadDictionaryFile(string path)
		{

			string strPath = path;
			_listOfWords = new List<string>();

			if (strPath == "")
			{
				strPath = Properties.Resources.words_english;
				foreach (string sw in strPath.Split(Environment.NewLine))
				{
					_listOfWords.Add(sw);
				}
				_logger.LogMessageOrError("Default Dictionary Used");
				return true;
			}
			else
			{
				try
				{
					if (File.Exists(strPath))
					{
						foreach (string sw in File.ReadLines(strPath))
						{
							_listOfWords.Add(sw);
						}
						return true;
					}
					else
					{
						_logger.LogMessageOrError("Dictionary file not found");
						_logger.WriteConsoleError("Dictionary file not found");
						return false;
					}
				}
				catch (Exception ex)
				{
					_logger.WriteConsoleError("Error Loading Dictionary please check error log");
					_logger.LogMessageOrError(ex.Message);
					return false;
				}
			}

		}

	}
}
