using UnityEngine;

namespace TubeRace
{
    public class GazePointer : MonoBehaviour
    {
        [SerializeField] private Camera thisCamera;
        [Range(1, 50)] [SerializeField] private float gazeRange;

        public Vector3 DirectionRelativePlane(Vector3 planePosition)
        {
            Transform transformCamera = thisCamera.transform;
            Ray ray = new Ray(transformCamera.position, transformCamera.forward);

            bool isCollision = Physics.Raycast(
                ray, out RaycastHit rayHit,
                gazeRange,
                LayerMask.GetMask("NavigationPanel")
            );

            if (isCollision)
            {
                Vector3 hitPosition = rayHit.point;
                Debug.DrawLine(transform.position, hitPosition);

                Vector3 relativePanel = hitPosition - planePosition;
                return relativePanel;
            }

            return Vector3.zero;
        }
    }
}