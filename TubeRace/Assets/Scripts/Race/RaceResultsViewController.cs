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

            place.text = "Place: " + stats.Place;
            topSpeed.text = "Top speed: " + ((int) stats.TopSpeed) + " m/s";
            totalTime.text = "Time: " + stats.TotalTime.ToString(CultureInfo.CurrentCulture) + " seconds";
            bestTime.text = "Best lap: " + stats.BestTime.ToString(CultureInfo.CurrentCulture) + " seconds";
        }

        public void OnButtonQuit()
        {
            UnityEngine.
                SceneManagement.SceneManager.
                LoadScene(PauseViewController.MainMenuScene);
        }
    }
}