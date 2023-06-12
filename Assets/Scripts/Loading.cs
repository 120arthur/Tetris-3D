using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

public class Loading : MonoBehaviour
{
    [Inject]
    private GameManager m_GameManager;

    private static AsyncOperationHandle<SceneInstance> m_SceneLoadOpHandle;

    [SerializeField]
    private Slider m_LoadingSlider;

    [SerializeField]
    private GameObject m_LoadingText;

    private void Awake()
    {
            string m_NextSxene;
        if(m_GameManager == null)
        {
            m_NextSxene = "Menu";
        }
        else
        {
            m_NextSxene = m_GameManager.m_NextSxene;
        }
            StartCoroutine(loadNextLevel(m_NextSxene));
    }

    private IEnumerator loadNextLevel(string Scene)
    {
        m_SceneLoadOpHandle = Addressables.LoadSceneAsync(Scene, activateOnLoad: true);

        while (!m_SceneLoadOpHandle.IsDone)
        {
            m_LoadingSlider.value = m_SceneLoadOpHandle.PercentComplete;

            yield return null;
        }

        Debug.Log($"Loaded Level {Scene}");
    }
}
