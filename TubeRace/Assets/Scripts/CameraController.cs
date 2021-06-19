using UnityEngine;

namespace TubeRace
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Bike bike;

        [SerializeField] private float minViewField = 60;
        [SerializeField] private float maxViewField = 85;
        [SerializeField] private float shakeFactor;
        [SerializeField] private AnimationCurve shakeCurve;

        private Camera cam;
        private Vector3 initialLocalPosition;

        private void Start()
        {
            cam = Camera.main;
            initialLocalPosition = cam.transform.localPosition;
        }

        private void UpdateCameraShake()
        {
            float t = bike.NormalizedVelocity();
            float curveValue = shakeCurve.Evaluate(t);
            
            Vector3 randomVector = Random.insideUnitSphere * shakeFactor;
            randomVector.z = 0;

            cam.transform.localPosition = initialLocalPosition + randomVector * curveValue;
        }

        private void UpdateViewField()
        {
            float t = bike.NormalizedVelocity();
            cam.fieldOfView = Mathf.Lerp(minViewField, maxViewField, t);
        }

        private void Update()
        {
            UpdateViewField();
            UpdateCameraShake();
        }
    }
}