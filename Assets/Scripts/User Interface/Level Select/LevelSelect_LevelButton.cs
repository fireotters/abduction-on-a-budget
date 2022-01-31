using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect_LevelButton : LevelSelect_Button
{
    private Button _button;
    [SerializeField] public string attachedScene;

    private void Start()
    {
        _button = GetComponent<Button>();
        if (attachedScene != "")
        {
            _button.onClick.AddListener(PlayLeaveAnimation);
        }
    }

    public void PlayLeaveAnimation()
    {
        FindObjectOfType<LevelSelectUi>().PlayTransition();
        Invoke(nameof(GoToLevel), 2);
    }

    private void GoToLevel()
    {
        if (attachedScene == "Level-W01-L01")
        {
            attachedScene = "ComicAnim";
        }
        SceneManager.LoadScene(attachedScene);
    }
}
