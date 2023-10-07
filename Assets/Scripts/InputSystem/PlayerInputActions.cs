//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Scripts/InputSystem/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Normal Mode"",
            ""id"": ""5798866c-2add-4708-b722-0674c6253653"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""a507704f-6c9a-4cdb-9023-80c65f858154"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""0c5d8686-a7e7-4deb-92bb-88bb50b013eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""3763b56f-8f5d-4509-8e12-eb4de4ba1f66"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom In"",
                    ""type"": ""Value"",
                    ""id"": ""d37c79ce-4145-4870-83f5-090b84b3ebb2"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Zoom Out"",
                    ""type"": ""Value"",
                    ""id"": ""83964de8-577d-42a5-9731-05758fa59c50"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7f9deddd-8baa-4af0-a81c-fd1a91ee939c"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""81fea8d9-47ee-4b05-870f-1b20d6adf382"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8cc6baa-9dd0-4d0b-bdbe-45073d1bce3e"",
                    ""path"": ""<Mouse>/scroll/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Zoom In"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9887a4dc-7e90-4d42-aa6a-cb6959ec1a50"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Zoom In"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b09ce28b-c619-4b09-b578-1a05726c6217"",
                    ""path"": ""<Mouse>/scroll/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Zoom Out"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e45eb72-594a-45a9-85f4-de361ba31b61"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Zoom Out"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f7841be5-bb9a-4248-97b5-de9aa119f167"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ee7e724d-56fa-4e7b-93d7-02f82a69f862"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""aa88f5e0-5e83-4e3c-9797-fa98f0531229"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5ba3b41d-f5af-44fc-8524-c713d02dc070"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""59943b5e-e8c4-41e2-9e6c-b119c6744062"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""aff4284a-fdb2-4fa8-abf3-c38ddf9e663e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3edf1398-3cb6-452a-a0a2-67151c933f49"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd26f4be-75dd-43bc-bd82-255bc23f3d02"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Precision Mode"",
            ""id"": ""5ac1c665-4667-4719-831d-e285d82fc570"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""77fe6afb-0f42-4fc7-a0d5-ab40cd0a867b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""487d8e5e-9a16-4305-a9c6-150383c58c29"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Normal Mode
        m_NormalMode = asset.FindActionMap("Normal Mode", throwIfNotFound: true);
        m_NormalMode_Movement = m_NormalMode.FindAction("Movement", throwIfNotFound: true);
        m_NormalMode_Use = m_NormalMode.FindAction("Use", throwIfNotFound: true);
        m_NormalMode_Throw = m_NormalMode.FindAction("Throw", throwIfNotFound: true);
        m_NormalMode_ZoomIn = m_NormalMode.FindAction("Zoom In", throwIfNotFound: true);
        m_NormalMode_ZoomOut = m_NormalMode.FindAction("Zoom Out", throwIfNotFound: true);
        // Precision Mode
        m_PrecisionMode = asset.FindActionMap("Precision Mode", throwIfNotFound: true);
        m_PrecisionMode_Newaction = m_PrecisionMode.FindAction("New action", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Normal Mode
    private readonly InputActionMap m_NormalMode;
    private List<INormalModeActions> m_NormalModeActionsCallbackInterfaces = new List<INormalModeActions>();
    private readonly InputAction m_NormalMode_Movement;
    private readonly InputAction m_NormalMode_Use;
    private readonly InputAction m_NormalMode_Throw;
    private readonly InputAction m_NormalMode_ZoomIn;
    private readonly InputAction m_NormalMode_ZoomOut;
    public struct NormalModeActions
    {
        private @PlayerInputActions m_Wrapper;
        public NormalModeActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_NormalMode_Movement;
        public InputAction @Use => m_Wrapper.m_NormalMode_Use;
        public InputAction @Throw => m_Wrapper.m_NormalMode_Throw;
        public InputAction @ZoomIn => m_Wrapper.m_NormalMode_ZoomIn;
        public InputAction @ZoomOut => m_Wrapper.m_NormalMode_ZoomOut;
        public InputActionMap Get() { return m_Wrapper.m_NormalMode; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NormalModeActions set) { return set.Get(); }
        public void AddCallbacks(INormalModeActions instance)
        {
            if (instance == null || m_Wrapper.m_NormalModeActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_NormalModeActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Use.started += instance.OnUse;
            @Use.performed += instance.OnUse;
            @Use.canceled += instance.OnUse;
            @Throw.started += instance.OnThrow;
            @Throw.performed += instance.OnThrow;
            @Throw.canceled += instance.OnThrow;
            @ZoomIn.started += instance.OnZoomIn;
            @ZoomIn.performed += instance.OnZoomIn;
            @ZoomIn.canceled += instance.OnZoomIn;
            @ZoomOut.started += instance.OnZoomOut;
            @ZoomOut.performed += instance.OnZoomOut;
            @ZoomOut.canceled += instance.OnZoomOut;
        }

        private void UnregisterCallbacks(INormalModeActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Use.started -= instance.OnUse;
            @Use.performed -= instance.OnUse;
            @Use.canceled -= instance.OnUse;
            @Throw.started -= instance.OnThrow;
            @Throw.performed -= instance.OnThrow;
            @Throw.canceled -= instance.OnThrow;
            @ZoomIn.started -= instance.OnZoomIn;
            @ZoomIn.performed -= instance.OnZoomIn;
            @ZoomIn.canceled -= instance.OnZoomIn;
            @ZoomOut.started -= instance.OnZoomOut;
            @ZoomOut.performed -= instance.OnZoomOut;
            @ZoomOut.canceled -= instance.OnZoomOut;
        }

        public void RemoveCallbacks(INormalModeActions instance)
        {
            if (m_Wrapper.m_NormalModeActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(INormalModeActions instance)
        {
            foreach (var item in m_Wrapper.m_NormalModeActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_NormalModeActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public NormalModeActions @NormalMode => new NormalModeActions(this);

    // Precision Mode
    private readonly InputActionMap m_PrecisionMode;
    private List<IPrecisionModeActions> m_PrecisionModeActionsCallbackInterfaces = new List<IPrecisionModeActions>();
    private readonly InputAction m_PrecisionMode_Newaction;
    public struct PrecisionModeActions
    {
        private @PlayerInputActions m_Wrapper;
        public PrecisionModeActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_PrecisionMode_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_PrecisionMode; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PrecisionModeActions set) { return set.Get(); }
        public void AddCallbacks(IPrecisionModeActions instance)
        {
            if (instance == null || m_Wrapper.m_PrecisionModeActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PrecisionModeActionsCallbackInterfaces.Add(instance);
            @Newaction.started += instance.OnNewaction;
            @Newaction.performed += instance.OnNewaction;
            @Newaction.canceled += instance.OnNewaction;
        }

        private void UnregisterCallbacks(IPrecisionModeActions instance)
        {
            @Newaction.started -= instance.OnNewaction;
            @Newaction.performed -= instance.OnNewaction;
            @Newaction.canceled -= instance.OnNewaction;
        }

        public void RemoveCallbacks(IPrecisionModeActions instance)
        {
            if (m_Wrapper.m_PrecisionModeActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPrecisionModeActions instance)
        {
            foreach (var item in m_Wrapper.m_PrecisionModeActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PrecisionModeActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PrecisionModeActions @PrecisionMode => new PrecisionModeActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface INormalModeActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnUse(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnZoomIn(InputAction.CallbackContext context);
        void OnZoomOut(InputAction.CallbackContext context);
    }
    public interface IPrecisionModeActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}
