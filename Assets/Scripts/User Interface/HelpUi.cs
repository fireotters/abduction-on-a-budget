using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class HelpUi : BaseUi
{
    public Button toCredits;
    public Button toHelp;
    public GameObject credits;
        
    private void Start()
    {
        // Change music track
        MusicManager.i.ChangeMusicTrack(0);

        // Fade in the screen
        StartCoroutine(UsefulFunctions.FadeScreenBlack("from", fullUiFadeBlack));
    }

    void Update()
    {
        CheckKeyInputs();
    }
    private void CheckKeyInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitHelp();
        }

    }

    public void ExitHelp()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowCredits()
    {
        if(!credits.activeSelf)
        {
            credits.SetActive(true);
        }
        else
        {
            credits.SetActive(false);
        }
    }
}
