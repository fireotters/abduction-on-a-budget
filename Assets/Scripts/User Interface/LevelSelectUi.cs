using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using User_Interface;

public class LevelSelectUi : BaseUi
{
    private void Start()
    {
        MusicManager.i.ChangeMusicTrack(0);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayTransition()
    {
        levelTransitionOverlay.SetBool("levelEndedOrDead", true);
    }
}
