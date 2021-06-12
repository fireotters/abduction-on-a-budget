using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsPanel : MonoBehaviour
{
    [Header("Option Panel")]
    [SerializeField] private Slider optionMusicSlider;
    public Slider optionSFXSlider;
    [SerializeField] private TextMeshProUGUI txtFullscreenToggleBtn;

    // Functions related to Options menu
    public void OptionsOpen()
    {
        SetBtnFullscreenText();

        optionMusicSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("Music"));
        optionSFXSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("SFX"));
    }

    public void OptionsClose()
    {
        optionMusicSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("Music"));
        optionSFXSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("SFX"));
    }

    public void SwapFullscreen()
    {
        if (Screen.fullScreen)
        {
            Screen.SetResolution(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, false);
        }
        else
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
        Invoke(nameof(SetBtnFullscreenText), 0.1f);
    }

    public void ResetHighScore()
    {
        PlayerPrefs.SetInt("HighscoreNum", 0);
        PlayerPrefs.SetString("HighscoreName", "No Highscore Yet");
        SceneManager.LoadScene("MainMenu");
    }

    public void SetBtnFullscreenText()
    {
        if (Screen.fullScreen)
        {
            txtFullscreenToggleBtn.text = "Fullscreen ON";
        }
        else
        {
            txtFullscreenToggleBtn.text = "Fullscreen OFF";
        }
    }
}