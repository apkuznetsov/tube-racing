using UnityEngine;
using UnityEngine.UI;

namespace TubeRace
{
    public class TrackEntryViewController : MonoBehaviour
    {
        [SerializeField] private TrackDescription trackDescription;
        [SerializeField] private Text trackName;
        [SerializeField] private GameObject image;

        private TrackDescription activeDescription;
        
        private void Start()
        {
            if (trackDescription != null)
                SetViewValues(trackDescription);
        }

        private void SetViewValues(TrackDescription description)
        {
            activeDescription = description;
            trackName.text = description.TrackName;
            image.GetComponent<Image>().sprite = description.PreviewImage;
        }

        public void OnButtonStartLevel()
        {
            UnityEngine.
                SceneManagement.SceneManager.
                LoadScene(activeDescription.SceneName);
        }
    }
}