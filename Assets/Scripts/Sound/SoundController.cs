using UnityEngine;
/// <summary>
/// This class manages all sounds in the game.
/// This class is a singleton.
/// </summary>
public class SoundController : MonoBehaviour
{
    public static SoundController SoundControllerInstance;
    
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioSource _sfx;
    [SerializeField] private AudioClip[] _sfxClip;
    [SerializeField] private AudioClip[] _musicClip;

    private void Awake()
    {
        
        DontDestroyOnLoad (this);
	 
        if (SoundControllerInstance == null) {         
            SoundControllerInstance = this;     
        } 
        else {
            Destroy(gameObject);
        } 
    }

    public void ChangeMusic(int musicIndex) => _music.clip = _musicClip[musicIndex];
    public void ChangeSfx(int sfxIndex) => _sfx.clip = _sfxClip[sfxIndex];
    public void PlayMusic() => _music.Play();
    public void PlaySfx() => _sfx.Play();
}