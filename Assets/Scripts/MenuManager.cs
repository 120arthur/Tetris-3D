using Sound;
using UnityEngine;
using Zenject;

public class MenuManager : MonoBehaviour
{
    [Inject]
    private SoundController m_SoundController;
    private void Start()
    {
        m_SoundController.ChangeMusic(0);
        m_SoundController.PlayMusic();
    }
}