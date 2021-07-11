using UnityEngine;

namespace TubeRace
{
    public class ComplexEngineSfxController : MonoBehaviour
    {
        private const float PitchFactor = 2f;

        [SerializeField] private Bike bike;

        [SerializeField] private AudioSource sfxLow;
        [SerializeField] private AudioSource sfxHigh;
        [SerializeField] private AudioSource sfxLoud;
        [SerializeField] private AudioSource sfxSonicBoom;

        [SerializeField] private AnimationCurve curveLow;
        [SerializeField] private AnimationCurve curveHigh;
        [SerializeField] private AnimationCurve curveLoud;

        [SerializeField] private float superSonicSpeed;
        [SerializeField] private AnimationCurve sonicCurve;
        private bool IsSuperSonic { get; set; }

        private void Update()
        {
            // UpdateSuperSonicSound();
            UpdateEngineSound();
        }

        private void SetSuperSonic(bool enable)
        {
            // if (!IsSuperSonic && enable)
            //     sfxSonicBoom.Play();

            IsSuperSonic = enable;
        }

        private void UpdateSuperSonicSound()
        {
            SetSuperSonic(Mathf.Abs(bike.Velocity) > superSonicSpeed);

            if (sfxSonicBoom.isPlaying)
            {
                float t = Mathf.Clamp01(sfxSonicBoom.time / sfxSonicBoom.clip.length);
                sfxSonicBoom.volume = sonicCurve.Evaluate(t);
            }
        }

        private void UpdateEngineSound()
        {
            if (IsSuperSonic)
            {
                sfxLow.volume = 0;
                sfxHigh.volume = 0;
                sfxLoud.volume = 0;

                return;
            }

            float t = Mathf.Clamp01(bike.Velocity / superSonicSpeed);

            sfxLow.volume = curveLow.Evaluate(t);
            sfxLow.pitch = 1.0f + PitchFactor * t;

            sfxHigh.volume = curveHigh.Evaluate(t);
            sfxHigh.pitch = 1.0f + PitchFactor * t;

            sfxLoud.volume = curveLoud.Evaluate(t);
        }
    }
}