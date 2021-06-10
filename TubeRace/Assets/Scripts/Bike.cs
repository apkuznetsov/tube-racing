using System;
using UnityEngine;

namespace TubeRace
{
    /// <summary>
    /// Data model
    /// </summary>
    [Serializable]
    public class BikeParametersInitial
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

        /// <summary>
        /// Data model
        /// </summary>
        [SerializeField] private BikeParametersInitial initial;

        /// <summary>
        /// View
        /// </summary>
        [SerializeField] private BikeViewController visualController;

        [SerializeField] private Track track;

        private float afterburnerHeat;
        private float angularVelocity;

        /// <summary>
        /// Управление газом. Нормализованное. От -1 до +1
        /// </summary>
        private float forwardThrustAxis;

        /// <summary>
        /// Управление отклонением влево и вправо. Нормализованное. От -1 до +1
        /// </summary>
        private float horizontalThrustAxis;

        public Track Track => track;

        public float Fuel { get; private set; }

        public float Distance { get; private set; }
        public float PrevDistance { get; private set; }

        public float Velocity { get; private set; }

        public float Angle { get; private set; }

        /// <summary>
        /// Вкл/выкл доп. ускорителя
        /// </summary>
        public bool EnableAfterburner { get; set; }

        public float NormalizedHeat
        {
            get
            {
                if (initial.afterburnerMaxHeat > 0)
                    return afterburnerHeat / initial.afterburnerMaxHeat;

                return 0.0f;
            }
        }

        private void Start()
        {
            GameObjects = GameObject.FindGameObjectsWithTag(Tag);
        }

        private void Update()
        {
            UpdateAfterburnerHeat();
            UpdateBikePhysics();
        }

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

        public void CoolAfterburner()
        {
            afterburnerHeat = 0;
        }

        private void UpdateAfterburnerHeat()
        {
            afterburnerHeat -= initial.afterburnerCoolSpeed * Time.deltaTime;
            if (afterburnerHeat < 0)
                afterburnerHeat = 0;
        }

        private void UpdateBikeSpeed()
        {
            float dt = Time.deltaTime;
            float currMaxSpeed = initial.maxSpeed;

            float forceThrustMax = initial.thrust;
            float maxSpeed = initial.maxSpeed;
            float force = forwardThrustAxis * initial.thrust;
            if (EnableAfterburner
                && CanConsumeFuel(1.0f * Time.deltaTime))
            {
                afterburnerHeat += initial.afterburnerHeatGeneration * Time.deltaTime;

                force += initial.afterburnerThrust;
                maxSpeed += initial.afterburnerMaxSpeedBonus;
                forceThrustMax += initial.afterburnerThrust;
            }

            force += -Velocity * (forceThrustMax / maxSpeed);
            Velocity += force * dt;

            float ds = Velocity * dt;
            if (Physics.Raycast(transform.position, transform.forward, ds))
            {
                Velocity = -Velocity * initial.linearBounceFactor;
                ds = Velocity * dt;
            }

            PrevDistance = Distance;
            Distance += ds;
        }

        private void UpdateBikeAngle()
        {
            float dt = Time.deltaTime;
            angularVelocity += horizontalThrustAxis * initial.agility;
            Angle += angularVelocity * dt;

            if (Angle > 180.0f)
                Angle -= 360.0f;
            else if (Angle < -180.0f)
                Angle += 360.0f;

            angularVelocity += -angularVelocity * initial.angleDrag * dt;
            angularVelocity = Mathf.Clamp(angularVelocity,
                -initial.maxAngularSpeed, initial.maxAngularSpeed);
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
    }
}