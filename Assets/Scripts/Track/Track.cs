using UnityEngine;

namespace TubeRace
{
    public abstract class Track : MonoBehaviour
    {
        [Header("Base track properties")] [SerializeField]
        private float radius;

        public float Radius => radius;

        public abstract float Length();

        public abstract Vector3 Position(float distance);

        public abstract Vector3 Direction(float distance);

        public virtual Quaternion Rotation(float distance)
        {
            return Quaternion.identity;
        }
    }
}