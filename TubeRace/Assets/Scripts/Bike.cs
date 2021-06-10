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

        [Range(100.0f, 1000.0f)] public float maxSpeed;

        [Range(100.0f, 1000.0f)] public float maxAngularSpeed;

        [Range(0.0f, 100.0f)] public float thrust;
        [Range(0.0f, 100.0f)] public float agility;

        [Range(0.0f, 1.0f)] public float angleDrag;

        [Range(0.0f, 1.0f)] public float linearBounceFactor;
        [Range(0.0f, 1.0f)] public float angleBounceFactor;

        public float afterburnerThrust;
        public float afterburnerMaxSpeedBonus;

        public float afterburnerCoolSpeed;
        public float afterburnerHeatGeneration;
        public float afterburnerMaxHeat;

        public GameObject engineModel;
        public GameObject hullModel;
    }

    /// <summary>
    /// Controller. Entity
    /// </summary>
    public class Bike : MonoBehaviour
    {
        private const string Tag = "Bike";

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
        public Track Track => track;

        /// <summary>
        /// Управление газом. Нормализованное. От -1 до +1
        /// </summary>
        private float forwardThrustAxis;

        /// <summary>
        /// Установка значения педали газа
        /// </summary>
        /// <param name="val"></param>
        public void SetForwardThrustAxis(float val)
        {
            forwardThrustAxis = val;
        }

        /// <summary>
        /// Управление отклонением влево и вправо. Нормализованное. От -1 до +1
        /// </summary>
        private float horizontalThrustAxis;

        public void SetHorizontalThrustAxis(float val)
        {
            horizontalThrustAxis = val;
        }

        public float Fuel { get; private set; }

        public void AddFuel(float amount)
        {
            Fuel += amount;
            Fuel = Mathf.Clamp(Fuel, 0, 100);
        }

        private bool CanConsumeFuel(float amount)
        {
            if (Fuel < amount)
                return false;

            Fuel -= amount;
            return false;
        }

        public float Distance { get; private set; }
        public float PrevDistance { get; private set; }

        public float Velocity { get; private set; }

        public float Angle { get; private set; }
        private float angularVelocity;

        /// <summary>
        /// Вкл/выкл доп. ускорителя
        /// </summary>
        public bool EnableAfterburner { get; set; }

        private float afterburnerHeat;

        public float NormalizedHeat
        {
            get
            {
                if (bikeParameters.afterburnerMaxHeat > 0)
                    return afterburnerHeat / bikeParameters.afterburnerMaxHeat;

                return 0.0f;
            }
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
                && CanConsumeFuel(1.0f * Time.deltaTime))
            {
                afterburnerHeat += bikeParameters.afterburnerHeatGeneration * Time.deltaTime;

                force += bikeParameters.afterburnerThrust;
                maxSpeed += bikeParameters.afterburnerMaxSpeedBonus;
                forceThrustMax += bikeParameters.afterburnerThrust;
            }

            force += -Velocity * (forceThrustMax / maxSpeed);
            Velocity += force * dt;

            float ds = Velocity * dt;
            if (Physics.Raycast(transform.position, transform.forward, ds))
            {
                Velocity = -Velocity * bikeParameters.linearBounceFactor;
                ds = Velocity * dt;
            }

            PrevDistance = Distance;
            Distance += ds;
        }

        private void UpdateBikeAngle()
        {
            float dt = Time.deltaTime;
            angularVelocity += horizontalThrustAxis * bikeParameters.agility;
            Angle += angularVelocity * dt;

            // float ds = RollAngle * dt;
            // if (Physics.Raycast(transform.position, transform.right, ds))
            //     RollAngle -= bikeParameters.angleBounceFactor;
            // else if (Physics.Raycast(transform.position, -transform.right, ds))
            //     RollAngle += bikeParameters.angleBounceFactor;

            if (Angle > 180.0f)
                Angle -= 360.0f;
            else if (Angle < -180.0f)
                Angle += 360.0f;

            angularVelocity += -angularVelocity * bikeParameters.angleDrag * dt;
            angularVelocity = Mathf.Clamp(angularVelocity,
                -bikeParameters.maxAngularSpeed, bikeParameters.maxAngularSpeed);
        }

        private void UpdateBikePhysics()
        {
            UpdateBikeSpeed();
            UpdateBikeAngle();

            if (Distance < 0)
                Distance = 0;

            Vector3 bikePos = track.Position(Distance);
            Vector3 bikeDir = track.Direction(Distance);

            Quaternion quater = Quaternion.AngleAxis(Angle, Vector3.forward);
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