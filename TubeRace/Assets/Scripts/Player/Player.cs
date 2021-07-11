using UnityEngine;

namespace TubeRace
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private NewInput newInput;

        [SerializeField] private Bike activeBike;

        private void Update()
        {
            CheckInput();
        }

        private void CheckInput()
        {
            if (!activeBike.IsMovementControlsActive)
                return;

            Vector3 direction = newInput.MoveDirection();

            activeBike.SetForwardThrustAxis(direction.y);
            activeBike.SetHorizontalThrustAxis(direction.x);

            activeBike.EnableAfterburner = newInput.EnableAfterburner();
        }
    }
}