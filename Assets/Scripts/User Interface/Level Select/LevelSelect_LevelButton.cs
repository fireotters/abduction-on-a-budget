using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect_LevelButton : MonoBehaviour
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
        SceneManager.LoadScene(attachedScene);
    }
}
