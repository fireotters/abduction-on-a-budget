using UnityEngine;
using UnityEngine.SceneManagement;

public partial class HelpUi : BaseUi
{
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
}
