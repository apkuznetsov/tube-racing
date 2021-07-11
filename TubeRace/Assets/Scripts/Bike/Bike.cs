using System;
using System.Collections.Generic;
using UnityEngine;

namespace TubeRace
{
    [Serializable]
    public class BikeParametersInitial
    {
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
    }

    public class Bike : MonoBehaviour
    {
        private const string Tag = "Bike";
        public static GameObject[] BikesAsGameObjects;

        #region Сериализуемые поля

        [SerializeField] private Track track;
        public Track Track => track;

        [SerializeField] private AudioSource collisionSfx;
        [SerializeField] private AnimationCurve collisionVolumeCurve;

        [SerializeField] private bool isPlayerBike;
        public bool IsPlayerBike => isPlayerBike;

        [SerializeField] private BikeParametersInitial initial;

        #endregion

        #region Управление

        public bool IsMovementControlsActive { get; set; }

        private float forwardThrustAxis;

        public void SetForwardThrustAxis(float val)
        {
            forwardThrustAxis = val;
        }

        private float horizontalThrustAxis;

        public void SetHorizontalThrustAxis(float val)
        {
            horizontalThrustAxis = val;
        }

        #endregion

        #region Топливо

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
            return true;
        }

        #endregion

        #region Скорости

        public float Velocity { get; private set; }

        public float NormalizedVelocity()
        {
            return Mathf.Clamp01(Velocity / initial.maxVelocity);
        }

        public void Slowdown(int percent)
        {
            Velocity -= Velocity * percent / 100.0f;
        }

        public float Angle { get; private set; }
        private float angularVelocity;

        #endregion

        #region Форсаж

        public bool EnableAfterburner { get; set; }
        private float afterburnerHeat;

        public float NormalizedHeat
        {
            get
            {
                if (initial.afterburnerMaxHeat > 0)
                    return afterburnerHeat / initial.afterburnerMaxHeat;

                return 0.0f;
            }
        }

        private void HeatAfterburner()
        {
            afterburnerHeat += Velocity;
        }

        public void CoolAfterburner()
        {
            afterburnerHeat = 0;
        }

        #endregion

        #region Статистика

        public class BikeStatistics
        {
            public float BestSeconds;
            public float BestVelocity;
            public int RacePlace;
            public float TotalSeconds;
        }

        public BikeStatistics Stats { get; private set; }

        private float raceStartTime;

        private int lapNum;
        private List<float> lapDurations;
        private float lapStartTime;

        public float Distance { get; private set; }
        public float PrevDistance { get; private set; }

        #endregion

        #region Гонка

        public void OnRaceStart()
        {
            Stats.RacePlace = 0;
            Stats.BestSeconds = 0;
            Stats.TotalSeconds = 0;

            raceStartTime = Time.time;
            lapStartTime = raceStartTime;
        }

        public void OnRaceEnd()
        {
            Stats.TotalSeconds = Time.time - raceStartTime;
        }

        #endregion

        #region Обновления

        private void UpdateHeat()
        {
            afterburnerHeat -= initial.afterburnerCoolSpeed * Time.deltaTime;

            if (afterburnerHeat < 0)
                afterburnerHeat = 0;

            if (afterburnerHeat > initial.afterburnerMaxHeat)
                Slowdown(100);
        }

        private void UpdateVelocity()
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
            if (Stats.BestVelocity < Mathf.Abs(Velocity))
                Stats.BestVelocity = Mathf.Abs(Velocity);

            float ds = Velocity * dt;
            if (Physics.Raycast(transform.position, transform.forward, ds))
            {
                HeatAfterburner();

                collisionSfx.volume = collisionVolumeCurve.Evaluate(NormalizedVelocity());
                collisionSfx.Play();

                Velocity = -Velocity * initial.bounceFactor;
                ds = Velocity * dt;
            }

            PrevDistance = Distance;
            Distance += ds;
        }

        private void UpdateAngle()
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

        private void UpdatePhysics()
        {
            UpdateVelocity();
            UpdateAngle();

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
            if (currLap <= lapNum)
                return;

            float lapDuration = Time.time - lapStartTime;
            lapStartTime = Time.time;

            lapDurations.Add(lapDuration);
            lapNum++;

            if (lapDuration > Stats.BestSeconds)
                Stats.BestSeconds = lapDuration;
        }

        #endregion

        #region Юнити

        private void Awake()
        {
            Stats = new BikeStatistics();
            lapDurations = new List<float>();
        }

        private void Start()
        {
            BikesAsGameObjects = GameObject.FindGameObjectsWithTag(Tag);
        }

        private void Update()
        {
            UpdateHeat();
            UpdatePhysics();
            UpdateBestTime();
        }

        #endregion
    }
}