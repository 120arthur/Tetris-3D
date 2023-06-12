using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Ui
{
    /// <summary>
    /// This class has useful functions to UI.
    /// </summary>
    public class Ui : MonoBehaviour
    {
        [Inject]
        private GameManager m_GameManager;

        // This method are called in on the ui buttons.
        public void ChangeScene(string sceneName)
        {
            m_GameManager.m_NextSxene = sceneName;
            Addressables.LoadSceneAsync("Loading", activateOnLoad: true);
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