//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/InputSystem/InputSystem.inputactions
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

public partial class @InputSystem: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputSystem"",
    ""maps"": [
        {
            ""name"": ""InGameController"",
            ""id"": ""bec99d04-9a05-4890-a51d-59e7fe0a6a46"",
            ""actions"": [
                {
                    ""name"": ""Direction"",
                    ""type"": ""Value"",
                    ""id"": ""7c775ab7-2de0-4687-aa70-dfcdc3baf6d4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Buttons"",
                    ""type"": ""Value"",
                    ""id"": ""c0f885af-bec5-402c-a144-063111a4ae29"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""8bf09c15-daff-405b-9ff3-dcd993519d1e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""74dfa56d-43dc-4384-8b42-c44e0b01c186"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""81c54ccd-bdf0-4f82-a605-618de1175b13"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""21e17831-7ca9-49e5-8cf0-13691eca1a2e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""56f2357a-25f7-4a11-bf15-1c09ed52b270"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""eca82bc8-7725-4bbe-b600-d04a745b6c46"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Buttons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""982af1a3-93a7-48e7-aee7-fc8585cfb8f1"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Buttons"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InGameController
        m_InGameController = asset.FindActionMap("InGameController", throwIfNotFound: true);
        m_InGameController_Direction = m_InGameController.FindAction("Direction", throwIfNotFound: true);
        m_InGameController_Buttons = m_InGameController.FindAction("Buttons", throwIfNotFound: true);
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

    // InGameController
    private readonly InputActionMap m_InGameController;
    private List<IInGameControllerActions> m_InGameControllerActionsCallbackInterfaces = new List<IInGameControllerActions>();
    private readonly InputAction m_InGameController_Direction;
    private readonly InputAction m_InGameController_Buttons;
    public struct InGameControllerActions
    {
        private @InputSystem m_Wrapper;
        public InGameControllerActions(@InputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Direction => m_Wrapper.m_InGameController_Direction;
        public InputAction @Buttons => m_Wrapper.m_InGameController_Buttons;
        public InputActionMap Get() { return m_Wrapper.m_InGameController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InGameControllerActions set) { return set.Get(); }
        public void AddCallbacks(IInGameControllerActions instance)
        {
            if (instance == null || m_Wrapper.m_InGameControllerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_InGameControllerActionsCallbackInterfaces.Add(instance);
            @Direction.started += instance.OnDirection;
            @Direction.performed += instance.OnDirection;
            @Direction.canceled += instance.OnDirection;
            @Buttons.started += instance.OnButtons;
            @Buttons.performed += instance.OnButtons;
            @Buttons.canceled += instance.OnButtons;
        }

        private void UnregisterCallbacks(IInGameControllerActions instance)
        {
            @Direction.started -= instance.OnDirection;
            @Direction.performed -= instance.OnDirection;
            @Direction.canceled -= instance.OnDirection;
            @Buttons.started -= instance.OnButtons;
            @Buttons.performed -= instance.OnButtons;
            @Buttons.canceled -= instance.OnButtons;
        }

        public void RemoveCallbacks(IInGameControllerActions instance)
        {
            if (m_Wrapper.m_InGameControllerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IInGameControllerActions instance)
        {
            foreach (var item in m_Wrapper.m_InGameControllerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_InGameControllerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public InGameControllerActions @InGameController => new InGameControllerActions(this);
    public interface IInGameControllerActions
    {
        void OnDirection(InputAction.CallbackContext context);
        void OnButtons(InputAction.CallbackContext context);
    }
}
