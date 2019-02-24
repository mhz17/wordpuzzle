using System.Collections.Generic;

namespace WordPuzzle
{
    public interface ILogger
    {
        void LogMessageOrError(string message);
		void WriteConsoleError(string message);
		void WriteConsole(string message);
        void ClearConsole();
		void SaveResults(List<string> results, string resultFilePath);
	}
}
