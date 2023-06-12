using UnityEngine;
using Zenject;

public class StartGame : MonoBehaviour
{
    [Inject]
    private ControllerManager m_ControllerManager;
    private void Start()
    {
        m_ControllerManager.GameIsOver = false;
    }
}