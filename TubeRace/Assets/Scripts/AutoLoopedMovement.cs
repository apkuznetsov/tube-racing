using UnityEngine;

namespace TubeRace
{
    public class AutoLoopedMovement : MonoBehaviour
    {
        [SerializeField] private Transform start;

        [SerializeField] private Transform end;

        private float length;

        private Vector3 direction;

        [SerializeField] private Transform bike;

        [SerializeField] private float bikeSpeed;

        private float bikeDistance;

        private const float LoopDistance = 15.0f;
    }
}
