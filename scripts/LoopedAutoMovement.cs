using UnityEngine;

namespace TubeRace
{
    public class LoopedAutoMovement : MonoBehaviour
    {
        private const float LoopDistance = 15.0f;

        [SerializeField] private Transform start;
        [SerializeField] private Transform end;

        [SerializeField] private Transform bike;
        [SerializeField] private float bikeSpeed;

        private float length;
        private Vector3 direction;

        private float bikeDistance;

        private void LoopBikePosition()
        {
            bool isEnd = (int) bikeDistance > (int) length - (int) LoopDistance;
            bool isStart = (int) bikeDistance < (int) LoopDistance;

            if (isEnd)
                bikeDistance = LoopDistance;
            else if (isStart)
                bikeDistance = length - LoopDistance;

            bike.position = start.position + direction.normalized * bikeDistance;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(start.position, end.position);
        }

        private void Start()
        {
            Vector3 vectorDifference = end.position - start.position;
            length = vectorDifference.magnitude;
            direction = vectorDifference.normalized;

            bike.forward = direction;
        }

        private void Update()
        {
            bikeDistance += bikeSpeed * Time.deltaTime;
            LoopBikePosition();
        }
    }
}