using UnityEngine;
using UnityEngine.UI;

namespace TubeRace
{
    public class BikeHudViewController : MonoBehaviour
    {
        [SerializeField] private Text labelSpeed;
        [SerializeField] private Text labelDistance;
        [SerializeField] private Text labelLapNumber;

        [SerializeField] private Text labelRollAngle;

        [SerializeField] private Text labelHeat;
        [SerializeField] private Text labelFuel;

        [SerializeField] private Bike bike;

        private void Update()
        {
            labelSpeed.text = "Speed: " + (int) (bike.Velocity) + " m/s";
            labelDistance.text = "Distance: " + (int) (bike.Distance) + " m";

            int laps = (int) (bike.Distance / bike.Track.Length()) + 1;
            labelLapNumber.text = "Lap: " + laps;

            labelRollAngle.text = "Angle: " + (int) (bike.Angle) + " deg";

            labelHeat.text = "Heat: " + (int) (bike.NormalizedHeat * 100.0f);
            labelFuel.text = "Fuel: " + (int) bike.Fuel;
        }
    }
}