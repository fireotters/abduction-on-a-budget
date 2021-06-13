using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameUi : BaseUi
{
    [Header("Game UI")]
    public GameObject gamePausePanel;

    public Animator _levelTransitionOverlay;

    private void Start()
    {
        _levelTransitionOverlay.gameObject.SetActive(true);
        // Change music track
        MusicManager.i.ChangeMusicTrack(1);
    }

    private void Update()
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

    public void PlayLevelTransition(bool didPlayerDie)
    {
        _levelTransitionOverlay.SetBool("levelEndedOrDead", true);
        if (didPlayerDie)
            Invoke(nameof(ReloadLevel), 2f);
        else
            NextLevel();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        int lvlNo = GameManager.i.levelNo;
        // If lvlNo less than 9, append a 0.
        string levelStr = lvlNo < 10 ? "0" + lvlNo.ToString() : lvlNo.ToString();
        SceneManager.LoadScene("Level" + levelStr);
    }

    public void ExitGameFromPause()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
