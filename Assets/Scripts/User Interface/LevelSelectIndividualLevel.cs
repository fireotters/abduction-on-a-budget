using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectIndividualLevel : MonoBehaviour
{
    private Button _button;
    [SerializeField] private string _attachedScene;

    private void Start()
    {
        _button = GetComponent<Button>();
        if (_attachedScene != "")
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
        SceneManager.LoadScene(_attachedScene);
    }
}
