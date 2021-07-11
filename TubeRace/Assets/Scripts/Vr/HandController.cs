using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TubeRace
{
    public enum HandType
    {
        Default = 0,
        Right = 1,
        Left = 2
    }

    public class HandController : MonoBehaviour,
        WrapperInputActions.IXRIRightHandActions,
        WrapperInputActions.IXRILeftHandActions
    {
        [SerializeField] private HandType handType;
        private bool canUpdatePositionAndRotation;
        private WrapperInputActions inputActions;

        private float inputSelect;

        private Vector3 lastFramePosition;

        private ControlDevice pickedDevice;

        private Transform thisTransform;

        private Vector3 InputPosition { get; set; }
        private Quaternion InputRotation { get; set; }

        private void Awake()
        {
            thisTransform = transform;
            canUpdatePositionAndRotation = true;
        }

        private void Start()
        {
            inputActions = new WrapperInputActions();

            switch (handType)
            {
                case HandType.Default:
                    throw new Exception("Choose hand type");
                case HandType.Right:
                    inputActions.XRIRightHand.SetCallbacks(this);
                    break;
                case HandType.Left:
                    inputActions.XRILeftHand.SetCallbacks(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            inputActions.Enable();
        }

        private void Update()
        {
            if (!canUpdatePositionAndRotation)
                return;

            thisTransform.localPosition = InputPosition;
            thisTransform.localRotation = InputRotation;
        }

        private void OnTriggerStay(Collider other)
        {
            if (pickedDevice != null)
                return;

            if (other.gameObject.CompareTag("ControlDevice") && inputSelect > 0)
            {
                ControlDevice controlDevice = other.GetComponentInParent<ControlDevice>();
                PickupDevice(controlDevice);
            }
        }

        public void OnPosition(InputAction.CallbackContext context)
        {
            if (lastFramePosition == Vector3.zero)
                lastFramePosition = InputPosition;

            lastFramePosition = InputPosition;
            InputPosition = context.ReadValue<Vector3>();
        }

        public void OnRotation(InputAction.CallbackContext context)
        {
            InputRotation = context.ReadValue<Quaternion>();
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            inputSelect = context.ReadValue<float>();

            if (pickedDevice && inputSelect == 0)
                ReleaseDevice();
        }

        public Vector3 DeltaS()
        {
            return InputPosition - lastFramePosition;
        }

        private void PickupDevice(ControlDevice device)
        {
            canUpdatePositionAndRotation = false;
            pickedDevice = device;
            device.StartMovement(this);
        }

        private void ReleaseDevice()
        {
            canUpdatePositionAndRotation = true;
            pickedDevice.StopMovement();
            pickedDevice = null;
        }
    }
}