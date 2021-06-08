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

        public float maxSpeed;

        [Range(0.0f, 100.0f)] public float agility;

        public float afterburnerThrust;
        public float afterburnerMaxSpeedBonus;

        public float afterburnerCoolSpeed;
        public float afterburnerHeatGeneration;
        public float afterburnerMaxHeat;

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
        public static readonly string Tag = "Bike";

        public static GameObject[] GameObjects;

        private void Start()
        {
            GameObjects = GameObject.FindGameObjectsWithTag(Tag);
        }

        /// <summary>
        /// Data model
        /// </summary>
        [SerializeField] private BikeParameters bikeParameters;

        /// <summary>
        /// View
        /// </summary>
        [SerializeField] private BikeViewController visualController;

        [SerializeField] private Track track;

        private float afterburnerHeat;
        private float fuel;

        public float NormalizedHeat()
        {
            if (bikeParameters.afterburnerMaxHeat > 0)
                return afterburnerHeat / bikeParameters.afterburnerMaxHeat;

            return 0.0f;
        }

        private float distance;
        private float velocity;
        private float rollAngle;

        private float prevDistance;

        public float Fuel => fuel;

        public float Distance => distance;
        public float Velocity => velocity;
        public float RollAngle => rollAngle;

        public float PrevDistance => prevDistance;

        public Track Track => track;

        public void AddFuel(float amount)
        {
            fuel += amount;

            fuel = Mathf.Clamp(fuel, 0 , 100);
        }
        
        private bool CanConsumeFuelForAfterburner(float amount)
        {
            if (fuel < amount)
                return false;

            fuel -= amount;

            return false;
        }

        /// <summary>
        /// Вкл/выкл доп. ускорителя
        /// </summary>
        public bool EnableAfterburner { get; set; }

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

        public void CoolAfterburner()
        {
            afterburnerHeat = 0;
        }

        private void UpdateAfterburnerHeat()
        {
            afterburnerHeat -= bikeParameters.afterburnerCoolSpeed * Time.deltaTime;
            if (afterburnerHeat < 0)
                afterburnerHeat = 0;
        }

        private void UpdateBikeSpeed()
        {
            float dt = Time.deltaTime;
            float currMaxSpeed = bikeParameters.maxSpeed;

            float forceThrustMax = bikeParameters.thrust;
            float maxSpeed = bikeParameters.maxSpeed;
            float force = forwardThrustAxis * bikeParameters.thrust;
            if (EnableAfterburner
                && CanConsumeFuelForAfterburner(1.0f * Time.deltaTime))
            {
                afterburnerHeat += bikeParameters.afterburnerHeatGeneration * Time.deltaTime;
                
                force += bikeParameters.afterburnerThrust;
                maxSpeed += bikeParameters.afterburnerMaxSpeedBonus;
                forceThrustMax += bikeParameters.afterburnerThrust;
            }

            force += -velocity * (forceThrustMax / maxSpeed);
            velocity += force * dt;

            float ds = velocity * dt;
            if (Physics.Raycast(transform.position, transform.forward, ds))
            {
                velocity = -velocity * bikeParameters.linearBounceFactor;
                ds = velocity * dt;
            }

            prevDistance = distance;
            distance += ds;
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
            UpdateAfterburnerHeat();
            UpdateBikePhysics();
        }
    }
}