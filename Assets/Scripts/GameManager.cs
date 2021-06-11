using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameIsOver = false;

    private static GameManager _i;
    public static GameManager i { get { if (_i == null) _i = FindObjectOfType<GameManager>(); return _i; } }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
