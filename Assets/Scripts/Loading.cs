using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

public class Loading : MonoBehaviour
{
    private const string MENU_SCENE_NAME = "Menu";

    [Inject]
    private IGameManager m_gameManager;

    private static AsyncOperationHandle<SceneInstance> m_sceneLoadOpHandle;

    [SerializeField]
    private Slider m_loadingSlider;

    [SerializeField]
    private GameObject m_loadingText;

    private void Awake()
    {
        string m_NextSxene;
        if (m_gameManager == null || string.IsNullOrEmpty(m_gameManager.NextScene))
        {
            m_NextSxene = MENU_SCENE_NAME;
        }
        else
        {
            m_NextSxene = m_gameManager.NextScene;
        }
        StartCoroutine(loadNextLevel(m_NextSxene));
    }

    private IEnumerator loadNextLevel(string Scene)
    {
        m_sceneLoadOpHandle = Addressables.LoadSceneAsync(Scene, activateOnLoad: true);

        while (!m_sceneLoadOpHandle.IsDone)
        {
            m_loadingSlider.value = m_sceneLoadOpHandle.PercentComplete;

            yield return null;
        }

        Debug.Log($"Loaded Level {Scene}");
    }
}
