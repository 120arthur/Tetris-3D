namespace Sound
{
    public interface ISoundController
    {
        void ChangeMusic(int musicIndex);
        void ChangeSfx(int sfxIndex);
        void PlayMusic();
        void PlaySfx();
    }
}