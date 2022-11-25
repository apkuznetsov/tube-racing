using UnityEngine;

namespace TubeRace
{
    public class PowerupSlowdown : Powerup
    {
        [Range(0, 100)] [SerializeField] private int slowdownPercent;

        protected override void OnPicked(Bike bike)
        {
            bike.Slowdown(slowdownPercent);
        }
    }
}