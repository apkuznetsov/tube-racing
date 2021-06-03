using UnityEngine;

namespace TubeRace
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Track track;
        [SerializeField] private float rollAngle;
        [SerializeField] private float distance;

        [Range(0.0f, 20.0f)] [SerializeField] private float radiusModifier = 1.0f;

        private void SetObstaclePoistion()
        {
            Vector3 obstaclePos = track.Position(distance);
            Vector3 obstacleDir = track.Direction(distance);

            Quaternion quater = Quaternion.AngleAxis(rollAngle, Vector3.forward);
            Vector3 trackOffset = quater * (Vector3.up * (radiusModifier * track.Radius));

            transform.position = obstaclePos - trackOffset;
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
            SetObstaclePoistion();
        }
    }
}