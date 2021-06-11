using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
    {

    public AudioMixer mixer;
    public AudioSource sfxDemo, currentMusicPlayer;
    public AudioClip musicMainMenu, musicGameplay;
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
    }

    // TODO see if this requires to be rewritten. Cam has a version that searches for Unity tags instead.
    public void FindAllSfxAndPlayPause(bool gameIsPaused)
    {
        AudioSource[] listOfSfxObjects = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        if (gameIsPaused == true) // Pause
        {
            foreach (AudioSource sfxObject in listOfSfxObjects)
            {
                if (sfxObject.isPlaying)
                {
                    sfxObject.Pause();
                }
            }
        }
        if (gameIsPaused == false) // Resume
        {
            foreach (AudioSource sfxObject in listOfSfxObjects)
            {
                if (!sfxObject.isPlaying)
                    sfxObject.UnPause();
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
