using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public partial class HelpUi : BaseUi
{
    public Button toCredits;
    public Button toHelp;
    public GameObject credits;

    public Animator _levelTransitionOverlay;

    private void Start()
    {
        // Change music track
        MusicManager.i.ChangeMusicTrack(0);


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
        _levelTransitionOverlay.SetBool("levelEndedOrDead", true);
        Invoke(nameof(ActuallyExit), 2);
    }

    private void ActuallyExit()
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
