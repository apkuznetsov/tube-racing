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

        [SerializeField] private Transform bike;

        [SerializeField] private float bikeSpeed;

        private float bikeDistance;

        public override float Length()
        {
            return (end.position - start.position).magnitude;
        }

        public override Vector3 Position(float distance)
        {
            float length = Length();

            int coeff = (int) (distance / length);
            float difference = distance - coeff * length;
            distance = (distance >= 0)
                ? difference
                : difference + length;

            Vector3 startPosition = start.position;
            Vector3 direction = end.position - startPosition;

            return startPosition + direction.normalized * distance;
        }

        public override Vector3 Direction(float distance)
        {
            distance = Mathf.Clamp(distance, 0, Length());

            return (end.position - start.position).normalized;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawLine(start.position, end.position);
        }

        private void Update()
        {
            bikeDistance += bikeSpeed;
            bike.position = Position(bikeDistance);
            bike.forward = Direction(bikeDistance);
        }
    }
}
