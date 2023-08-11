using Sound;
using UnityEngine;
using Zenject;

public class MenuController : MonoBehaviour
{
    [Inject]
    private ISoundController m_soundController;

    private void Start()
    {
        m_soundController.ChangeMusic(0);
    }
}