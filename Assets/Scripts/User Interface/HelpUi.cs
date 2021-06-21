using UnityEngine;
using UnityEngine.SceneManagement;

namespace User_Interface
{
    public class HelpUi : BaseUi
    {
        public GameObject credits;
    
        private void Start()
        {
            // Change music track
            MusicManager.i.ChangeMusicTrack(0);
        }

        private void Update()
        {
            CheckKeyInputs();
        }
        private void CheckKeyInputs()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitHelp();
            }
        }

        public void ExitHelp()
        {
            levelTransitionOverlay.SetBool("levelEndedOrDead", true);
            Invoke(nameof(ActuallyExit), 2);
        }

        private void ActuallyExit()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void ShowCredits()
        {
            credits.SetActive(!credits.activeSelf);
        }
    }
}
