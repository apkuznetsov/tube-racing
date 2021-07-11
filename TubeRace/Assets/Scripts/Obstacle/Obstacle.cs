using UnityEngine;

namespace TubeRace
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Track track;
        [SerializeField] private float distance;

        [Range(0.0f, 20.0f)] [SerializeField] private float radiusModifier = 1.0f;
        [SerializeField] private float angle;
        [Range(0.0f, 100.0f)] public float angularThrust;

        private Vector3 obstacleDirection;
        private Vector3 trackPosition;

        private Quaternion quater;
        private Vector3 trackOffset;

        private void UpdateAngle()
        {
            angle += angularThrust * Time.deltaTime;
            if (angle > 180.0f)
                angle -= 360.0f;
            else if (angle < -180.0f)
                angle = 360.0f + angle;
        }

        private void UpdatePosition()
        {
            quater = Quaternion.AngleAxis(angle, Vector3.forward);
            trackOffset = quater * (Vector3.up * (radiusModifier * track.Radius));

            transform.position = trackPosition - trackOffset;
            transform.rotation = Quaternion.LookRotation(obstacleDirection, trackOffset);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Vector3 centerlinePos = track.Position(distance);
            Gizmos.DrawSphere(centerlinePos, track.Radius);
        }

        private void OnValidate()
        {
            obstacleDirection = track.Direction(distance);
            trackPosition = track.Position(distance);
            UpdatePosition();
        }

        private void Update()
        {
            UpdateAngle();
            UpdatePosition();
        }
    }
}