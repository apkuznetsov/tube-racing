using System;
using UnityEngine;

namespace TubeRace
{
    public enum BotBehaviour
    {
        Nothing,
        Behave
    }

    public enum TimerType
    {
        Nothing,

        Delay,
        MoveOn,
        Rotate,

        MaxValues
    }

    [RequireComponent(typeof(Bike))]
    public class BotController : MonoBehaviour
    {
        [SerializeField] private bool isEnabled;
        [SerializeField] private BotBehaviour behaviour;

        [Range(0.0f, 10.0f)] [SerializeField] private float delayTime;
        [Range(0.0f, 10.0f)] [SerializeField] private float moveForwardTime;

        private Bike bike;
        private Transform bikeTransform;

        private float[] timers;

        private void InitTimers()
        {
            timers = new float[(int) TimerType.MaxValues];

            SetTimer(TimerType.Delay, delayTime);
        }

        private void SetTimer(TimerType e, float time)
        {
            timers[(int) e] = time;
        }

        private bool IsTimerFinished(TimerType e)
        {
            return timers[(int) e] <= 0;
        }

        private void MoveForward()
        {
            bike.SetForwardThrustAxis(1);
        }

        private void StallForward()
        {
            bike.SetForwardThrustAxis(0);
        }

        private void MoveForwardOrStall()
        {
            if (!IsTimerFinished(TimerType.MoveOn))
            {
                MoveForward();
            }
            else
            {
                StallForward();
                SetTimer(TimerType.Delay, delayTime);
            }
        }

        private void MoveHorizontal()
        {
            bike.SetHorizontalThrustAxis(1);
        }

        private void StallHorizontal()
        {
            bike.SetHorizontalThrustAxis(0);
        }

        private void Move()
        {
            float dt = Time.deltaTime;
            float ds = bike.Velocity * dt;
            bool isCollision = Physics.Raycast(bikeTransform.position, bikeTransform.forward, ds);

            if (!isCollision)
            {
                StallHorizontal();
                MoveForwardOrStall();
            }
            else
            {
                MoveHorizontal();
            }
        }

        private void Behave()
        {
            if (!bike.IsMovementControlsActive)
                return;

            if (IsTimerFinished(TimerType.Delay))
                Move();
            else
                SetTimer(TimerType.MoveOn, moveForwardTime);
        }

        private void UpdateBot()
        {
            if (!isEnabled)
                return;

            switch (behaviour)
            {
                case BotBehaviour.Nothing:
                    break;

                case BotBehaviour.Behave:
                    Behave();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateTimers()
        {
            for (int i = 0; i < timers.Length; i++)
            {
                if (timers[i] > 0)
                    timers[i] -= Time.deltaTime;
            }
        }

        private void Start()
        {
            bike = GetComponent<Bike>();
            bikeTransform = bike.transform;

            InitTimers();
        }

        private void Update()
        {
            UpdateBot();
            UpdateTimers();
        }
    }
}