using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This class aims to verify if the words assigned to it match.
/// </summary>
public class MatchWords : MonoBehaviour, IMatchWords
{
    public string wordTheme;

    public MatchWords(string correctWord)
    {
        wordTheme = correctWord;
    }
    
    // If the received word matches the one predefined by the ScriptableObject(wordTheme), the method returns true;
    public bool VerifyMatch(List<char> wordLettersChars)
    {
        if (wordLettersChars.Count != wordTheme.Length) return false;
        
        // Checks that each character in the list (wordLettersChars) matches the word stored in scriptableObject.
        return !wordTheme.Where((t, j) => wordLettersChars[j] != t).Any();
    }
}