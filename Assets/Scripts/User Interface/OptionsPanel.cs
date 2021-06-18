using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsPanel : MonoBehaviour
{
    [Header("Option Panel")]
    [SerializeField] private Slider _optionMusicSlider;
    public Slider optionSFXSlider;
    [SerializeField] private TextMeshProUGUI _optionFullscreenText, _optionRopeInvertText, _optionRopeInvertClarifyText;
    private const string StrRopeInvertOn = "(When UFO is below alien, rope climbing controls are flipped)",
                         StrRopeInvertOff = "(Rope climbing controls never flip)";

    // Functions related to Options menu
    public void OptionsOpen()
    {
        SetBtnFullscreenText();
        SetBtnRopeInvertText();

        _optionMusicSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("Music"));
        optionSFXSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("SFX"));
    }

    public void OptionsClose()
    {
        _optionMusicSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("Music"));
        optionSFXSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("SFX"));
    }

    public void SwapFullscreen()
    {
        if (Screen.fullScreen)
            Screen.SetResolution(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, false);
        else
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        Invoke(nameof(SetBtnFullscreenText), 0.1f);
    }
    private void SetBtnFullscreenText()
    {
        if (Screen.fullScreen)
            _optionFullscreenText.text = "Fullscreen On";
        else
            _optionFullscreenText.text = "Fullscreen Off";
    }

    public void SwapRopeInvert()
    {
        if (PlayerPrefs.GetInt("RopeInvert") == 0)
        {
            PlayerPrefs.SetInt("RopeInvert", 1);
        }
        else
        {
            PlayerPrefs.SetInt("RopeInvert", 0);
        }
        SetBtnRopeInvertText();
    }

    private void SetBtnRopeInvertText()
    {
        if (PlayerPrefs.GetInt("RopeInvert") == 1)
        {
            _optionRopeInvertText.text = "Rope Invert On";
            _optionRopeInvertClarifyText.text = StrRopeInvertOn;
        }
        else
        {
            _optionRopeInvertText.text = "Rope Invert Off";
            _optionRopeInvertClarifyText.text = StrRopeInvertOff;
        }
    }

    public void ChangeMusicPassToManager(float musVolume)
    {
        MusicManager.i.ChangeMusic(musVolume);
    }
    public void ChangeSFXPassToManager(float sfxVolume)
    {
        MusicManager.i.ChangeSFX(sfxVolume);
    }
}
