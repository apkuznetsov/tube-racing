using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace TubeRace
{
    public class RaceResultsViewController : MonoBehaviour
    {
        [SerializeField] private Text place;
        [SerializeField] private Text topSpeed;
        [SerializeField] private Text totalTime;
        [SerializeField] private Text bestTime;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Show(Bike.BikeStatistics stats)
        {
            gameObject.SetActive(true);

            place.text = "Place: " + stats.RacePlace;
            topSpeed.text = "Top speed: " + ((int) stats.BestVelocity) + " m/s";
            totalTime.text = "Time: " + stats.TotalSeconds.ToString(CultureInfo.CurrentCulture) + " seconds";
            bestTime.text = "Best lap: " + stats.BestSeconds.ToString(CultureInfo.CurrentCulture) + " seconds";
        }

        public void OnButtonQuit()
        {
            UnityEngine.
                SceneManagement.SceneManager.
                LoadScene(PauseViewController.MainMenuScene);
        }
    }
}