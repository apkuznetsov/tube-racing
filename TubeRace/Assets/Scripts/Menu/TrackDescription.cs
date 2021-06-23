using UnityEngine;
using UnityEngine.UI;

namespace TubeRace
{
    [CreateAssetMenu]
    public class TrackDescription : ScriptableObject
    {
        [SerializeField] private Text trackName;
        public Text TrackName => trackName;

        [SerializeField] private string sceneName;
        public string SceneName => sceneName;

        [SerializeField] private Sprite previewImage;
        public Sprite PreviewImage => previewImage;
    }
}