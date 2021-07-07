using UnityEngine;
using UnityEngine.SceneManagement;

namespace TubeRace
{
    public class PauseViewController : MonoBehaviour
    {
        public const string MainMenuScene = "MainMenuScene";

        [SerializeField] private RectTransform content;

        [SerializeField] private RaceController raceController;

        private void Start()
        {
            content.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                if (raceController.IsRaceActive)
                {
                    GameObject go;
                    (go = content.gameObject).SetActive(!content.gameObject.activeInHierarchy);
                    UpdateGameActivity(!go.activeInHierarchy);
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