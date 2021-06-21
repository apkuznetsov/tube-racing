using UnityEngine;

namespace TubeRace
{
    public class RaceConditionLaps : RaceCondition
    {
        [SerializeField] private RaceController raceController;

        private void Update()
        {
            if (!raceController.IsRaceActive && IsTriggered)
                return;

            var bikes = raceController.Bikes;
            foreach (Bike bike in bikes)
            {
                int laps = (int) (bike.Distance / bike.Track.Length()) + 1;
                if (laps < raceController.MaxLaps)
                    return;
            }

            IsTriggered = true;
        }
    }
}