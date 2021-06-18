using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{

    public AudioMixer mixer;
    public AudioSource sfxDemo, currentMusicPlayer; // SFX Slider in Options, & playing music
    public AudioClip musicMainMenu, musicGameplay;
    public AudioClip selectSound, startGameSound, backSound;
    public AudioLowPassFilter audLowPass;
    
    private int lastTrackRequested = -1; // When first created, pick the scene's chosen song

    public static MusicManager i;
    private void Awake()
    {
        
        if (i != null)
        {
            Destroy(gameObject);
        }
        else
        {
            i = this;
            DontDestroyOnLoad(gameObject);
        }
        audLowPass = GetComponent<AudioLowPassFilter>();
    }

    public void ChangeMusic(float sliderValue) {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("Music", sliderValue);
        PlayerPrefs.Save();
    }

    public void ChangeSFX(float sliderValue) {
        mixer.SetFloat("SfxVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFX", sliderValue);
        PlayerPrefs.Save();
        if (!sfxDemo.isPlaying) {
            sfxDemo.Play();
        }
        print(PlayerPrefs.GetFloat("SFX"));
    }

    // TODO Consider rewriting. Cam has a version that searches for Unity tags instead.
    public void FindAllSfxAndPlayPause(bool gameIsPaused)
    {
        if (FindObjectsOfType(typeof(AudioSource)) is AudioSource[] listOfSfxObjects)
        {
            if (gameIsPaused) // Pause
            {
                foreach (var sfxObject in listOfSfxObjects)
                {
                    if (sfxObject.isPlaying)
                        sfxObject.Pause();
                }
            }
            else // Resume
            {
                foreach (var sfxObject in listOfSfxObjects)
                {
                    if (!sfxObject.isPlaying)
                        sfxObject.UnPause();
                }
            }
        }

    }

    public void ChangeMusicTrack(int index)
    {
        // Set volumes
        float musicVol = PlayerPrefs.GetFloat("Music");
        float sfxVol = PlayerPrefs.GetFloat("SFX");
        mixer.SetFloat("MusicVolume", Mathf.Log10(musicVol) * 20);
        mixer.SetFloat("SfxVolume", Mathf.Log10(sfxVol) * 20);

        Debug.Log($"Music requested: {index} Music last played: {lastTrackRequested}");
        if (index != lastTrackRequested)
        {
            currentMusicPlayer.enabled = true;
            if (currentMusicPlayer.isPlaying)
                currentMusicPlayer.Stop();
            switch (index)
            {
                case 0:
                    currentMusicPlayer.clip = musicMainMenu;
                    break;

                case 1:
                    currentMusicPlayer.clip = musicGameplay;
                    break;
            }
            currentMusicPlayer.Play();
            lastTrackRequested = index;
        }
    }
}
