using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TubeRace
{
    public class RaceResultsViewController : MonoBehaviour
    {
        [SerializeField] private Text place;
        [SerializeField] private Text topVelocity;
        [SerializeField] private Text totalSeconds;
        [SerializeField] private Text bestSeconds;

        public void Show(Bike.BikeStatistics stats)
        {
            gameObject.SetActive(true);

            place.text = "Place: " + stats.RacePlace;
            topVelocity.text = "Top speed: " + ((int) stats.BestVelocity) + " m/s";
            totalSeconds.text = "Time: " + stats.TotalSeconds.ToString(CultureInfo.CurrentCulture) + " seconds";
            bestSeconds.text = "Best lap: " + stats.BestSeconds.ToString(CultureInfo.CurrentCulture) + " seconds";
        }

        public void OnButtonQuit()
        {
            SceneManager.LoadScene(PauseViewController.MainMenuScene);
        }

        private void Awake()
        {
            gameObject.SetActive(false);
        }
    }
}