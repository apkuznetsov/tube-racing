using UnityEngine;

namespace TubeRace
{
    public abstract class ControlDevice : MonoBehaviour
    {
        private HandController handController;

        public void StartMovement(HandController hand)
        {
            handController = hand;
        }

        public void StopMovement()
        {
            handController = null;
        }

        protected abstract void Movement(HandController hand);

        private void Update()
        {
            if (handController == null)
                return;

            Movement(handController);
        }
    }
}