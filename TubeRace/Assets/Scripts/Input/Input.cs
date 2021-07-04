using UnityEngine;

namespace TubeRace
{
    public abstract class Input : MonoBehaviour
    {
        public abstract Vector3 MoveDirection();

        public abstract bool EnableAfterburner();
    }
}