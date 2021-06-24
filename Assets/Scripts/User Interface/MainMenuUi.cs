using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using User_Interface;

public class MainMenuUi : BaseUi
{
    public enum SceneNavigationIntent
    {
        HelpMenu = 0
    }
    
    [Header("Main Menu UI")]
    [SerializeField] private OptionsPanel optionsPanel;
    [SerializeField] private TextMeshProUGUI versionText;
    //Sign anim
    public Animator animSign;

    private void Start()
    {
        // Set version number
        SetVersionText();
        // Find SFX Slider & tell MusicManager where it is
        MusicManager.i.sfxDemo = optionsPanel.optionSFXSlider.GetComponent<AudioSource>();
        
        // Set up PlayerPrefs when game is first ever loaded
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetFloat("Music", 0.8f);
            PlayerPrefs.SetFloat("SFX", 0.8f);
            PlayerPrefs.SetInt("RopeInvert", 1);
        }

        // Change music track & set volume. Disable low pass filter.
        MusicManager.i.ChangeMusicTrack(0);
        MusicManager.i.audLowPass.enabled = false;
    }

    private void SetVersionText()
    {
        if (Debug.isDebugBuild)
        {
            versionText.text = !string.IsNullOrEmpty(Application.buildGUID)
                ? $"Version debug-{Application.version}-{Application.buildGUID}"
                : $"Version debug-{Application.version}-editor";
        }
        else
        {
            versionText.text = $"Version {Application.version}";
        }
    }

    public void TransitionToLevelSelect()
    {
        levelTransitionOverlay.SetBool("levelEndedOrDead", true);
        Invoke(nameof(OpenLevelSelect), 2);
    }

    public void TransitionToHelpMenu()
    {
        levelTransitionOverlay.SetBool("levelEndedOrDead", true);
        Invoke(nameof(OpenHelp), 2);
    }

    public void ResetGameProgressConfirmed()
    {
        // TODO Implement when progress system is developed
        levelTransitionOverlay.SetBool("levelEndedOrDead", true);
        Invoke(nameof(OpenMain), 2);
    }

    private void OpenLevelSelect()
    {
        SceneManager.LoadScene("LevelSelectMenu");
    }
    private void OpenHelp()
    {
        SceneManager.LoadScene("HelpMenu");
    }

    private void OpenMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void AnimateSign()
    {
        animSign.SetBool("go", true);
    }
}
