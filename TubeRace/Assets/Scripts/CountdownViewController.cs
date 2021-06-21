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
            int t = (int) raceController.CountTimer;

            if (t != 0)
            {
                label.text = t.ToString();
            }
            else
            {
                label.text = "";
                gameObject.SetActive(false);
            }
        }
    }
}