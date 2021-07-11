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

        [SerializeField] private float length;
        public float Length => length;

        public void SetLength(float newLength)
        {
            length = newLength;
        }
    }
}