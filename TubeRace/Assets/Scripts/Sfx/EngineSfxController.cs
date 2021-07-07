using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TubeRace
{
    public class EngineSfxController : MonoBehaviour
    {
        [SerializeField] private Bike bike;
        [SerializeField] private AudioSource engineSource;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float pitchVelocityModifier;
        
        private void UpdateEngineSoundSimple()
        {
            engineSource.pitch = 1.0f + pitchVelocityModifier * bike.NormalizedVelocity();
        }

        private void Update()
        {
            UpdateEngineSoundSimple();
        }
    }
}


