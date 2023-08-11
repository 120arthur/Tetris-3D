using Input;
using Score;
using TetrisMechanic;
using Ui;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    [SerializeField]
    private GameScreen m_gameUi;
    [SerializeField]
    private TetraminoSpawner m_tetraminoSpawner;

    public override void InstallBindings()
    {
        Container.DeclareSignal<OnTetratiminoFinishMovementSignal>();
        Container.DeclareSignal<OnGameEndSignal>();

        Container.Bind<IGameStateManager>().To<GameStateManager>().AsSingle();
        Container.Bind<ITetraminoController>().To<TetraminoController>().AsSingle();

        Container.Bind<IScore>().To<ScoreManager>().AsSingle();

#if UNITY_STANDALONE
        Container.Bind<IInputType>().To<DesktopInputType>().AsSingle();
#else
        Container.Bind<IInputType>().To<DesktopInputType>().AsSingle();
#endif
        IGameUI iGameUiInstance = m_gameUi;
        Container.Bind<IGameUI>().FromInstance(iGameUiInstance).AsSingle();

        ITetraminoSpawner iTetraminoSpawnerInstance = m_tetraminoSpawner;
        Container.Bind<ITetraminoSpawner>().FromInstance(iTetraminoSpawnerInstance).AsSingle();
    }
}