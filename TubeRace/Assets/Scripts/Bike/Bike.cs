using System;
using System.Collections.Generic;
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

        [Range(100.0f, 1000.0f)] public float maxVelocity;
        [Range(0.0f, 100.0f)] public float thrust;
        [Range(0.0f, 1.0f)] public float bounceFactor;

        [Range(100.0f, 1000.0f)] public float maxAngularVelocity;
        [Range(0.0f, 100.0f)] public float angularThrust;
        [Range(0.0f, 1.0f)] public float angularDrag;

        public float afterburnerThrust;
        public float afterburnerMaxVelocityBonus;

        public float afterburnerCoolSpeed;
        public float afterburnerHeatSpeed;
        public float afterburnerMaxHeat;

        public GameObject engineModel;
        public GameObject hullModel;
    }

    /// <summary>
    /// Controller. Entity
    /// </summary>
    public class Bike : MonoBehaviour
    {
        public class BikeStatistics
        {
            public float TopSpeed;
            public float TotalTime;
            public float BestTime;
            public int Place;
        }

        public BikeStatistics Statistics { get; private set; }

        private List<float> lapDurations;
        private int lap;
        private float lapStartTime;

        private void Awake()
        {
            Statistics = new BikeStatistics();
            lapDurations = new List<float>();
        }

        private float raceStartTime;

        public void OnRaceStart()
        {
            Statistics.Place = 0;
            Statistics.TotalTime = 0;
            Statistics.BestTime = 0;

            raceStartTime = Time.time;
            lapStartTime = raceStartTime;
        }

        public void OnRaceEnd()
        {
            Statistics.TotalTime = Time.time - raceStartTime;
        }

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

        [SerializeField] private bool isPlayerBike;
        public bool IsPlayerBike => isPlayerBike;

        public bool IsMovementControlsActive { get; set; }

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
            UpdateBestTime();
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
            return true;
        }

        public void CoolAfterburner()
        {
            afterburnerHeat = 0;
        }

        public void Slowdown(int percent)
        {
            Velocity -= Velocity * percent / 100.0f;
        }

        private void Heat()
        {
            afterburnerHeat += Velocity;
        }

        private void UpdateAfterburnerHeat()
        {
            afterburnerHeat -= initial.afterburnerCoolSpeed * Time.deltaTime;

            if (afterburnerHeat < 0)
                afterburnerHeat = 0;

            if (afterburnerHeat > initial.afterburnerMaxHeat)
                Slowdown(100);
        }

        public float NormalizedVelocity()
        {
            return Mathf.Clamp01(Velocity / initial.maxVelocity);
        }

        private void UpdateBikeVelocity()
        {
            float dt = Time.deltaTime;

            float forceThrustMax = initial.thrust;
            float velocityMax = initial.maxVelocity;
            float force = forwardThrustAxis * initial.thrust;

            if (EnableAfterburner
                && CanConsumeFuel(1.0f * Time.deltaTime))
            {
                afterburnerHeat += initial.afterburnerHeatSpeed * Time.deltaTime;

                force += initial.afterburnerThrust;
                velocityMax += initial.afterburnerMaxVelocityBonus;
                forceThrustMax += initial.afterburnerThrust;
            }

            float forceDrag = -Velocity * (forceThrustMax / velocityMax);
            force += forceDrag;

            Velocity += force * dt;
            if (Statistics.TopSpeed < Mathf.Abs(Velocity))
                Statistics.TopSpeed = Mathf.Abs(Velocity);

            float ds = Velocity * dt;
            if (Physics.Raycast(transform.position, transform.forward, ds))
            {
                Heat();

                Velocity = -Velocity * initial.bounceFactor;
                ds = Velocity * dt;
            }

            PrevDistance = Distance;
            Distance += ds;
        }

        private void UpdateBikeAngle()
        {
            float dt = Time.deltaTime;
            angularVelocity += horizontalThrustAxis * initial.angularThrust;
            Angle += angularVelocity * dt;

            if (Angle > 180.0f)
                Angle -= 360.0f;
            else if (Angle < -180.0f)
                Angle += 360.0f;

            angularVelocity += -angularVelocity * initial.angularDrag * dt;
            angularVelocity = Mathf.Clamp(angularVelocity,
                -initial.maxAngularVelocity, initial.maxAngularVelocity);
        }

        private void UpdateBikePhysics()
        {
            UpdateBikeVelocity();
            UpdateBikeAngle();

            if (Distance < 0)
                Distance = 0;

            Vector3 bikePos = track.Position(Distance);
            transform.position = bikePos;
            transform.rotation = track.Rotation(Distance);
            transform.Rotate(Vector3.forward, Angle, Space.Self);
            transform.Translate(-Vector3.up * track.Radius, Space.Self);
        }

        private void UpdateBestTime()
        {
            int currLap = (int) (Distance / track.Length()) + 1;
            if (currLap <= lap)
                return;

            float lapDuration = Time.time - lapStartTime;
            lapStartTime = Time.time;

            lapDurations.Add(lapDuration);
            lap++;

            if (lapDuration > Statistics.BestTime)
                Statistics.BestTime = lapDuration;
        }
    }
}