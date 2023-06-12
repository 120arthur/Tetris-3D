using Sound;
using UnityEngine;
using Zenject;

public class ManagerInstaller : MonoInstaller<ManagerInstaller>
{
    [SerializeField]
    private GameManager m_GameManager;
    [SerializeField]
    private SoundController m_SoundController;
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.BindInstance(m_GameManager);
        Container.BindInstance(m_SoundController);
    }
}
