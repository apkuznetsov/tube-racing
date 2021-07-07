using Vr;
using UnityEngine;

namespace TubeRace
{
    /// <summary>
    /// Игрок гонки, бот, человек
    /// </summary>
    public class Player : MonoBehaviour
    {
        [SerializeField] private NewInput newInput;

        [SerializeField] private Bike activeBike;

        private void CheckInput()
        {
            if (!activeBike.IsMovementControlsActive)
                return;

            Vector3 direction = newInput.MoveDirection();

            activeBike.SetForwardThrustAxis(direction.y);
            activeBike.SetHorizontalThrustAxis(direction.x);

            activeBike.EnableAfterburner = newInput.EnableAfterburner();
        }

        private void Update()
        {
            CheckInput();
        }
    }
}