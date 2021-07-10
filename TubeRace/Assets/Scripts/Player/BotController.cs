using UnityEngine;

namespace TubeRace
{
    public enum BotBehaviour
    {
        Nothing,
        Move
    }

    public enum ActionTimerType
    {
        Nothing,

        Delay,
        PressW,
        Rotate,

        MaxValues
    }

    [RequireComponent(typeof(Bike))]
    public class BotController : MonoBehaviour
    {
        [SerializeField] private bool isEnabled;
        [SerializeField] private BotBehaviour behaviour;

        [Range(0.0f, 10.0f)] [SerializeField] private float pressWTime;

        private Bike bike;

        private float[] actionTimers;

        private void InitActionTimers()
        {
            actionTimers = new float[(int) ActionTimerType.MaxValues];

            SetActionTimer(ActionTimerType.PressW, pressWTime);
        }

        private void SetActionTimer(ActionTimerType e, float time)
        {
            actionTimers[(int) e] = time;
        }

        private bool IsActionTimerFinished(ActionTimerType e)
        {
            return actionTimers[(int) e] <= 0;
        }

        private void PressW()
        {
            bike.SetForwardThrustAxis(1);
        }

        private void UnpressW()
        {
            bike.SetForwardThrustAxis(0);
        }

        private void CheckInput()
        {
            if (!bike.IsMovementControlsActive)
                return;

            float dt = Time.deltaTime;
            float ds = bike.Velocity * dt;

            if (!Physics.Raycast(bike.transform.position, bike.transform.forward, ds))
            {
                if (!IsActionTimerFinished(ActionTimerType.PressW))
                {
                    PressW();
                }
                else
                {
                    UnpressW();
                }
            }
        }

        private void UpdateBot()
        {
            if (!isEnabled)
                return;

            switch (behaviour)
            {
                case BotBehaviour.Nothing:
                    break;

                case BotBehaviour.Move:
                    CheckInput();
                    break;
            }
        }

        private void UpdateActionTimers()
        {
            for (int i = 0; i < actionTimers.Length; i++)
            {
                if (actionTimers[i] > 0)
                    actionTimers[i] -= Time.deltaTime;
            }
        }

        private void Start()
        {
            bike = GetComponent<Bike>();
            InitActionTimers();
        }

        private void Update()
        {
            UpdateBot();
            UpdateActionTimers();
        }
    }
}