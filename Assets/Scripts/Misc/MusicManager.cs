using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class MusicManager : MonoBehaviour
    {

    public AudioMixer mixer;
    [FormerlySerializedAs("sfxDemo")] public AudioSource sfxPlayer;
    public AudioSource currentMusicPlayer;
    public AudioClip musicMainMenu, musicGameplay;
    public AudioClip hoverSound;
    public AudioClip selectSound;
    public AudioClip startGameSound;
    public AudioClip backSound;
    
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
        if (!sfxPlayer.isPlaying) {
            sfxPlayer.Play();
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

    public void PlaySound(string sound)
    {
        sfxPlayer.enabled = true;
        switch (sound)
        {
            case "hover":
                sfxPlayer.clip = hoverSound;
                break;

            case "select":
                sfxPlayer.clip = selectSound;
                Debug.Log("Select sound");
                break;

            case "back":
                sfxPlayer.clip = backSound;
                break;

            case "start":
                sfxPlayer.clip = startGameSound;
                break;
        }
        sfxPlayer.Play();
    }
}
