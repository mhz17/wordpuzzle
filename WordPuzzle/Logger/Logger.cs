using System;
using System.Collections.Generic;
using System.IO;

namespace WordPuzzle
{
	class Logger : ILogger
	{

		public void LogMessageOrError(string message)
		{
			string directory = Directory.GetCurrentDirectory();
			string strPath = $"{ directory }\\log.txt";
			if (!File.Exists(strPath))
			{
				File.Create(strPath).Dispose();
			}
			using (StreamWriter sw = File.AppendText(strPath))
			{
				sw.WriteLine("=============Error Logging ===========");
				sw.WriteLine("===========Start============= " + DateTime.Now);
				sw.WriteLine("Error Message: " + message);
				sw.WriteLine("===========End=============== " + DateTime.Now);
				sw.WriteLine();
			}
		}

		public void WriteConsoleError(string message)
		{
			Console.WriteLine("=============Error====================");
			Console.WriteLine("Error Message: " + message);
			Console.WriteLine("======================================");
		}

		public void WriteConsole(string message)
		{
			Console.WriteLine(message);
		}

		public void ClearConsole()
		{
			Console.Clear();
		}

		public void SaveResults(List<string> results, string filePath)
		{

			string directory = Directory.GetCurrentDirectory();
			if (filePath == null) { filePath = $"{ directory }\\results.txt"; }

			if (!File.Exists(filePath))
			{
				File.Create(filePath).Dispose();
			}

			using (StreamWriter sw = File.AppendText(filePath))
			{
				sw.WriteLine("-----------GAME------------" + DateTime.Now);
				sw.WriteLine();
				sw.WriteLine("-----------WORDS-----------");
				sw.WriteLine("First Word: " + results[0]);
				sw.WriteLine("Last Word:  " + results[results.Count - 1]);
				sw.WriteLine("--------------------------------");
				sw.WriteLine("");
				sw.WriteLine("-----------RESULT----------");
			}

			
			foreach (var word in results)
			{
				using (StreamWriter sw = File.AppendText(filePath))
				{
					sw.WriteLine($" -> { word }");
				}
			}

			using (StreamWriter sw = File.AppendText(filePath))
			{
				sw.WriteLine("--------------------------------");
				sw.WriteLine();
			}

		}
	}
}
