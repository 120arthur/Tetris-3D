using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
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

        protected static void TurnOnUi(GameObject uiObject)
        {
            uiObject.SetActive(true);
        }

        protected static void TurnOffUi(GameObject uiObject)
        {
            uiObject.SetActive(false);
        }
    }
}