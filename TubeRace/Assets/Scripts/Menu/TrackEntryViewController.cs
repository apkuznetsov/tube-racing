using UnityEngine;
using UnityEngine.UI;

namespace TubeRace
{
    public class TrackEntryViewController : MonoBehaviour
    {
        [SerializeField] private TrackDescription trackDescription;
        [SerializeField] private Text trackName;

        private TrackDescription activeDescription;
        
        private void Start()
        {
            if (trackDescription != null)
                SetViewValues(trackDescription);
        }

        private void SetViewValues(TrackDescription description)
        {
            activeDescription = description;
            trackName = description.TrackName;
        }

        public void OnButtonStartLevel()
        {
            UnityEngine.
                SceneManagement.SceneManager.
                LoadScene(activeDescription.SceneName);
        }
    }
}