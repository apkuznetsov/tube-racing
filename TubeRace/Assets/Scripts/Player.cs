using Gaze;
using UnityEngine;

namespace TubeRace
{
    /// <summary>
    /// Игрок гонки, бот, человек
    /// </summary>
    public class Player : MonoBehaviour
    {
        [SerializeField] private Input input;

        [SerializeField] private NavigationPanel navigationPanel;

        [SerializeField] private string nickname;

        [SerializeField] private Bike activeBike;

        private void CheckInput()
        {
            if (!activeBike.IsMovementControlsActive)
                return;

            Vector3 direction = input.MoveDirection();

            activeBike.SetForwardThrustAxis(direction.y);
            activeBike.SetHorizontalThrustAxis(direction.x);

            activeBike.EnableAfterburner = input.EnableAfterburner();
        }

        private void Update()
        {
            CheckInput();
        }
    }
}