using UnityEngine;

namespace TubeRace
{
    public class KeyboardCustomInput : CustomInput
    {
        public override Vector3 MoveDirection()
        {
            Vector3 direction = new Vector3();

            if (UnityEngine.Input.GetKey(KeyCode.W))
                direction.y = 1;

            if (UnityEngine.Input.GetKey(KeyCode.S))
                direction.y = -1;

            if (UnityEngine.Input.GetKey(KeyCode.A))
                direction.x = -1;

            if (UnityEngine.Input.GetKey(KeyCode.D))
                direction.x = 1;

            return direction;
        }

        public override bool EnableAfterburner()
        {
            return UnityEngine.Input.GetKey(KeyCode.Space);
        }
    }
}