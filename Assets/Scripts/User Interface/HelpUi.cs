using UnityEngine;
using UnityEngine.SceneManagement;

namespace User_Interface
{
    public class HelpUi : BaseUi
    {
        public GameObject credits;
    
        private void Start()
        {
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

        public void VisitSite(string who)
        {
            switch (who)
            {
                case "Benchi":
                    Application.OpenURL("https://benchi99.itch.io/");
                    break;
                case "Cross":
                    Application.OpenURL("https://crossfirecam.itch.io/");
                    break;
                case "Darelt":
                    Application.OpenURL("https://darelt.itch.io/");
                    break;
                case "Frank":
                    Application.OpenURL("https://frankbusquets.itch.io/");
                    break;
                case "Tesla":
                    Application.OpenURL("https://teslasp2.itch.io/");
                    break;
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
