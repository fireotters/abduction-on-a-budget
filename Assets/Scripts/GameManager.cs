using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameIsOver = false;

    private static GameManager _i;
    public static GameManager i { get { if (_i == null) _i = FindObjectOfType<GameManager>(); return _i; } }

    public int keyCount = 0;
    public int humanCount = 0;
}
