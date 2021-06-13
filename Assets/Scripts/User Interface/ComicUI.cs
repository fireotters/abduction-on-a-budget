using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicUI : BaseUi
{
    public Animator _animComic;
    // Start is called before the first frame update
    private void Start()
    {
        // Change music track
        MusicManager.i.ChangeMusicTrack(0);

        // Fade in the screen
        StartCoroutine(UsefulFunctions.FadeScreenBlack("from", fullUiFadeBlack));
    }

    // Update is called once per frame
    void Update()
    {
        CheckKeyInputs();
    }
    private void CheckKeyInputs()
    {
        if (Input.anyKey)
        {
            ExitComic();
        }

    }

    public void ExitComic()
    {
        SceneManager.LoadScene("Level01");
    }
}
