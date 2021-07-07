using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class has useful functions to UI.
/// </summary>
public class Ui : MonoBehaviour
{
    // This method are called in on the ui buttons.
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Turn on Ui Elements
    /// </summary>
    /// <param name="uiObjct"></param>
    protected static void TurnOnUi(GameObject uiObjct)
    {
        uiObjct.SetActive(true);
    }
    /// <summary>
    /// Turn off Ui Elements
    /// </summary>
    /// <param name="uiObjct"></param>
    protected static void TurnOffUi(GameObject uiObjct)
    {
        uiObjct.SetActive(false);
    }
}