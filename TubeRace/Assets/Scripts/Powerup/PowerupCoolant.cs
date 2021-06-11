using UnityEngine;

namespace TubeRace
{
    public class PowerupCoolant : Powerup
    {
        protected override void OnPicked(Bike bike)
        {
            bike.CoolAfterburner();
            Debug.Log("PowerupCoolant picked up by " + bike.name);
        }
    }
}