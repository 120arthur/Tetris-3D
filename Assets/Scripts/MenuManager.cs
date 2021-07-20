using Sound;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        SoundController.SoundControllerInstance.ChangeMusic(0);
        SoundController.SoundControllerInstance.PlayMusic();
    }
}