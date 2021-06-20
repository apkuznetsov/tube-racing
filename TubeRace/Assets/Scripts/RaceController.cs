using UnityEngine;
using UnityEngine.Events;

namespace TubeRace
{
    public class RaceController : MonoBehaviour
    {
        [SerializeField] private int maxLaps;

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

        private bool IsRaceActive { get; set; }

        private void StartRace()
        {
            IsRaceActive = true;

            CountTimer = countdownTimer;
        }

        private void EndRace()
        {
            IsRaceActive = false;
        }

        private void Start()
        {
            StartRace();
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
        }
    }
}