using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Vr
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
        private Transform thisTransform;
        private WrapperInputActions inputActions;

        private Vector3 inputPosition;
        public Vector3 InputPosition => inputPosition;

        private Quaternion inputRotation;
        public Quaternion InputRotation => inputRotation;

        private float inputSelect;

        private Vector3 lastFramePosition;
        private bool canUpdatePositionAndRotation;

        private ControlDevice pickedDevice;

        public void OnPosition(InputAction.CallbackContext context)
        {
            if (lastFramePosition == Vector3.zero)
                lastFramePosition = inputPosition;

            lastFramePosition = inputPosition;
            inputPosition = context.ReadValue<Vector3>();
        }

        public void OnRotation(InputAction.CallbackContext context)
        {
            inputRotation = context.ReadValue<Quaternion>();
        }

        public void OnSelect(InputAction.CallbackContext context)
        {
            inputSelect = context.ReadValue<float>();

            if (pickedDevice && inputSelect == 0)
            {
                ReleaseDevice();
            }
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

        public Vector3 DeltaS()
        {
            return inputPosition - lastFramePosition;
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

            thisTransform.localPosition = inputPosition;
            thisTransform.localRotation = inputRotation;
        }
    }
}