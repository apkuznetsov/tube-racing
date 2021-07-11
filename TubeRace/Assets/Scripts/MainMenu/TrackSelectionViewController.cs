using UnityEngine;

namespace TubeRace
{
    public class TrackSelectionViewController : MonoBehaviour
    {
        [SerializeField] private MainMenuViewController mainMenuViewController;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void OnButtonExit()
        {
            gameObject.SetActive(false);
            mainMenuViewController.gameObject.SetActive(true);
        }
    }
}