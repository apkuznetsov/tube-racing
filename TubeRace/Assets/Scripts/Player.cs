using Gaze;
using UnityEngine;

namespace TubeRace
{
    /// <summary>
    /// Игрок гонки, бот, человек
    /// </summary>
    public class Player : MonoBehaviour
    {
        private enum InputType
        {
            Keyboard = 0,
            Gaze = 1
        }

        [SerializeField] private InputType inputType;
        [SerializeField] private NavigationPanel navigationPanel;

        [SerializeField] private string nickname;

        [SerializeField] private Bike activeBike;

        private void ControlWithKeyboard()
        {
            activeBike.SetForwardThrustAxis(0);
            activeBike.SetHorizontalThrustAxis(0);

            if (!activeBike.IsMovementControlsActive)
                return;

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

        private void ControlWithGaze()
        {
            Vector3 direction = navigationPanel.MoveDirection();

            activeBike.SetForwardThrustAxis(direction.y);
            activeBike.SetHorizontalThrustAxis(direction.x);
        }

        private void Update()
        {
            if (inputType == InputType.Keyboard)
                ControlWithKeyboard();
            else
                ControlWithGaze();
        }
    }
}