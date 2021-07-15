using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Context
{
    public class Context : MonoBehaviour
    {
        public AssetLoader assetloader { get; }
        public GameManager gameManager { get; }
        public GameUi gameUi { get; }

        public Context()
        {
            ContextProvider.Subscribe(this);

            assetloader = new AssetLoader();
            gameUi = assetloader.LoadAndInstantiate<GameUi>($"Managers/{nameof(gameUi)}");
            gameManager = assetloader.LoadAndInstantiate<GameManager>($"Managers/{nameof(gameManager)}");

        }
    }
}