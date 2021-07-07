using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class aims to verify if the words assigned to it match.
/// </summary>
public class MatchWords : MonoBehaviour, IMatchWords
{
    public WordsScriptable wordTheme;
    // If the received word matches the one predefined by the ScriptableObject(wordTheme), the method returns true;
    public bool VerifyMatch(List<char> wordLettersChars)
    {
            // This for checks that each character in the list (wordLettersChars) matches the word stored in scriptableObject.
        for (var j = 0; j < wordTheme.word.Length; j++)
        {
            if (wordLettersChars.Count >= wordTheme.word.Length && wordLettersChars[j] == wordTheme.word[j]) continue;
            return false;
        }
        return true;
    }
}