using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public partial class GameUi : BaseUi
{
    [Header("Game UI")]
    public GameObject gamePausePanel, gameEndPanel;
    public string levelGo;

    [SerializeField] private TextMeshProUGUI keyCountText, humanCountText, endLevelHumans;

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

        keyCountText.text = $"{GameManager.i.keyCount}";
        humanCountText.text = $"{GameManager.i.humanCount}/{GameManager.i.totalCountOfHumans}";
    }

    private void CheckKeyInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Pause if pause panel isn't open, resume if it is open
            GameIsPaused(!gamePausePanel.activeInHierarchy);
        }

    }

    public void ShowEndLevelScreen()
    {
        gameEndPanel.SetActive(true);
        endLevelHumans.text = GameManager.i.humanCount.ToString();

    }

    public void GameIsPaused(bool intent)
    {
        if (!GameManager.i.gameIsOver && !gameEndPanel.activeInHierarchy)
        {
            // Show or hide pause panel and set timescale
            gamePausePanel.SetActive(intent);
            Time.timeScale = (intent == true) ? 0 : 1;

            MusicManager.i.FindAllSfxAndPlayPause(gameIsPaused: intent);
        }
    }

    private const int transitionTime = 2;
    public void PlayLevelTransition(int intent)
    {
        _levelTransitionOverlay.SetBool("levelEndedOrDead", true);
        if (intent == 0)
            Invoke(nameof(ReloadLevel), transitionTime);
        else if (intent == 1)
            Invoker.InvokeDelayed(ExitGameFromPause, transitionTime);
        else
            Invoke(nameof(NextLevel), transitionTime);
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
        SceneManager.LoadScene(levelGo);
        //SceneManager.LoadScene("Level" + levelStr);
    }

    public void ExitGameFromPause()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
