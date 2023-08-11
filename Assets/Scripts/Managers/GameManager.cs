using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameManager : MonoBehaviour, IGameManager
{
    private const string LOADING_SCENE_NAME = "Loading";

    private string m_nextScene;

    public string NextScene => m_nextScene;

    public void SetNextScene(string nextScene)
    {
        m_nextScene = nextScene;
    }

    public void ChangeScene(string sceneName)
    {
        SetNextScene(sceneName);
        Addressables.LoadSceneAsync(LOADING_SCENE_NAME, activateOnLoad: true);
    }
}
