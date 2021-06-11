using UnityEngine;

namespace TubeRace
{
    public class PowerupFuel : Powerup
    {
        [Range(0.0f, 100.0f)]
        [SerializeField] private float fuelAmount;

        protected override void OnPicked(Bike bike)
        {
            bike.AddFuel(fuelAmount);
        }
    }
}