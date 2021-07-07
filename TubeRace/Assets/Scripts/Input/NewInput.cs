using UnityEngine;

namespace TubeRace
{
    public abstract class NewInput : MonoBehaviour
    {
        public abstract Vector3 MoveDirection();

        public abstract bool EnableAfterburner();
    }
}