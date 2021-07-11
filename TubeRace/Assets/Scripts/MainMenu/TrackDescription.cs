using UnityEngine;

namespace TubeRace
{
    [CreateAssetMenu]
    public class TrackDescription : ScriptableObject
    {
        [SerializeField] private string title;

        [SerializeField] private string sceneName;

        [SerializeField] private Sprite preview;

        [SerializeField] private float length;
        public string Title => title;
        public string SceneName => sceneName;
        public Sprite Preview => preview;
        public float Length => length;

        public void SetLength(float newLength)
        {
            length = newLength;
        }
    }
}