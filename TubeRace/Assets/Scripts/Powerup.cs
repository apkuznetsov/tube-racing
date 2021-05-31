using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TubeRace
{
    public abstract class Powerup : MonoBehaviour
    {
        public abstract void OnPickedByPlayer();
    }
}
