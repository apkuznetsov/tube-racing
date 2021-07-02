using Gaze;
using UnityEngine;

namespace TubeRace
{
    public class GazeInput : Input
    {
        [SerializeField] private NavigationPanel navigationPanel;

        public override Vector3 MoveDirection()
        {
            return navigationPanel.MoveDirection();
        }

        public override bool EnableAfterburner()
        {
            return false;
        }
    }
}