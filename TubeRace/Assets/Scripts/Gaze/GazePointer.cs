using UnityEngine;

namespace Gaze
{
    public class GazePointer : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [Range(1, 50)] [SerializeField] private float gazeRange;

        public Vector3 DirectionRelativePlane(Vector3 planePosition)
        {
            Transform camTransform = cam.transform;
            Ray ray = new Ray(camTransform.position, camTransform.forward);

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