using UnityEngine;

namespace TubeRace
{
    public class CurvedTrackPoint : MonoBehaviour
    {
        [SerializeField] private float length = 1.0f;

        public float Length => length;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawSphere(transform.position, 10.0f);
        }
    }
}