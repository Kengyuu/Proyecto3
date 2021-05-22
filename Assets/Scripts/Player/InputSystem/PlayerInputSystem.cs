// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/InputSystem/PlayerInputSystem.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputSystem : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputSystem"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""42f65eb9-2de1-4e11-8652-e370cb2ef8e0"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c0a626f7-d3f7-4fb7-b915-1a61713edb2f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseScroll"",
                    ""type"": ""PassThrough"",
                    ""id"": ""77f109c9-7b71-40ef-85d1-42404af68fb7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""56852487-0044-4815-8d55-6e1fa75c1814"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""ba15e259-18ba-4f86-9b27-81730df603af"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Press(behavior=2)""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""95ddad16-0fd9-4cc7-a478-fc2d314c8d5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""EnableTrap"",
                    ""type"": ""Button"",
                    ""id"": ""39645a81-ae8d-4b62-b24b-1d592566c018"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""3202d8a1-1462-4ee2-a10e-03284b14797d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""9050c616-516f-4b57-a10b-ef2daf21ee61"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Map"",
                    ""type"": ""Button"",
                    ""id"": ""fe9f36e8-f782-4b8b-9588-ee642fb6a1ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpecialAbility_1"",
                    ""type"": ""Button"",
                    ""id"": ""68a95db9-0091-4fe7-96ce-6a2865cf4e08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""cec2ad91-a77c-4f0e-a826-57369479fe52"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""039dfe7a-7ae3-4cde-8b45-0b55c943f5d2"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5b65486-a2d9-4fe4-9c36-b719f773576f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone,ScaleVector2(x=10,y=10)"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""889beccb-d4ba-437d-801f-c054efb85f84"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc129510-59cc-47cb-bd24-b8ed1344aed2"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""784660c7-6893-4ca9-b11d-af6ae3318c93"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""643c4160-c73b-456c-bb12-8927000c597a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b070a1b6-14a6-49e1-bd9a-123e02c81d06"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bbe3a954-e467-4180-8964-2c56dd640442"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6ded0bf8-05c1-408a-b12c-dbdd0a692f47"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""605af190-9a9b-429c-81b0-e5ed9b057256"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef056052-c7f4-4a4f-8e38-5a805b4d80ae"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""965c1515-0f19-4de5-a21c-4127c4dfa966"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""15f0adb6-c7c7-4371-bfb1-53d9babdf641"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f6709a8-7f87-47f3-9d72-94c8100db269"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f6c3f03a-f665-4e46-a1b6-bb972c3ddf98"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41abeb14-4db1-49b2-bf83-258fda4547d0"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7fca1e3-6487-4084-b06c-4ce245c03b3f"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""391b435b-f0e1-4b65-8164-15e2b7551d3c"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialAbility_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60304c04-71a8-4ea2-a40d-cfeb71c9c981"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialAbility_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a643954-d5dc-4561-8202-6695e4321fc8"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnableTrap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b76c86ee-c0a7-4ce6-af83-de42593f46af"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""EnableTrap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73bf1cec-c747-4387-a97d-05262c50251f"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90ce022b-933e-4237-a21a-40d2adcf3ec9"",
                    ""path"": ""<Gamepad>/rightShoulder"",
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
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Look = m_Gameplay.FindAction("Look", throwIfNotFound: true);
        m_Gameplay_MouseScroll = m_Gameplay.FindAction("MouseScroll", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Run = m_Gameplay.FindAction("Run", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_EnableTrap = m_Gameplay.FindAction("EnableTrap", throwIfNotFound: true);
        m_Gameplay_Dash = m_Gameplay.FindAction("Dash", throwIfNotFound: true);
        m_Gameplay_Shoot = m_Gameplay.FindAction("Shoot", throwIfNotFound: true);
        m_Gameplay_Map = m_Gameplay.FindAction("Map", throwIfNotFound: true);
        m_Gameplay_SpecialAbility_1 = m_Gameplay.FindAction("SpecialAbility_1", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Look;
    private readonly InputAction m_Gameplay_MouseScroll;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Run;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_EnableTrap;
    private readonly InputAction m_Gameplay_Dash;
    private readonly InputAction m_Gameplay_Shoot;
    private readonly InputAction m_Gameplay_Map;
    private readonly InputAction m_Gameplay_SpecialAbility_1;
    private readonly InputAction m_Gameplay_Pause;
    public struct GameplayActions
    {
        private @PlayerInputSystem m_Wrapper;
        public GameplayActions(@PlayerInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_Gameplay_Look;
        public InputAction @MouseScroll => m_Wrapper.m_Gameplay_MouseScroll;
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Run => m_Wrapper.m_Gameplay_Run;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @EnableTrap => m_Wrapper.m_Gameplay_EnableTrap;
        public InputAction @Dash => m_Wrapper.m_Gameplay_Dash;
        public InputAction @Shoot => m_Wrapper.m_Gameplay_Shoot;
        public InputAction @Map => m_Wrapper.m_Gameplay_Map;
        public InputAction @SpecialAbility_1 => m_Wrapper.m_Gameplay_SpecialAbility_1;
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLook;
                @MouseScroll.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseScroll;
                @MouseScroll.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseScroll;
                @MouseScroll.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseScroll;
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @EnableTrap.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEnableTrap;
                @EnableTrap.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEnableTrap;
                @EnableTrap.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEnableTrap;
                @Dash.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Shoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Map.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMap;
                @Map.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMap;
                @Map.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMap;
                @SpecialAbility_1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAbility_1;
                @SpecialAbility_1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAbility_1;
                @SpecialAbility_1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAbility_1;
                @Pause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @MouseScroll.started += instance.OnMouseScroll;
                @MouseScroll.performed += instance.OnMouseScroll;
                @MouseScroll.canceled += instance.OnMouseScroll;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @EnableTrap.started += instance.OnEnableTrap;
                @EnableTrap.performed += instance.OnEnableTrap;
                @EnableTrap.canceled += instance.OnEnableTrap;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Map.started += instance.OnMap;
                @Map.performed += instance.OnMap;
                @Map.canceled += instance.OnMap;
                @SpecialAbility_1.started += instance.OnSpecialAbility_1;
                @SpecialAbility_1.performed += instance.OnSpecialAbility_1;
                @SpecialAbility_1.canceled += instance.OnSpecialAbility_1;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnMouseScroll(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnEnableTrap(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnMap(InputAction.CallbackContext context);
        void OnSpecialAbility_1(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
