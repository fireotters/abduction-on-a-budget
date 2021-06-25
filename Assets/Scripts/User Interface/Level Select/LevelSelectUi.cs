using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using User_Interface;

public partial class LevelSelectUi : BaseUi
{
    private void Start()
    {
        MusicManager.i.ChangeMusicTrack(0);
        foreach (World world in LevelHandling.worlds)
        {
            foreach (Level level in world.LevelsInWorld)
            {
                print(level.LevelName);
            }
        }
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
