using UnityEngine;
using UnityEngine.SceneManagement;

namespace TubeRace
{
    public class PauseViewController : MonoBehaviour
    {
        public static string MainMenuScene = "MainMenuScene";

        [SerializeField] private RectTransform content;

        [SerializeField] private RaceController raceController;

        private void Start()
        {
            content.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (raceController.IsRaceActive)
                {
                    content.gameObject.SetActive(!content.gameObject.activeInHierarchy);
                    UpdateGameActivity(!content.gameObject.activeInHierarchy);
                }
            }
        }

        private void UpdateGameActivity(bool canUpdate)
        {
            Time.timeScale = canUpdate ? 1 : 0;
        }

        public void OnButtonContinue()
        {
            UpdateGameActivity(true);
            content.gameObject.SetActive(false);
        }

        public void OnButtonEndRace()
        {
            SceneManager.LoadScene(MainMenuScene);
        }
    }
}