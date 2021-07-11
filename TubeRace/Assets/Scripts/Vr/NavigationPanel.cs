using UnityEngine;

namespace TubeRace
{
    public class NavigationPanel : MonoBehaviour
    {
        [SerializeField] private GazePointer gazePointer;
        [Range(0.05f, 5f)] [SerializeField] private float deadZoneRadius;
        [Range(1f, 5f)] [SerializeField] private float maxRadius;

        private void OnDrawGizmos()
        {
            Vector3 position = transform.position;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(position, deadZoneRadius);

            Gizmos.color = new Color(0, 0, 1, 0.3f);
            Gizmos.DrawSphere(position, maxRadius);
        }

        public Vector3 MoveDirection()
        {
            Vector3 relPos = gazePointer.DirectionRelativePlane(transform.position);
            relPos = Vector3.ClampMagnitude(relPos, maxRadius);

            if (relPos.magnitude < deadZoneRadius)
                relPos = Vector3.zero;

            relPos = new Vector3(
                Vector3.Dot(transform.right, relPos),
                Vector3.Dot(transform.up, relPos));

            return relPos;
        }
    }
}