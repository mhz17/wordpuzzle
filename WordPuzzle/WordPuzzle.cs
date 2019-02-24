using System;
using System.Threading;

namespace WordPuzzle
{
    public class WordPuzzle
    {

        static void Main(string[] args)
        {

            ILogger logger = new Logger();

            var dictionary = new WordDictionary(logger);
            var word = new WordSearch(dictionary, logger);
            var keepPlaying = false;
			var dictionaryLoaded = false;

			logger.WriteConsole("----------------------");
			logger.WriteConsole("WELCOME TO WORD PUZZLE");
			logger.WriteConsole("");


			while (!dictionaryLoaded) 
			{
				logger.WriteConsole(" == Please provide path to dictionary == ");
				logger.WriteConsole(" == Leave Empty to use default dictionary == ");
				logger.WriteConsole("");

				var strPath = Console.ReadLine();

				if (dictionary.LoadDictionaryFile(strPath))
				{
					dictionaryLoaded = true;
					break;
				}
				else
				{
					logger.ClearConsole();
					logger.WriteConsole("");
					logger.WriteConsoleError("Dictionary File Invalid");
					logger.LogMessageOrError("Dictionary File Invalid");
					logger.WriteConsole("");				
				}
			};
			
			if (dictionaryLoaded) {
                do
                {

					word.PlayGame();

                    logger.WriteConsole("");
                    logger.WriteConsole("Start Again? Y or Exit");

                    if (Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        keepPlaying = true;
                    }
                    else
                    {
                        keepPlaying = false;
                    }
                    logger.ClearConsole();

                } while (keepPlaying);

                logger.WriteConsole("");
                logger.WriteConsole("*** Goodbye ***");
                Thread.Sleep(2000);
			}

        }

    }
}
