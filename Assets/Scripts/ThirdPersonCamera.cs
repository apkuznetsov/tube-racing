using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform targetCenter;

    [SerializeField] private float interpolationLinear;
    [SerializeField] private float interpolationAngular;

    [SerializeField] private float forwardObserveDistance;

    private void FixedUpdate()
    {
        var transformPosition = transform.position;
        transformPosition = Vector3.Lerp(transformPosition, target.position, interpolationLinear * Time.deltaTime);

        var cameraTransform = transform;
        cameraTransform.position = transformPosition;

        Vector3 fw = targetCenter.position + forwardObserveDistance * targetCenter.forward;
        transform.rotation = Quaternion.LookRotation(
            fw - transformPosition,
            Vector3.Lerp(cameraTransform.up, target.up, interpolationAngular * Time.deltaTime));
    }
}