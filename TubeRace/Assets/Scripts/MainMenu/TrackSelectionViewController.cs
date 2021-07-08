using UnityEngine;

namespace TubeRace
{
    public class TrackSelectionViewController : MonoBehaviour
    {
        [SerializeField] private MainMenuViewController mainMenuViewController;

        public void OnButtonExit()
        {
            gameObject.SetActive(false);
            mainMenuViewController.gameObject.SetActive(true);
        }

        private void Awake()
        {
            gameObject.SetActive(false);
        }
    }
}