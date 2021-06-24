using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using User_Interface;

public class LevelSelectUi : BaseUi
{
    private void Start()
    {

    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
