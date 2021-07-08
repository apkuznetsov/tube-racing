using UnityEngine;

namespace TubeRace
{
    public abstract class Powerup : MonoBehaviour
    {
        [SerializeField] private Track track;
        [SerializeField] private float distance;
        [SerializeField] private float angle;

        protected abstract void OnPicked(Bike bike);

        private void SetPosition()
        {
            Vector3 obstacleDir = track.Direction(distance);
            Vector3 trackPosition = track.Position(distance);

            Quaternion quater = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 trackOffset = quater * (Vector3.up * 0);

            transform.position = trackPosition - trackOffset;
            transform.rotation = Quaternion.LookRotation(obstacleDir, trackOffset);
        }

        private void UpdateBikes()
        {
            foreach (GameObject bikeGo in Bike.BikesAsGameObjects)
            {
                Bike bike = bikeGo.GetComponent<Bike>();

                float prev = bike.PrevDistance;
                float curr = bike.Distance;

                if (prev < distance && curr > distance)
                {
                    // limit angles
                    OnPicked(bike);
                }
            }
        }

        private void OnValidate()
        {
            SetPosition();
        }

        private void Update()
        {
            UpdateBikes();
        }
    }
}