using UnityEngine;

namespace TubeRace
{
    public abstract class CustomInput : MonoBehaviour
    {
        public abstract Vector3 MoveDirection();

        public abstract bool EnableAfterburner();
    }
}