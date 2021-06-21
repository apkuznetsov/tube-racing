using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TubeRace
{
    public class RaceController : MonoBehaviour
    {
        [SerializeField] private int maxLaps;
        public int MaxLaps => maxLaps;

        private enum RaceMode
        {
            Laps,
            Time,
            LastStanding
        }

        [SerializeField] private RaceMode raceMode;

        [SerializeField] private UnityEvent eventRaceStart;
        [SerializeField] private UnityEvent eventRaceFinished;

        [SerializeField] private int countdownTimer;
        public int CountdownTimer => countdownTimer;

        public float CountTimer { get; private set; }

        [SerializeField] private Bike[] bikes;
        public IEnumerable<Bike> Bikes => bikes;

        public bool IsRaceActive { get; private set; }

        [SerializeField] private RaceCondition[] conditions;

        private void StartRace()
        {
            IsRaceActive = true;

            CountTimer = countdownTimer;

            foreach (RaceCondition c in conditions)
                c.OnRaceStart();

            eventRaceStart?.Invoke();
        }

        private void EndRace()
        {
            IsRaceActive = false;

            foreach (RaceCondition c in conditions)
                c.OnRaceEnd();
        }

        private void Start()
        {
            StartRace();
        }

        private void UpdateConditions()
        {
            if (!IsRaceActive)
                return;

            foreach (RaceCondition c in conditions)
            {
                if (!c.IsTriggered)
                    return;
            }

            // race ends
            EndRace();
            eventRaceFinished?.Invoke();
        }

        private void UpdateRacePrestart()
        {
            if (CountTimer > 0)
            {
                CountTimer -= Time.deltaTime;

                if (CountTimer <= 0)
                {
                    foreach (Bike bike in bikes)
                        bike.IsMovementControlsActive = true;
                }
            }
        }

        private void Update()
        {
            if (!IsRaceActive)
                return;

            UpdateRacePrestart();
            UpdateConditions();
        }
    }
}