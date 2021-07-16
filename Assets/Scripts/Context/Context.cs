using System;
using Score;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Context
{
    public class Context : IContext
    {
        public AssetLoader AssetLoader { get; }
        public GameManager GameManager { get; }
        public IScore Score { get; }
        public IMatchWords MatchWords { get; }
        public WordsScriptable WordsScriptable { get; }

        public Context()
        {
            ContextProvider.Subscribe(this);

            AssetLoader = new AssetLoader();
            
            GameManager = AssetLoader.LoadAndInstantiate<GameManager>($"Managers/{nameof(GameManager)}");
            
            WordsScriptable = AssetLoader.Load<WordsScriptable>($"WordSetting/{nameof(WordsScriptable)}");

            Score = new ScoreController();
            MatchWords = new MatchWords(WordsScriptable.word);
        }
    }
}