using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using System;

public class MainMenuUi : BaseUi
{
    [Header("Main Menu UI")]
    [SerializeField] private OptionsPanel optionsPanel;
    
    // High Score display
    [SerializeField] private TextMeshProUGUI highScoreNum, highScoreName;

    // Audio
    public AudioMixer mixer;

    void Start()
    {
        // Find SFX Slider & tell MusicManager where it is
        MusicManager.i.sfxDemo = optionsPanel.optionSFXSlider.GetComponent<AudioSource>();
        
        // Set up PlayerPrefs when game is first ever loaded
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetFloat("Music", 0.8f);
            PlayerPrefs.SetFloat("SFX", 0.8f);
        }

        // Change music track & set volume
        MusicManager.i.ChangeMusicTrack(0);
        StartCoroutine(UsefulFunctions.FadeScreenBlack("from", fullUiFadeBlack));
    }

    // Other functions
    public void OpenGame()
    {
        StartCoroutine(UsefulFunctions.FadeScreenBlack("to", fullUiFadeBlack));
        Invoke(nameof(OpenGame2), 1f);
    }
    
    public void OpenGame2()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenHelp()
    {
        SceneManager.LoadScene("HelpMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
