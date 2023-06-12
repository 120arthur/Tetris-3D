using UnityEngine;

namespace Sound
{
    /// <summary>
    /// This class manages all sounds in the game.
    /// </summary>
    public class SoundController : MonoBehaviour
    {    
        [SerializeField] private AudioSource music;
        [SerializeField] private AudioSource sfx;
        [SerializeField] private AudioClip[] sfxClip;
        [SerializeField] private AudioClip[] musicClip;

        public void ChangeMusic(int musicIndex) => music.clip = musicClip[musicIndex];
        public void ChangeSfx(int sfxIndex) => sfx.clip = sfxClip[sfxIndex];
        public void PlayMusic() => music.Play();
        public void PlaySfx() => sfx.Play();
    }
}