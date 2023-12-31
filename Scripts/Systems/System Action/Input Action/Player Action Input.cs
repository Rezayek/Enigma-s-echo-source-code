//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Systems/System Action/Input Action/Player Action Input.inputactions
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

public partial class @PlayerActionInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActionInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player Action Input"",
    ""maps"": [
        {
            ""name"": ""Player Action"",
            ""id"": ""402f2e40-34cd-4c75-b2d6-614b2e36aefa"",
            ""actions"": [
                {
                    ""name"": ""Global"",
                    ""type"": ""Button"",
                    ""id"": ""0a72f150-602a-49e3-a013-387014de3cdb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Health"",
                    ""type"": ""Button"",
                    ""id"": ""39891048-f276-4a5c-b23e-c88d06b009a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sanity"",
                    ""type"": ""Button"",
                    ""id"": ""ab762710-e2b0-4f9c-bdef-2a6d0ac2483d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Torch"",
                    ""type"": ""Button"",
                    ""id"": ""c8efc482-2fe0-4cf4-b431-89d5922ae6b7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inspect"",
                    ""type"": ""Button"",
                    ""id"": ""a8e65082-b773-450b-91a7-7bf1cb1e8f3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Value"",
                    ""id"": ""9ebd5fa8-881e-45ca-b55d-70b921ee85e0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Mouse Pressed"",
                    ""type"": ""Button"",
                    ""id"": ""2d27f1cd-9c98-460a-8d99-fda40dc07acb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Bag"",
                    ""type"": ""Button"",
                    ""id"": ""7753c39f-75b5-442c-beb3-04262bb5558b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""bfc9032c-8e97-4b1d-a098-dd3e83d7dc6d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""e74098ee-9f6c-4447-8de6-d9185211af90"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a2a4235b-ced9-4a8f-8041-3df5d03b0f4e"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Global"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""810984a7-cff4-4733-9d8c-e7e91a6a60e3"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Health"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""64cdf111-a657-4368-a699-9f9e366e527a"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sanity"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c30ba8be-3ac3-411d-8a0d-82279a33acca"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Torch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""edb9ad13-d935-46e9-99ff-dadd96bbd07c"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inspect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""52837e45-8347-40ce-9593-98f1ef55c93e"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f33299d-e5d4-4e4c-bca6-4c66ed81e31a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Pressed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24bcc3ba-8d82-451a-b839-fbe93f920022"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Bag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d52c4d90-c70c-4c9b-a9dc-281cde511f5c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f3c2f28e-0586-427c-9426-2b2f4259791a"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Action
        m_PlayerAction = asset.FindActionMap("Player Action", throwIfNotFound: true);
        m_PlayerAction_Global = m_PlayerAction.FindAction("Global", throwIfNotFound: true);
        m_PlayerAction_Health = m_PlayerAction.FindAction("Health", throwIfNotFound: true);
        m_PlayerAction_Sanity = m_PlayerAction.FindAction("Sanity", throwIfNotFound: true);
        m_PlayerAction_Torch = m_PlayerAction.FindAction("Torch", throwIfNotFound: true);
        m_PlayerAction_Inspect = m_PlayerAction.FindAction("Inspect", throwIfNotFound: true);
        m_PlayerAction_Mouse = m_PlayerAction.FindAction("Mouse", throwIfNotFound: true);
        m_PlayerAction_MousePressed = m_PlayerAction.FindAction("Mouse Pressed", throwIfNotFound: true);
        m_PlayerAction_Bag = m_PlayerAction.FindAction("Bag", throwIfNotFound: true);
        m_PlayerAction_Click = m_PlayerAction.FindAction("Click", throwIfNotFound: true);
        m_PlayerAction_Pause = m_PlayerAction.FindAction("Pause", throwIfNotFound: true);
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

    // Player Action
    private readonly InputActionMap m_PlayerAction;
    private IPlayerActionActions m_PlayerActionActionsCallbackInterface;
    private readonly InputAction m_PlayerAction_Global;
    private readonly InputAction m_PlayerAction_Health;
    private readonly InputAction m_PlayerAction_Sanity;
    private readonly InputAction m_PlayerAction_Torch;
    private readonly InputAction m_PlayerAction_Inspect;
    private readonly InputAction m_PlayerAction_Mouse;
    private readonly InputAction m_PlayerAction_MousePressed;
    private readonly InputAction m_PlayerAction_Bag;
    private readonly InputAction m_PlayerAction_Click;
    private readonly InputAction m_PlayerAction_Pause;
    public struct PlayerActionActions
    {
        private @PlayerActionInput m_Wrapper;
        public PlayerActionActions(@PlayerActionInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Global => m_Wrapper.m_PlayerAction_Global;
        public InputAction @Health => m_Wrapper.m_PlayerAction_Health;
        public InputAction @Sanity => m_Wrapper.m_PlayerAction_Sanity;
        public InputAction @Torch => m_Wrapper.m_PlayerAction_Torch;
        public InputAction @Inspect => m_Wrapper.m_PlayerAction_Inspect;
        public InputAction @Mouse => m_Wrapper.m_PlayerAction_Mouse;
        public InputAction @MousePressed => m_Wrapper.m_PlayerAction_MousePressed;
        public InputAction @Bag => m_Wrapper.m_PlayerAction_Bag;
        public InputAction @Click => m_Wrapper.m_PlayerAction_Click;
        public InputAction @Pause => m_Wrapper.m_PlayerAction_Pause;
        public InputActionMap Get() { return m_Wrapper.m_PlayerAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionActions instance)
        {
            if (m_Wrapper.m_PlayerActionActionsCallbackInterface != null)
            {
                @Global.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnGlobal;
                @Global.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnGlobal;
                @Global.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnGlobal;
                @Health.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnHealth;
                @Health.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnHealth;
                @Health.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnHealth;
                @Sanity.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnSanity;
                @Sanity.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnSanity;
                @Sanity.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnSanity;
                @Torch.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnTorch;
                @Torch.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnTorch;
                @Torch.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnTorch;
                @Inspect.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnInspect;
                @Inspect.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnInspect;
                @Inspect.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnInspect;
                @Mouse.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnMouse;
                @MousePressed.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnMousePressed;
                @MousePressed.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnMousePressed;
                @MousePressed.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnMousePressed;
                @Bag.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnBag;
                @Bag.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnBag;
                @Bag.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnBag;
                @Click.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnClick;
                @Pause.started -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PlayerActionActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PlayerActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Global.started += instance.OnGlobal;
                @Global.performed += instance.OnGlobal;
                @Global.canceled += instance.OnGlobal;
                @Health.started += instance.OnHealth;
                @Health.performed += instance.OnHealth;
                @Health.canceled += instance.OnHealth;
                @Sanity.started += instance.OnSanity;
                @Sanity.performed += instance.OnSanity;
                @Sanity.canceled += instance.OnSanity;
                @Torch.started += instance.OnTorch;
                @Torch.performed += instance.OnTorch;
                @Torch.canceled += instance.OnTorch;
                @Inspect.started += instance.OnInspect;
                @Inspect.performed += instance.OnInspect;
                @Inspect.canceled += instance.OnInspect;
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
                @MousePressed.started += instance.OnMousePressed;
                @MousePressed.performed += instance.OnMousePressed;
                @MousePressed.canceled += instance.OnMousePressed;
                @Bag.started += instance.OnBag;
                @Bag.performed += instance.OnBag;
                @Bag.canceled += instance.OnBag;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PlayerActionActions @PlayerAction => new PlayerActionActions(this);
    public interface IPlayerActionActions
    {
        void OnGlobal(InputAction.CallbackContext context);
        void OnHealth(InputAction.CallbackContext context);
        void OnSanity(InputAction.CallbackContext context);
        void OnTorch(InputAction.CallbackContext context);
        void OnInspect(InputAction.CallbackContext context);
        void OnMouse(InputAction.CallbackContext context);
        void OnMousePressed(InputAction.CallbackContext context);
        void OnBag(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
