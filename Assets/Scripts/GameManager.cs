using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameIsOver = false;
    [SerializeField] private GameUi _gameUi;
    public int levelNo;

    private static GameManager _i;
    public static GameManager i { get { if (_i == null) _i = FindObjectOfType<GameManager>(); return _i; } }

    public int keyCount = 0;
    public int humanCount = 0;

    public void PlayerDied()
    {
        _gameUi.PlayLevelTransition(didPlayerDie: true);
    }

    public void LevelFinished()
    {
        _gameUi.PlayLevelTransition(didPlayerDie: false);
    }
}
