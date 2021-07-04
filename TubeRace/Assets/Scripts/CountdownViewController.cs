using UnityEngine;
using UnityEngine.UI;

namespace TubeRace
{
    public class CountdownViewController : MonoBehaviour
    {
        [SerializeField] private RaceController raceController;
        [SerializeField] private Text label;

        private void DisableCountdown()
        {
            label.text = "";
            gameObject.SetActive(false);
        }

        private void UpdateCountdown()
        {
            float currSeconds = raceController.CountTimer + 1;

            if (currSeconds > 0.0f && currSeconds < 1.0f)
            {
                label.text = "GO";
                Invoke(nameof(DisableCountdown), 1.0f);
            }
            else
            {
                label.text = ((int) currSeconds).ToString();
            }
        }

        private void Update()
        {
            UpdateCountdown();
        }
    }
}