using UnityEngine;
using UnityEngine.UI;

namespace TubeRace
{
    public class CountdownViewController : MonoBehaviour
    {
        [SerializeField] private RaceController raceController;
        [SerializeField] private Text label;

        private void Update()
        {
            float currSeconds = raceController.CountTimer;

            if (currSeconds > 0.0f && currSeconds < 1.0f)
                label.text = "GO";
            else if (currSeconds > 1)
                label.text = ((int) currSeconds).ToString();
            else
                gameObject.SetActive(false);
        }
    }
}