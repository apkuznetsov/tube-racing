using UnityEngine;

namespace TubeRace
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Track track;

        [SerializeField] private float distance;
        [Range(0.0f, 20.0f)] [SerializeField] private float radiusModifier = 1.0f;
        [SerializeField] private float rollAngle;

        [Range(0.0f, 100.0f)] public float agility;

        private Vector3 obstacleDir;
        private Vector3 trackPosition;

        private Quaternion quater;
        private Vector3 trackOffset;

        private void SetPosition()
        {
            quater = Quaternion.AngleAxis(rollAngle, Vector3.forward);
            trackOffset = quater * (Vector3.up * (radiusModifier * track.Radius));

            transform.position = trackPosition - trackOffset;
            transform.rotation = Quaternion.LookRotation(obstacleDir, trackOffset);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 centerlinePos = track.Position(distance);
            Gizmos.DrawSphere(centerlinePos, track.Radius);
        }

        private void OnValidate()
        {
            obstacleDir = track.Direction(distance);
            trackPosition = track.Position(distance);
            SetPosition();
        }

        private void Update()
        {
            rollAngle += agility * Time.deltaTime;
            if (rollAngle > 180.0f)
                rollAngle -= 360.0f;
            else if (rollAngle < -180.0f)
                rollAngle = 360.0f + rollAngle;

            SetPosition();
        }
    }
}