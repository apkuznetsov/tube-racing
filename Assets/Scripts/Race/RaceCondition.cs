using UnityEngine;

namespace TubeRace
{
    public abstract class RaceCondition : MonoBehaviour
    {
        public bool IsTriggered { get; protected set; }

        public virtual void OnRaceStart()
        {
        }

        public virtual void OnRaceEnd()
        {
        }
    }
}