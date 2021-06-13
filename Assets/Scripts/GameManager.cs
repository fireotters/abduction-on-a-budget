using System;
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

    private void Update()
    {
        if (keyCountText)
        {
            keyCountText.text = $"Key {keyCount}, Human count {humanCount}";
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
