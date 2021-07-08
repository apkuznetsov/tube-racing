using UnityEngine;
using UnityEngine.SceneManagement;

namespace TubeRace
{
    public class PauseViewController : MonoBehaviour
    {
        public const string MainMenuScene = "MainMenuScene";

        [SerializeField] private RaceController raceController;
        [SerializeField] private RectTransform content;

        public void OnButtonContinue()
        {
            Time.timeScale = 1;
            content.gameObject.SetActive(false);
        }

        public void OnButtonEndRace()
        {
            SceneManager.LoadScene(MainMenuScene);
        }

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
                    GameObject go = content.gameObject;
                    go.SetActive(!go.activeInHierarchy);

                    bool canUpdate = !go.activeInHierarchy;
                    Time.timeScale = canUpdate ? 1 : 0;
                }
            }
        }
    }
}