using UnityEngine;

namespace TubeRace
{
    [CreateAssetMenu]
    public class TrackDescription : ScriptableObject
    {
        [SerializeField] private string title;
        public string Title => title;

        [SerializeField] private string sceneName;
        public string SceneName => sceneName;

        [SerializeField] private Sprite preview;
        public Sprite Preview => preview;

        public float Length { get; private set; }

        public void SetLength(float length)
        {
            Length = length;
        }
    }
}