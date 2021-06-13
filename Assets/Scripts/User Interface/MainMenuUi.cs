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

    public Animator _levelTransitionOverlay;

    // Audio
    public AudioMixer mixer;

    //Sign anim
    public Animator _animSign;

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
    }

    public void Transition(int b)
    {
        _levelTransitionOverlay.SetBool("levelEndedOrDead", true);
        if (b == 0)
        {
            Invoke(nameof(OpenHelp), 2);
        }
        else if (b == 1)
        {
            Invoke(nameof(ActuallyGame), 2);
        }
    }
    
    public void ActuallyGame()
    {
        SceneManager.LoadScene("ComicAnim");
    }

    public void OpenHelp()
    {
        SceneManager.LoadScene("HelpMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad >= 5)
        {
            _animSign.SetBool("go", true);
        }
    }
}
