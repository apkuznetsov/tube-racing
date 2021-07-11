using UnityEngine;
using UnityEngine.SceneManagement;

namespace TubeRace
{
    public class PauseViewController : MonoBehaviour
    {
        public const string MainMenuScene = "scene_main_menu";

        [SerializeField] private RaceController raceController;
        [SerializeField] private RectTransform content;

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

        public void OnButtonContinue()
        {
            Time.timeScale = 1;
            content.gameObject.SetActive(false);
        }

        public void OnButtonEndRace()
        {
            SceneManager.LoadScene(MainMenuScene);
        }
    }
}