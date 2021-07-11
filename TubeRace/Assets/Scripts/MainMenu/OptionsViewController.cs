using UnityEngine;
using UnityEngine.UI;

namespace TubeRace
{
    public class OptionsViewController : MonoBehaviour
    {
        [SerializeField] private MainMenuViewController mainMenuViewController;

        [SerializeField] private bool isFullScreen = true;
        [SerializeField] private Dropdown dropdown;
        private readonly int[] height = {1080, 768, 900};

        private readonly int[] width = {1920, 1366, 1440};

        private void Awake()
        {
            gameObject.SetActive(false);

            InitDropdownOptions();
            dropdown.value = 0;

            SetScreenResolution();
        }

        private void InitDropdownOptions()
        {
            for (int i = 0; i < height.Length; i++)
            {
                Dropdown.OptionData optionData = new Dropdown.OptionData
                {
                    text = $"{width[i]} x {height[i]}"
                };

                dropdown.options.Add(optionData);
            }
        }

        private void SetScreenResolution()
        {
            int option = dropdown.value;
            Screen.SetResolution(width[option], height[option], isFullScreen);
        }

        public void OnButtonExit()
        {
            SetScreenResolution();

            gameObject.SetActive(false);
            mainMenuViewController.gameObject.SetActive(true);
        }
    }
}