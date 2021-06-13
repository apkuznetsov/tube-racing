using UnityEngine;

namespace TubeRace
{
    /// <summary>
    /// Класс линейного трека
    /// </summary>
    public class TrackLinear : Track
    {
        [Header("Linear track properties")] [SerializeField]
        private Transform start;

        [SerializeField] private Transform end;

        public override float Length()
        {
            return (end.position - start.position).magnitude;
        }

        public override Vector3 Position(float distance)
        {
            Vector3 startPosition = start.position;
            Vector3 direction = end.position - startPosition;

            return startPosition + direction.normalized * distance;
        }

        public override Vector3 Direction(float distance)
        {
            Mathf.Clamp(distance, 0, Length());

            return (end.position - start.position).normalized;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawLine(start.position, end.position);
        }
    }
}