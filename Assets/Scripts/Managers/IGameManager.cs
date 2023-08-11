public interface IGameManager
{
    string NextScene { get; }

    void SetNextScene(string nextScene);

    void ChangeScene(string sceneName);
}