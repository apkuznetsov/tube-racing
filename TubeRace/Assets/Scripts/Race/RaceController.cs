using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TubeRace
{
    public enum RaceMode
    {
        Laps,
        Time,
        LastStanding
    }

    public class RaceController : MonoBehaviour
    {
        [SerializeField] private RaceResultsViewController raceResultsViewController;
        [SerializeField] private Track track;

        [SerializeField] private Bike[] bikes;

        [SerializeField] private RaceMode raceMode;
        [SerializeField] private RaceCondition[] conditions;

        [SerializeField] private UnityEvent eventRaceStart;
        [SerializeField] private UnityEvent eventRaceFinished;

        [SerializeField] private int maxLaps;

        [SerializeField] private int countdownTimer;
        private List<Bike> activeBikes;
        private List<Bike> finishedBikes;
        public IEnumerable<Bike> Bikes => bikes;
        public bool IsRaceActive { get; private set; }
        public int MaxLaps => maxLaps;
        public float CountTimer { get; private set; }


        private void Start()
        {
            StartRace();
        }

        private void Update()
        {
            if (!IsRaceActive)
                return;

            UpdatePositions();
            UpdatePrestart();
            UpdateConditions();
        }

        private void StartRace()
        {
            activeBikes = new List<Bike>(bikes);
            finishedBikes = new List<Bike>();

            IsRaceActive = true;
            CountTimer = countdownTimer;

            foreach (RaceCondition condition in conditions)
                condition.OnRaceStart();

            foreach (Bike bike in bikes)
                bike.OnRaceStart();

            eventRaceStart?.Invoke();
        }

        private void EndRace()
        {
            IsRaceActive = false;

            foreach (RaceCondition condition in conditions)
                condition.OnRaceEnd();

            eventRaceFinished?.Invoke();
        }

        private void UpdatePositions()
        {
            foreach (Bike bike in activeBikes)
            {
                if (finishedBikes.Contains(bike))
                    continue;

                float currDistance = bike.Distance;
                float totalRaceDistance = maxLaps * track.Length();

                if (currDistance > totalRaceDistance)
                {
                    finishedBikes.Add(bike);
                    bike.Stats.RacePlace = finishedBikes.Count;
                    bike.OnRaceEnd();

                    if (bike.IsPlayerBike)
                        raceResultsViewController.Show(bike.Stats);
                }
            }
        }

        private void UpdatePrestart()
        {
            if (CountTimer > 0)
            {
                CountTimer -= Time.deltaTime;

                if (CountTimer <= 0)
                    foreach (Bike bike in bikes)
                        bike.IsMovementControlsActive = true;
            }
        }

        private void UpdateConditions()
        {
            if (IsRaceActive)
                return;

            foreach (RaceCondition condition in conditions)
                if (!condition.IsTriggered)
                    return;

            EndRace();
        }
    }
}