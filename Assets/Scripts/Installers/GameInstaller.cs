using Input;
using Match;
using Score;
using TetrisMechanic;
using Ui;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    [SerializeField]
    private GameUi m_GameUi;
    [SerializeField]
    private ControllerManager m_ControllerManager;
    [SerializeField]
    private TetraminoSpawner m_TetraminoSpawner;
    [SerializeField]
    private TetraminoManager m_TetraminoManager;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ScoreManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<DesktopInputType>().AsSingle();
        Container.BindInterfacesAndSelfTo<MatchWords>().AsSingle();

        Container.BindInstance(m_TetraminoManager);
        Container.BindInstance(m_GameUi);
        Container.BindInstance(m_ControllerManager);
        Container.BindInstance(m_TetraminoSpawner);
        //Container.DeclareSignal<OnCubeMove>().OptionalSubscriber();
    }
}