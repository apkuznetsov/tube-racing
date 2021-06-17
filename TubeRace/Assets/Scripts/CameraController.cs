using UnityEngine;

namespace TubeRace
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Bike bike;

        [SerializeField] private float minViewField = 60;
        [SerializeField] private float maxViewField = 85;
        private Camera cam;

        private void Start()
        {
            cam = Camera.main;
        }

        private void UpdateViewField()
        {
            float t = bike.NormalizedVelocity();
            cam.fieldOfView = Mathf.Lerp(minViewField, maxViewField, t);
        }

        private void Update()
        {
            UpdateViewField();
        }
    }
}