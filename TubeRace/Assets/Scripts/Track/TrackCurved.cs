using UnityEngine;

namespace TubeRace
{
    public class TrackCurved : Track
    {
        public override float Length()
        {
            return 1.0f;
        }

        public override Vector3 Position(float distance)
        {
            return Vector3.zero;
        }

        public override Vector3 Direction(float distance)
        {
            return Vector3.zero;
        }
    }
}