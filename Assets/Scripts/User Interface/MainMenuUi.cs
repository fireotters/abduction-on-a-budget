using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using User_Interface;

public class MainMenuUi : BaseUi
{
    private enum SceneNavigationIntent
    {
        StartGame = 1,
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

    public void Transition(int b)
    {
        var intent = (SceneNavigationIntent) b;
        levelTransitionOverlay.SetBool("levelEndedOrDead", true);
        
        switch (intent)
        {
            case SceneNavigationIntent.HelpMenu:
                Invoke(nameof(OpenHelp), 2);
                break;
            case SceneNavigationIntent.StartGame:
                Invoke(nameof(ActuallyGame), 2);
                break;
            default:
                Debug.LogError("This option is not defined!");
                break;
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
        if (Time.timeSinceLevelLoad >= 5)
        {
            animSign.SetBool("go", true);
        }
    }
}
