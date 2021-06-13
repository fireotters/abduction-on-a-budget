using System;
using In_Game_Items;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameIsOver = false;
    [SerializeField] private GameUi _gameUi;
    public int levelNo;

    private static GameManager _i;
    public static GameManager i { get { if (_i == null) _i = FindObjectOfType<GameManager>(); return _i; } }
    [SerializeField] private Text keyCountText;
    [SerializeField] private Text humanCountText;
    private int _totalCountOfHumans = 0;
    
    private void Start()
    {
        var allHumansInCurrentLevel = FindObjectsOfType<Human>();
        _totalCountOfHumans = allHumansInCurrentLevel.Length;
    }

    private void Update()
    {
        if (keyCountText && humanCountText)
        {
            keyCountText.text = $"{keyCount}";
            humanCountText.text = $"{humanCount}/{_totalCountOfHumans}";
        }
    }
    
    public int keyCount = 0;
    public int humanCount = 0;

    public void PlayerDied()
    {
        gameIsOver = true;
        Invoke(nameof(PlDied2), 0.8f);
    }

    public void LevelFinished()
    {
        _gameUi.PlayLevelTransition(didPlayerDie: false);
    }

    private void PlDied2()
    {
        _gameUi.PlayLevelTransition(didPlayerDie: true);
    }
}
