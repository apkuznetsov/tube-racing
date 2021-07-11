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
        private Vector3 initialLocalPosition;

        private Camera thisCamera;

        private void Start()
        {
            thisCamera = Camera.main;
            initialLocalPosition = thisCamera.transform.localPosition;
        }

        private void Update()
        {
            UpdateViewField();
            UpdateCameraShake();
        }

        private void UpdateViewField()
        {
            float t = bike.NormalizedVelocity();
            thisCamera.fieldOfView = Mathf.Lerp(minViewField, maxViewField, t);
        }

        private void UpdateCameraShake()
        {
            if (Time.timeScale <= 0)
                return;

            float t = bike.NormalizedVelocity();
            float curveValue = shakeCurve.Evaluate(t);

            Vector3 randomVector = Random.insideUnitSphere * shakeFactor;
            randomVector.z = 0;

            thisCamera.transform.localPosition = initialLocalPosition + randomVector * curveValue;
        }
    }
}