using UnityEngine;
/// <summary>
/// This ScriptableObject stores the phrases that scripts use to compare with the phrases formed in the game.
/// </summary>
[CreateAssetMenu(fileName = "Theme", menuName = "ScriptableObjects/WordTheme", order = 1)]
public class WordsScriptable : ScriptableObject
{
    public string Word;
}
