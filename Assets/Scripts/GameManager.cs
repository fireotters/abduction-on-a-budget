using System;
using In_Game_Items;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameIsOver = false;
    [SerializeField] private GameUi _gameUi;
    private Player.UfoController _ufo;
    public int levelNo;

    private static GameManager _i;
    public static GameManager i { get { if (_i == null) _i = FindObjectOfType<GameManager>(); return _i; } }

    public int totalCountOfHumans = 0;
    
    private void Start()
    {
        var allHumansInCurrentLevel = FindObjectsOfType<Human>();
        totalCountOfHumans = allHumansInCurrentLevel.Length;
        Invoke(nameof(FindUfo), 0.5f);
    }

    private void FindUfo()
    {
        _ufo = FindObjectOfType<Player.UfoController>();
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
        _ufo.levelOverFlyRight = true;
        _gameUi.ShowEndLevelScreen();
    }

    private void PlDied2()
    {
        _gameUi.PlayLevelTransition(0);
    }
}
