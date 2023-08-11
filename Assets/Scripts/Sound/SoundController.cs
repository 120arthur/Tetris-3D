using UnityEngine;

namespace Sound
{
    /// <summary>
    /// This class manages all sounds in the game.
    /// </summary>
    public class SoundController : MonoBehaviour, ISoundController
    {
        [SerializeField] private AudioSource m_music;
        [SerializeField] private AudioSource m_sfx;
        [SerializeField] private AudioClip[] m_sfxClip;
        [SerializeField] private AudioClip[] m_musicClip;

        public void ChangeMusic(int musicIndex)
        {
            m_music.clip = m_musicClip[musicIndex];
            PlayMusic();
        }

        public void ChangeSfx(int sfxIndex) => m_sfx.clip = m_sfxClip[sfxIndex];
        public void PlayMusic() => m_music.Play();
        public void PlaySfx() => m_sfx.Play();
    }
}