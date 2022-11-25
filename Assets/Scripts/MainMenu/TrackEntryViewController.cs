using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TubeRace
{
    public class TrackEntryViewController : MonoBehaviour
    {
        [SerializeField] private TrackDescription trackDescription;
        private TrackDescription activeDescription;

        [SerializeField] private Text labelTitle;
        [SerializeField] private Text labelLength;
        [SerializeField] private GameObject labelImage;

        private void SetViewFields(TrackDescription description)
        {
            activeDescription = description;

            labelTitle.text = description.Title;
            labelLength.text = description.Length.ToString(CultureInfo.InvariantCulture);
            labelImage.GetComponent<Image>().sprite = description.Preview;
        }

        public void OnButtonStartLevel()
        {
            SceneManager.LoadScene(activeDescription.SceneName);
        }

        private void Start()
        {
            if (trackDescription != null)
                SetViewFields(trackDescription);
        }
    }
}