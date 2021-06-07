using UnityEngine;
using UnityEngine.UI;

namespace TubeRace.UserInterface
{
    public class BikeHudViewController : MonoBehaviour
    {
        [SerializeField] private Text labelSpeed;
        [SerializeField] private Text labelDistance;
        [SerializeField] private Text labelRollAngle;
        [SerializeField] private Text labelLapNumber;

        [SerializeField] private Bike bike;

        private void Update()
        {
            labelSpeed.text = "Speed: " + (int) (bike.Velocity) + " m/s";
            labelDistance.text = "Distance: " + (int) (bike.Distance) + " m";
            labelRollAngle.text = "Angle: " + (int) (bike.RollAngle) + " deg";
            labelRollAngle.text = "Angle: " + (int) (bike.RollAngle) + " deg";

            int laps = (int) (bike.Distance / bike.Track.Length());
            labelLapNumber.text = "Lap: " + (laps + 1).ToString();
        }
    }
}