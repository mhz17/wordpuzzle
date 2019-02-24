
using System.Collections.Generic;

namespace WordPuzzle
{
   public interface IWordDictionary
    {
        bool LoadDictionaryFile(string path);
        bool CheckIfWordExsits(string word);
		List<string> GetDictionary();
	}
}
