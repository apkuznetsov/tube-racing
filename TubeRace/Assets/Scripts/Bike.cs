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
        [Range(0.0f, 1.0f)] public float angleDrag;

        [Range(0.0f, 1.0f)] public float linearBounceFactor;
        [Range(0.0f, 1.0f)] public float angleBounceFactor;

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

        public float Distance => distance;
        public float Velocity => velocity;
        public float RollAngle => rollAngle;

        public Track Track => track;

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

        private void UpdateBikeSpeed()
        {
            float dt = Time.deltaTime;
            float currMaxSpeed = bikeParameters.maxSpeed;

            velocity += forwardThrustAxis * bikeParameters.thrust * dt;
            velocity = Mathf.Clamp(velocity, -currMaxSpeed, currMaxSpeed);

            float ds = velocity * dt;
            if (Physics.Raycast(transform.position, transform.forward, ds))
            {
                velocity = -velocity * bikeParameters.linearBounceFactor;
                ds = velocity * dt;
            }

            distance += ds;

            velocity += -velocity * bikeParameters.linearDrag * dt;
        }

        private void UpdateBikeRollAngle()
        {
            float dt = Time.deltaTime;
            rollAngle += horizontalThrustAxis * bikeParameters.agility * dt;
            if (rollAngle > 180.0f)
                rollAngle -= 360.0f;
            else if (rollAngle < -180.0f)
                rollAngle = 360.0f + rollAngle;

            float ds = rollAngle * dt;
            if (Physics.Raycast(transform.position, transform.right, ds))
                rollAngle -= bikeParameters.angleBounceFactor;
            else if (Physics.Raycast(transform.position, -transform.right, ds))
                rollAngle += bikeParameters.angleBounceFactor;

            if (horizontalThrustAxis == 0)
                rollAngle += -rollAngle * bikeParameters.angleDrag * dt;
        }

        private void UpdateBikePhysics()
        {
            UpdateBikeSpeed();
            UpdateBikeRollAngle();

            if (distance < 0)
                distance = 0;

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