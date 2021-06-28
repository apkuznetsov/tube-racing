using UnityEngine;

namespace TubeRace
{
    [CreateAssetMenu]
    public class TrackDescription : ScriptableObject
    {
        [SerializeField] private string trackName;
        public string TrackName => trackName;

        [SerializeField] private string sceneName;
        public string SceneName => sceneName;

        [SerializeField] private Sprite previewImage;
        public Sprite PreviewImage => previewImage;
    }
}