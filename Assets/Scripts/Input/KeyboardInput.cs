using UnityEngine;

namespace TubeRace
{
    public class KeyboardInput : NewInput
    {
        public override Vector3 MoveDirection()
        {
            Vector3 direction = new Vector3();

            if (Input.GetKey(KeyCode.W))
                direction.y = 1;

            if (Input.GetKey(KeyCode.S))
                direction.y = -1;

            if (Input.GetKey(KeyCode.A))
                direction.x = -1;

            if (Input.GetKey(KeyCode.D))
                direction.x = 1;

            return direction;
        }

        public override bool EnableAfterburner()
        {
            return Input.GetKey(KeyCode.Space);
        }
    }
}