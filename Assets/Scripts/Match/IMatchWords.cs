using System.Collections.Generic;

namespace Match
{
    public interface IMatchWords
    {
        bool VerifyMatch(List<char> wordLettersChars);
    }
}