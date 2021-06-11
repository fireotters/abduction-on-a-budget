using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameUi : BaseUi
{
    [Header("Game UI")]
    public GameObject gamePausePanel;

    private void Start()
    {
        // Change music track
        MusicManager.i.ChangeMusicTrack(1);

        // Fade in the level
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
            // Pause if pause panel isn't open, resume if it is open
            GameIsPaused(!gamePausePanel.activeInHierarchy);
        }

    }

    public void GameIsPaused(bool intent)
    {
        if (!GameManager.i.gameIsOver)
        {
            // Show or hide pause panel and set timescale
            gamePausePanel.SetActive(intent);
            Time.timeScale = (intent == true) ? 0 : 1;

            MusicManager.i.FindAllSfxAndPlayPause(gameIsPaused: intent);
        }
    }

    public void ExitGameFromPause()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
