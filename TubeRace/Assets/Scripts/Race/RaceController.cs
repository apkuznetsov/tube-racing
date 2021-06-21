using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TubeRace
{
    public class RaceController : MonoBehaviour
    {
        [SerializeField] private Track track;

        [SerializeField] private RaceResultsViewController raceResultsViewController;

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
        private List<Bike> activeBikes;
        private List<Bike> finishedBikes;
        public IEnumerable<Bike> Bikes => bikes;

        public bool IsRaceActive { get; private set; }

        [SerializeField] private RaceCondition[] conditions;

        private void StartRace()
        {
            activeBikes = new List<Bike>(bikes);
            finishedBikes = new List<Bike>();

            IsRaceActive = true;

            CountTimer = countdownTimer;

            foreach (RaceCondition c in conditions)
                c.OnRaceStart();

            foreach (Bike b in bikes)
                b.OnRaceStart();

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
            if (IsRaceActive)
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

        private void UpdateBikeRacePositions()
        {
            if (activeBikes.Count == 0)
            {
                EndRace();
                return;
            }

            foreach (Bike bike in activeBikes)
            {
                if (finishedBikes.Contains(bike))
                    continue;

                float currDistance = bike.Distance;
                float totalRaceDistance = maxLaps * track.Length();

                if (currDistance > totalRaceDistance)
                {
                    finishedBikes.Add(bike);
                    bike.Statistics.Place = finishedBikes.Count;
                    bike.OnRaceEnd();

                    if (bike.IsPlayerBike)
                    {
                        raceResultsViewController.Show(bike.Statistics);
                    }
                }
            }
        }

        private void Update()
        {
            if (!IsRaceActive)
                return;

            UpdateBikeRacePositions();
            UpdateRacePrestart();
            UpdateConditions();
        }
    }
}