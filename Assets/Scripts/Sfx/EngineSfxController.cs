using UnityEngine;

namespace TubeRace
{
    public class EngineSfxController : MonoBehaviour
    {
        [SerializeField] private Bike bike;
        [SerializeField] private AudioSource engineSource;

        [Range(0.0f, 1.0f)] [SerializeField] private float pitchModifier;

        private void UpdateEngineSound()
        {
            engineSource.pitch = 1.0f + pitchModifier * bike.NormalizedVelocity();
        }

        private void Update()
        {
            UpdateEngineSound();
        }
    }
}