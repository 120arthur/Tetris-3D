using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

namespace Ui
{
    /// <summary>
    /// This class has useful functions to UI.
    /// </summary>
    public class Ui : MonoBehaviour
    {
        private const string GAME_SCENE_NAME = "Game";
        
        [Inject]
        private IGameManager m_gameManager;

        [SerializeField]
        private Button m_startButton;

        private void Start()
        {
            m_startButton.onClick.AddListener(ChangeToGameScene);
        }

        private void ChangeToGameScene()
        {
            m_gameManager.ChangeScene(GAME_SCENE_NAME);
        }

        protected void TurnOnUi(GameObject uiObject)
        {
            uiObject.SetActive(true);
        }

        protected void TurnOffUi(GameObject uiObject)
        {
            uiObject.SetActive(false);
        }
    }
}