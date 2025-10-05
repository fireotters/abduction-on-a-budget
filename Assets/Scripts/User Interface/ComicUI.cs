using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace User_Interface
{
    public class ComicUI : BaseUi
    {
        public Image fullUiFadeBlack;
        
        // Start is called before the first frame update
        private void Start()
        {
            // Change music track
            MusicManager.i.ChangeMusicTrack(0);
            // Fade in the screen
            StartCoroutine(UsefulFunctions.FadeScreenBlack("from", fullUiFadeBlack));
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.anyKey)
            {
                ExitComic();
            }
        }

        private static void ExitComic()
        {
            SceneManager.LoadScene("Level-W01-L01");
        }
    }
}
