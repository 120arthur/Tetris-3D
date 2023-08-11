using Sound;
using UnityEngine;
using Zenject;

public class ManagerInstaller : MonoInstaller<ManagerInstaller>
{
    [SerializeField]
    private GameManager m_gameManager;
    [SerializeField]
    private SoundController m_soundController;
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        IGameManager iGameManagerInstance = m_gameManager;
        Container.Bind<IGameManager>().FromInstance(iGameManagerInstance).AsSingle();

        ISoundController iSoundControllerInstance = m_soundController;
        Container.Bind<ISoundController>().FromInstance(iSoundControllerInstance).AsSingle();
    }
}
