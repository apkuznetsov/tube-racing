using System;
using UnityEngine;

namespace TubeRace
{
    /// <summary>
    /// Data model
    /// </summary>
    [Serializable]
    public class BikeParameters
    {
        [Range(0.0f, 10.0f)] public float mass;

        [Range(0.0f, 100.0f)] public float thrust;

        [Range(0.0f, 100.0f)] public float agility;

        public float maxSpeed;

        [Range(0.0f, 1.0f)] public float linearDrag;

        [Range(0.0f, 1.0f)] public float collisionBounceFactor;

        public bool afterburner;

        public GameObject engineModel;
        public GameObject hullModel;
    }

    /// <summary>
    /// Controller. Entity
    /// </summary>
    public class Bike : MonoBehaviour
    {
        /// <summary>
        /// Data model
        /// </summary>
        [SerializeField] private BikeParameters bikeParameters;

        /// <summary>
        /// View
        /// </summary>
        [SerializeField] private BikeViewController visualController;

        [SerializeField] private Track track;

        private float distance;
        private float velocity;
        private float rollAngle;

        /// <summary>
        /// Управление газом. Нормализованное. От -1 до +1
        /// </summary>
        private float forwardThrustAxis;

        /// <summary>
        /// Управление отклонением влево и вправо. Нормализованное. От -1 до +1
        /// </summary>
        private float horizontalThrustAxis;

        /// <summary>
        /// Установка значения педали газа
        /// </summary>
        /// <param name="val"></param>
        public void SetForwardThrustAxis(float val)
        {
            forwardThrustAxis = val;
        }

        public void SetHorizontalThrustAxis(float val)
        {
            horizontalThrustAxis = val;
        }

        private void UpdateBikePhysics()
        {
            float dt = Time.deltaTime;
            float dv = dt * forwardThrustAxis * bikeParameters.thrust;
            velocity += dv;

            float currMaxSpeed = bikeParameters.maxSpeed;
            velocity = Mathf.Clamp(velocity, -currMaxSpeed, currMaxSpeed);

            float ds = velocity * dt;
            
            if (Physics.Raycast(transform.position, transform.forward, ds))
            {
                velocity = -velocity * bikeParameters.collisionBounceFactor;
                ds = velocity * dt;
            }

            distance += ds;

            velocity += -velocity * bikeParameters.linearDrag * dt;

            if (distance < 0)
                distance = 0;

            rollAngle += bikeParameters.agility * horizontalThrustAxis * dt;

            Vector3 bikePos = track.Position(distance);
            Vector3 bikeDir = track.Direction(distance);

            Quaternion quater = Quaternion.AngleAxis(rollAngle, Vector3.forward);
            Vector3 trackOffset = quater * (Vector3.up * track.Radius);

            transform.position = bikePos - trackOffset;
            transform.rotation = Quaternion.LookRotation(bikeDir, trackOffset);
        }

        private void Update()
        {
            UpdateBikePhysics();
        }
    }
}