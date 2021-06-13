using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameIsOver = false;

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
}
