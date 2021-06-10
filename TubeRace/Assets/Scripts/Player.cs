using UnityEngine;

namespace TubeRace
{
    /// <summary>
    /// Игрок гонки, бот, человек
    /// </summary>
    public class Player : MonoBehaviour
    {
        [SerializeField] private string nickname;

        [SerializeField] private Bike activeBike;

        private void ControlBike()
        {
            activeBike.SetForwardThrustAxis(0);
            activeBike.SetHorizontalThrustAxis(0);

            if (Input.GetKey(KeyCode.W))
                activeBike.SetForwardThrustAxis(1);

            if (Input.GetKey(KeyCode.S))
                activeBike.SetForwardThrustAxis(-1);

            if (Input.GetKey(KeyCode.A))
                activeBike.SetHorizontalThrustAxis(-1);

            if (Input.GetKey(KeyCode.D))
                activeBike.SetHorizontalThrustAxis(1);

            activeBike.EnableAfterburner = Input.GetKey(KeyCode.Space);
        }

        private void Update()
        {
            ControlBike();
        }
    }
}