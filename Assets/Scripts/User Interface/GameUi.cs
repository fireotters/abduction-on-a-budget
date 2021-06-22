using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using User_Interface;

public class GameUi : BaseUi
{
    [Header("Game UI")]
    public GameObject gamePausePanel, gameEndPanel;
    public string levelGo;
    [SerializeField] private TextMeshProUGUI keyCountText, humanCountText, endLevelHumans;
    private const int TransitionTime = 2;
    
    private void Start()
    {
        levelTransitionOverlay.gameObject.SetActive(true);
        // Change music track
        MusicManager.i.ChangeMusicTrack(1);
    }

    private void Update()
    {
        CheckKeyInputs();
        keyCountText.text = $"{GameManager.i.keyCount}";
        humanCountText.text = $"{GameManager.i.humanCount} {GameManager.i.totalCountOfHumans}";
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
        endLevelHumans.text = $"{GameManager.i.humanCount}";
    }

    public void GameIsPaused(bool intent)
    {
        if (!GameManager.i.gameIsOver && !gameEndPanel.activeInHierarchy)
        {
            // Show or hide pause panel and set timescale
            gamePausePanel.SetActive(intent);
            Time.timeScale = intent ? 0 : 1;
            MusicManager.i.FindAllSfxAndPlayPause(gameIsPaused: intent);
        }
    }

    
    public void PlayLevelTransition(int intent)
    {
        levelTransitionOverlay.SetBool("levelEndedOrDead", true);
        switch (intent)
        {
            case 0:
                Invoke(nameof(ReloadLevel), TransitionTime);
                break;
            case 1:
                Invoker.InvokeDelayed(ExitGameFromPause, TransitionTime);
                break;
            default:
                Invoke(nameof(NextLevel), TransitionTime);
                break;
        }
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(levelGo);
    }

    private static void ExitGameFromPause()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
