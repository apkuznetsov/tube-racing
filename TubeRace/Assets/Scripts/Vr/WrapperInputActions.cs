// GENERATED AUTOMATICALLY FROM 'Assets/Samples/XR Interaction Toolkit/1.0.0-pre.4/Default Input Actions/XRI Default Input Actions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Vr
{
    public class @WrapperInputActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @WrapperInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""XRI Default Input Actions"",
    ""maps"": [
        {
            ""name"": ""XRI HMD"",
            ""id"": ""09ff3ccc-21b4-4346-a3a2-7c978b5af892"",
            ""actions"": [
                {
                    ""name"": ""Position"",
                    ""type"": ""Value"",
                    ""id"": ""1a9029f8-7a46-46b9-9eff-e9ae8365f611"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""aed87fe6-2b01-4dd2-a8fa-195578fd8158"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cff1f981-6e1f-4e2c-a90c-715a0ea2e80e"",
                    ""path"": ""<XRHMD>/centerEyePosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2017383-a3f6-4c46-acb1-012b8eece9cc"",
                    ""path"": ""<XRHMD>/centerEyeRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""XRI LeftHand"",
            ""id"": ""5fe596f9-1b7b-49b7-80a7-3b5195caf74d"",
            ""actions"": [
                {
                    ""name"": ""Position"",
                    ""type"": ""Value"",
                    ""id"": ""83a7af0b-87e3-42c3-a909-95fbf8091e4f"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""cb6b7130-2bac-4ef7-abe4-6991ae7d419d"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""33754c03-48ec-46ef-9bc6-22ed6bfdd8e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Quaternion Fallback"",
                    ""id"": ""61466a56-4ee4-47b1-aa6a-4806de1de5f2"",
                    ""path"": ""QuaternionFallback"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""first"",
                    ""id"": ""afdcfbff-e241-4fdd-a6d1-23b0bf273360"",
                    ""path"": ""<XRController>{LeftHand}/pointerRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""second"",
                    ""id"": ""ed03d944-4c09-4c38-8b68-5c844e18ca7c"",
                    ""path"": ""<XRController>{LeftHand}/deviceRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""third"",
                    ""id"": ""c98fc8c8-7fc6-4909-89b6-c5b7568e7275"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Vector 3 Fallback"",
                    ""id"": ""14aeff85-d719-43ff-a124-b1cd7ca8686d"",
                    ""path"": ""Vector3Fallback"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Position"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""first"",
                    ""id"": ""abf752ec-feee-4d51-b530-f0870f48acc9"",
                    ""path"": ""<XRController>{LeftHand}/pointerPosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""second"",
                    ""id"": ""6580b669-0651-401c-9779-85ef22689130"",
                    ""path"": ""<XRController>{LeftHand}/devicePosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""third"",
                    ""id"": ""ae101942-9eaa-4c53-a388-cafc3fd89bdf"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""71a4d23f-3e9a-4513-923b-ba388c5e84bf"",
                    ""path"": ""<XRController>{LeftHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""XRI RightHand"",
            ""id"": ""7960f8ef-2bf3-4281-aecc-4c03809d6c8c"",
            ""actions"": [
                {
                    ""name"": ""Position"",
                    ""type"": ""Value"",
                    ""id"": ""c4990d70-7b8a-4ce1-b03c-da86716b8352"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""ee6bf5bf-bb0a-4a50-8327-cb654b19e298"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""ac96c10b-c955-4a46-8e67-bf16bc069b53"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Quaternion Fallback"",
                    ""id"": ""84e51e1c-1b95-4f3e-a61f-29da6c1f0816"",
                    ""path"": ""QuaternionFallback"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""first"",
                    ""id"": ""3722d501-eb80-4f61-9361-08a5ea7a1394"",
                    ""path"": ""<XRController>{RightHand}/pointerRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""second"",
                    ""id"": ""2e6ad191-d5aa-4919-aac6-295c83387a72"",
                    ""path"": ""<XRController>{RightHand}/deviceRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""third"",
                    ""id"": ""b9ecb60d-341e-47cf-b50a-41d5815af8b0"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Vector 3 Fallback"",
                    ""id"": ""74e968f1-ad08-4a82-a68d-764517faecef"",
                    ""path"": ""Vector3Fallback"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Position"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""first"",
                    ""id"": ""9717e367-64a4-440a-9974-1e641d753eb2"",
                    ""path"": ""<XRController>{RightHand}/pointerPosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""second"",
                    ""id"": ""0794a41d-29ef-48ec-a452-6b7de29b52fa"",
                    ""path"": ""<XRController>{RightHand}/devicePosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""third"",
                    ""id"": ""3ef0a781-60c5-48bc-a584-f95553f8ae0a"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1ce80054-410d-4112-a332-50faa7fb4f23"",
                    ""path"": ""<XRController>{RightHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Generic XR Controller"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Generic XR Controller"",
            ""bindingGroup"": ""Generic XR Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>{LeftHand}"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<XRController>{RightHand}"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<WMRHMD>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Continuous Move"",
            ""bindingGroup"": ""Continuous Move"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>{LeftHand}"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<XRController>{RightHand}"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Noncontinuous Move"",
            ""bindingGroup"": ""Noncontinuous Move"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>{LeftHand}"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<XRController>{RightHand}"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // XRI HMD
            m_XRIHMD = asset.FindActionMap("XRI HMD", throwIfNotFound: true);
            m_XRIHMD_Position = m_XRIHMD.FindAction("Position", throwIfNotFound: true);
            m_XRIHMD_Rotation = m_XRIHMD.FindAction("Rotation", throwIfNotFound: true);
            // XRI LeftHand
            m_XRILeftHand = asset.FindActionMap("XRI LeftHand", throwIfNotFound: true);
            m_XRILeftHand_Position = m_XRILeftHand.FindAction("Position", throwIfNotFound: true);
            m_XRILeftHand_Rotation = m_XRILeftHand.FindAction("Rotation", throwIfNotFound: true);
            m_XRILeftHand_Select = m_XRILeftHand.FindAction("Select", throwIfNotFound: true);
            // XRI RightHand
            m_XRIRightHand = asset.FindActionMap("XRI RightHand", throwIfNotFound: true);
            m_XRIRightHand_Position = m_XRIRightHand.FindAction("Position", throwIfNotFound: true);
            m_XRIRightHand_Rotation = m_XRIRightHand.FindAction("Rotation", throwIfNotFound: true);
            m_XRIRightHand_Select = m_XRIRightHand.FindAction("Select", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // XRI HMD
        private readonly InputActionMap m_XRIHMD;
        private IXRIHMDActions m_XRIHMDActionsCallbackInterface;
        private readonly InputAction m_XRIHMD_Position;
        private readonly InputAction m_XRIHMD_Rotation;
        public struct XRIHMDActions
        {
            private @WrapperInputActions m_Wrapper;
            public XRIHMDActions(@WrapperInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Position => m_Wrapper.m_XRIHMD_Position;
            public InputAction @Rotation => m_Wrapper.m_XRIHMD_Rotation;
            public InputActionMap Get() { return m_Wrapper.m_XRIHMD; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(XRIHMDActions set) { return set.Get(); }
            public void SetCallbacks(IXRIHMDActions instance)
            {
                if (m_Wrapper.m_XRIHMDActionsCallbackInterface != null)
                {
                    @Position.started -= m_Wrapper.m_XRIHMDActionsCallbackInterface.OnPosition;
                    @Position.performed -= m_Wrapper.m_XRIHMDActionsCallbackInterface.OnPosition;
                    @Position.canceled -= m_Wrapper.m_XRIHMDActionsCallbackInterface.OnPosition;
                    @Rotation.started -= m_Wrapper.m_XRIHMDActionsCallbackInterface.OnRotation;
                    @Rotation.performed -= m_Wrapper.m_XRIHMDActionsCallbackInterface.OnRotation;
                    @Rotation.canceled -= m_Wrapper.m_XRIHMDActionsCallbackInterface.OnRotation;
                }
                m_Wrapper.m_XRIHMDActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Position.started += instance.OnPosition;
                    @Position.performed += instance.OnPosition;
                    @Position.canceled += instance.OnPosition;
                    @Rotation.started += instance.OnRotation;
                    @Rotation.performed += instance.OnRotation;
                    @Rotation.canceled += instance.OnRotation;
                }
            }
        }
        public XRIHMDActions @XRIHMD => new XRIHMDActions(this);

        // XRI LeftHand
        private readonly InputActionMap m_XRILeftHand;
        private IXRILeftHandActions m_XRILeftHandActionsCallbackInterface;
        private readonly InputAction m_XRILeftHand_Position;
        private readonly InputAction m_XRILeftHand_Rotation;
        private readonly InputAction m_XRILeftHand_Select;
        public struct XRILeftHandActions
        {
            private @WrapperInputActions m_Wrapper;
            public XRILeftHandActions(@WrapperInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Position => m_Wrapper.m_XRILeftHand_Position;
            public InputAction @Rotation => m_Wrapper.m_XRILeftHand_Rotation;
            public InputAction @Select => m_Wrapper.m_XRILeftHand_Select;
            public InputActionMap Get() { return m_Wrapper.m_XRILeftHand; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(XRILeftHandActions set) { return set.Get(); }
            public void SetCallbacks(IXRILeftHandActions instance)
            {
                if (m_Wrapper.m_XRILeftHandActionsCallbackInterface != null)
                {
                    @Position.started -= m_Wrapper.m_XRILeftHandActionsCallbackInterface.OnPosition;
                    @Position.performed -= m_Wrapper.m_XRILeftHandActionsCallbackInterface.OnPosition;
                    @Position.canceled -= m_Wrapper.m_XRILeftHandActionsCallbackInterface.OnPosition;
                    @Rotation.started -= m_Wrapper.m_XRILeftHandActionsCallbackInterface.OnRotation;
                    @Rotation.performed -= m_Wrapper.m_XRILeftHandActionsCallbackInterface.OnRotation;
                    @Rotation.canceled -= m_Wrapper.m_XRILeftHandActionsCallbackInterface.OnRotation;
                    @Select.started -= m_Wrapper.m_XRILeftHandActionsCallbackInterface.OnSelect;
                    @Select.performed -= m_Wrapper.m_XRILeftHandActionsCallbackInterface.OnSelect;
                    @Select.canceled -= m_Wrapper.m_XRILeftHandActionsCallbackInterface.OnSelect;
                }
                m_Wrapper.m_XRILeftHandActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Position.started += instance.OnPosition;
                    @Position.performed += instance.OnPosition;
                    @Position.canceled += instance.OnPosition;
                    @Rotation.started += instance.OnRotation;
                    @Rotation.performed += instance.OnRotation;
                    @Rotation.canceled += instance.OnRotation;
                    @Select.started += instance.OnSelect;
                    @Select.performed += instance.OnSelect;
                    @Select.canceled += instance.OnSelect;
                }
            }
        }
        public XRILeftHandActions @XRILeftHand => new XRILeftHandActions(this);

        // XRI RightHand
        private readonly InputActionMap m_XRIRightHand;
        private IXRIRightHandActions m_XRIRightHandActionsCallbackInterface;
        private readonly InputAction m_XRIRightHand_Position;
        private readonly InputAction m_XRIRightHand_Rotation;
        private readonly InputAction m_XRIRightHand_Select;
        public struct XRIRightHandActions
        {
            private @WrapperInputActions m_Wrapper;
            public XRIRightHandActions(@WrapperInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Position => m_Wrapper.m_XRIRightHand_Position;
            public InputAction @Rotation => m_Wrapper.m_XRIRightHand_Rotation;
            public InputAction @Select => m_Wrapper.m_XRIRightHand_Select;
            public InputActionMap Get() { return m_Wrapper.m_XRIRightHand; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(XRIRightHandActions set) { return set.Get(); }
            public void SetCallbacks(IXRIRightHandActions instance)
            {
                if (m_Wrapper.m_XRIRightHandActionsCallbackInterface != null)
                {
                    @Position.started -= m_Wrapper.m_XRIRightHandActionsCallbackInterface.OnPosition;
                    @Position.performed -= m_Wrapper.m_XRIRightHandActionsCallbackInterface.OnPosition;
                    @Position.canceled -= m_Wrapper.m_XRIRightHandActionsCallbackInterface.OnPosition;
                    @Rotation.started -= m_Wrapper.m_XRIRightHandActionsCallbackInterface.OnRotation;
                    @Rotation.performed -= m_Wrapper.m_XRIRightHandActionsCallbackInterface.OnRotation;
                    @Rotation.canceled -= m_Wrapper.m_XRIRightHandActionsCallbackInterface.OnRotation;
                    @Select.started -= m_Wrapper.m_XRIRightHandActionsCallbackInterface.OnSelect;
                    @Select.performed -= m_Wrapper.m_XRIRightHandActionsCallbackInterface.OnSelect;
                    @Select.canceled -= m_Wrapper.m_XRIRightHandActionsCallbackInterface.OnSelect;
                }
                m_Wrapper.m_XRIRightHandActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Position.started += instance.OnPosition;
                    @Position.performed += instance.OnPosition;
                    @Position.canceled += instance.OnPosition;
                    @Rotation.started += instance.OnRotation;
                    @Rotation.performed += instance.OnRotation;
                    @Rotation.canceled += instance.OnRotation;
                    @Select.started += instance.OnSelect;
                    @Select.performed += instance.OnSelect;
                    @Select.canceled += instance.OnSelect;
                }
            }
        }
        public XRIRightHandActions @XRIRightHand => new XRIRightHandActions(this);
        private int m_GenericXRControllerSchemeIndex = -1;
        public InputControlScheme GenericXRControllerScheme
        {
            get
            {
                if (m_GenericXRControllerSchemeIndex == -1) m_GenericXRControllerSchemeIndex = asset.FindControlSchemeIndex("Generic XR Controller");
                return asset.controlSchemes[m_GenericXRControllerSchemeIndex];
            }
        }
        private int m_ContinuousMoveSchemeIndex = -1;
        public InputControlScheme ContinuousMoveScheme
        {
            get
            {
                if (m_ContinuousMoveSchemeIndex == -1) m_ContinuousMoveSchemeIndex = asset.FindControlSchemeIndex("Continuous Move");
                return asset.controlSchemes[m_ContinuousMoveSchemeIndex];
            }
        }
        private int m_NoncontinuousMoveSchemeIndex = -1;
        public InputControlScheme NoncontinuousMoveScheme
        {
            get
            {
                if (m_NoncontinuousMoveSchemeIndex == -1) m_NoncontinuousMoveSchemeIndex = asset.FindControlSchemeIndex("Noncontinuous Move");
                return asset.controlSchemes[m_NoncontinuousMoveSchemeIndex];
            }
        }
        public interface IXRIHMDActions
        {
            void OnPosition(InputAction.CallbackContext context);
            void OnRotation(InputAction.CallbackContext context);
        }
        public interface IXRILeftHandActions
        {
            void OnPosition(InputAction.CallbackContext context);
            void OnRotation(InputAction.CallbackContext context);
            void OnSelect(InputAction.CallbackContext context);
        }
        public interface IXRIRightHandActions
        {
            void OnPosition(InputAction.CallbackContext context);
            void OnRotation(InputAction.CallbackContext context);
            void OnSelect(InputAction.CallbackContext context);
        }
    }
}
