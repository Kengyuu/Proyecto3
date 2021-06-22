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
                    ""path"": ""<Gamepad>/select"",
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
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0176ce08-f62c-46f5-a70c-40c1be6b6b87"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""TutorialInputs"",
            ""id"": ""40abe390-246f-4313-9ee5-5d217731b7cb"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6add19a6-70cc-40d4-ae45-5139fce68115"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseScroll"",
                    ""type"": ""PassThrough"",
                    ""id"": ""563f6fdc-6fc3-4280-b03a-9949671fe317"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1b89f829-253d-4af6-811d-9db4374a87ca"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""c27f0278-f6c5-48c0-b1f1-4e8be29cbe30"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Press(behavior=2)""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""d061106f-0e34-48fc-8aae-7a98fab2550e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""68aac2cb-a9cc-4ca9-b2c1-2db02d8533be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""b35421ed-84c9-4bb7-bb8e-75af77b73f37"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpecialAbility_1"",
                    ""type"": ""Button"",
                    ""id"": ""d60802d4-2f26-4c0f-8176-bea3ba137f68"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""350b7097-510f-4a63-8f40-753d910933e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2cff12e3-9784-48bd-b4f5-289df62bcf92"",
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
                    ""id"": ""f6bcea06-79ad-45fd-a200-7c94510a0d05"",
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
                    ""id"": ""fecc5008-7666-4759-a8d0-b1c6ed4b2839"",
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
                    ""id"": ""f590bb89-038a-4a15-8e94-39730ca44087"",
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
                    ""id"": ""91d24069-2c40-46e6-970f-96e21d2aa555"",
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
                    ""id"": ""953bfed9-32b9-42c0-99be-99f4e662b488"",
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
                    ""id"": ""ae3bd5d4-c5d3-4841-8da5-1a1e7734f48b"",
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
                    ""id"": ""9bff06dc-86b2-4290-b23b-16732af04a9d"",
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
                    ""id"": ""cf2af130-55ec-4fdc-a9df-b1fb009ab0f9"",
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
                    ""id"": ""c4420ef7-6083-46b6-99a5-db49aeb532bf"",
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
                    ""id"": ""972686bc-ba1d-4928-8e13-4affd2b8410a"",
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
                    ""id"": ""01fed448-c233-4962-96d0-aeaa0ee7fd28"",
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
                    ""id"": ""2d5f5829-7705-4a9c-9540-3a03b729b473"",
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
                    ""id"": ""effcf601-fc9d-4f5b-82c4-b126628756b2"",
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
                    ""id"": ""7b753b79-604e-4bc5-99fb-9567b55eebce"",
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
                    ""id"": ""05d21c01-83fc-4994-89dd-866e31748985"",
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
                    ""id"": ""1521cae0-67b4-4ef2-9ce6-ceec528f41aa"",
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
                    ""id"": ""80341c2f-ae49-4dc1-9a7e-5ce939032361"",
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
                    ""id"": ""f10906de-cd8e-4d8d-ad1d-134aa69a39de"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5405de23-5dfe-433e-af0b-775c52c58abd"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Minimap"",
            ""id"": ""4b79d59a-2f7c-446b-bc2a-14f19fc00730"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3f059e74-914d-4ee8-a921-9c57ac78b0d3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Map"",
                    ""type"": ""Button"",
                    ""id"": ""607e0ddd-08be-476e-ad54-5be682cbb9e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2a2a8d71-abf3-4df7-8385-864591946d09"",
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
                    ""id"": ""2acfc74e-c321-408b-b2da-86fd380fbfaa"",
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
                    ""id"": ""d39b67e1-9db0-47d6-97a1-36f307bd4e61"",
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
                    ""id"": ""49129bbe-6363-47ee-91ff-333b96c7563e"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Pause"",
            ""id"": ""9a0651cb-1855-48d7-a0fc-9e328cbfeaca"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""560874a3-8341-4268-be8f-9315c5977adb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d91ba57a-3c32-4f9a-8db3-02a7c38980a3"",
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
                    ""id"": ""3709ab99-e452-43b6-9a4c-d0989bf88b04"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c2f05ae-5f07-4664-8c48-acab68fb169d"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""NoInputs"",
            ""id"": ""83c73d0b-7591-4f57-8642-a022ee38356a"",
            ""actions"": [],
            ""bindings"": []
        },
        {
            ""name"": ""Gameplay_BACKUP"",
            ""id"": ""9e41b8bc-1be3-4329-8800-658ab927966a"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""57604488-2ccb-4d4f-9d86-04e972cfcc13"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseScroll"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0bd9dfe4-9926-4885-8abe-accd8f6ddfce"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9a836e7c-6c2f-4851-91aa-6cca5b83c29e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""527afb49-be62-4a93-a766-1abb19e19545"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Press(behavior=2)""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""8bde9c84-b1cc-4714-bf53-988faf4fd884"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""EnableTrap"",
                    ""type"": ""Button"",
                    ""id"": ""4593f19c-9141-4e2d-abe7-c597db1db1d7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""2a060a13-dad4-48df-a2f0-581559eb0149"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""8b3ddf75-c687-4385-ac47-4e278aae974b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Map"",
                    ""type"": ""Button"",
                    ""id"": ""3d004a8f-a90e-4339-8005-8ec4d5dabeb7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpecialAbility_1"",
                    ""type"": ""Button"",
                    ""id"": ""e37836d8-5ccb-4b8d-8eb2-b37fa7520310"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""be1ca991-51a9-490a-a1ec-caae9af1b462"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""98e0d07a-71b3-4bdd-8f1d-7c8f70f79c37"",
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
                    ""id"": ""638e6697-e417-480c-8406-fc626365c6fd"",
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
                    ""id"": ""9d75c0f7-b6af-4e4c-b7fa-9b6cec0b0cbb"",
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
                    ""id"": ""14dc8fe6-60c8-4e63-8987-4b141d868558"",
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
                    ""id"": ""9f647417-dff2-4f9a-a239-efd5ec7d498e"",
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
                    ""id"": ""bda17b26-da2f-4694-af29-9b683a4a114a"",
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
                    ""id"": ""bf32a58c-f83a-4c23-ba36-fe727166c63d"",
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
                    ""id"": ""e06185d8-a338-4057-b1bf-86fc9ca7d8ec"",
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
                    ""id"": ""f83315c0-3824-4e44-9fea-2959c9e691e6"",
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
                    ""id"": ""99628c82-37bb-4c55-b581-f525d4777db4"",
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
                    ""id"": ""21c64d21-9a60-4793-97a5-78a1fa6282bf"",
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
                    ""id"": ""ede5a51f-6910-460f-ad27-4b1363e6e4dc"",
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
                    ""id"": ""62947f52-67ca-4469-8779-13e620434efc"",
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
                    ""id"": ""74743438-8e9d-4e5d-84cf-9172660d3911"",
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
                    ""id"": ""88fb6ce2-fe95-4c07-bc64-e4f6130f3c49"",
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
                    ""id"": ""f5a2a3a9-d80f-4583-a5ad-204981823d2d"",
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
                    ""id"": ""63865a76-9d32-4d37-a5af-e43bc02dad62"",
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
                    ""id"": ""854a270f-80d3-430e-bba0-2d9283305284"",
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
                    ""id"": ""26c12e37-34a7-4496-a06c-116a66eb8b17"",
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
                    ""id"": ""c10d064a-3c2b-4c9b-b4bd-f00a18bc8206"",
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
                    ""id"": ""fa128443-0c79-474d-9158-91ad92e95a85"",
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
                    ""id"": ""9a177c09-dbb5-4f48-9173-5afffea0b6fc"",
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
                    ""id"": ""aca23480-77ab-44e7-8d47-da52c8c81397"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""152f7bc5-98bb-4dcd-ae74-b65f2e7dad5b"",
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
        // TutorialInputs
        m_TutorialInputs = asset.FindActionMap("TutorialInputs", throwIfNotFound: true);
        m_TutorialInputs_Look = m_TutorialInputs.FindAction("Look", throwIfNotFound: true);
        m_TutorialInputs_MouseScroll = m_TutorialInputs.FindAction("MouseScroll", throwIfNotFound: true);
        m_TutorialInputs_Move = m_TutorialInputs.FindAction("Move", throwIfNotFound: true);
        m_TutorialInputs_Run = m_TutorialInputs.FindAction("Run", throwIfNotFound: true);
        m_TutorialInputs_Jump = m_TutorialInputs.FindAction("Jump", throwIfNotFound: true);
        m_TutorialInputs_Dash = m_TutorialInputs.FindAction("Dash", throwIfNotFound: true);
        m_TutorialInputs_Shoot = m_TutorialInputs.FindAction("Shoot", throwIfNotFound: true);
        m_TutorialInputs_SpecialAbility_1 = m_TutorialInputs.FindAction("SpecialAbility_1", throwIfNotFound: true);
        m_TutorialInputs_Pause = m_TutorialInputs.FindAction("Pause", throwIfNotFound: true);
        // Minimap
        m_Minimap = asset.FindActionMap("Minimap", throwIfNotFound: true);
        m_Minimap_Look = m_Minimap.FindAction("Look", throwIfNotFound: true);
        m_Minimap_Map = m_Minimap.FindAction("Map", throwIfNotFound: true);
        // Pause
        m_Pause = asset.FindActionMap("Pause", throwIfNotFound: true);
        m_Pause_Pause = m_Pause.FindAction("Pause", throwIfNotFound: true);
        // NoInputs
        m_NoInputs = asset.FindActionMap("NoInputs", throwIfNotFound: true);
        // Gameplay_BACKUP
        m_Gameplay_BACKUP = asset.FindActionMap("Gameplay_BACKUP", throwIfNotFound: true);
        m_Gameplay_BACKUP_Look = m_Gameplay_BACKUP.FindAction("Look", throwIfNotFound: true);
        m_Gameplay_BACKUP_MouseScroll = m_Gameplay_BACKUP.FindAction("MouseScroll", throwIfNotFound: true);
        m_Gameplay_BACKUP_Move = m_Gameplay_BACKUP.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_BACKUP_Run = m_Gameplay_BACKUP.FindAction("Run", throwIfNotFound: true);
        m_Gameplay_BACKUP_Jump = m_Gameplay_BACKUP.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_BACKUP_EnableTrap = m_Gameplay_BACKUP.FindAction("EnableTrap", throwIfNotFound: true);
        m_Gameplay_BACKUP_Dash = m_Gameplay_BACKUP.FindAction("Dash", throwIfNotFound: true);
        m_Gameplay_BACKUP_Shoot = m_Gameplay_BACKUP.FindAction("Shoot", throwIfNotFound: true);
        m_Gameplay_BACKUP_Map = m_Gameplay_BACKUP.FindAction("Map", throwIfNotFound: true);
        m_Gameplay_BACKUP_SpecialAbility_1 = m_Gameplay_BACKUP.FindAction("SpecialAbility_1", throwIfNotFound: true);
        m_Gameplay_BACKUP_Pause = m_Gameplay_BACKUP.FindAction("Pause", throwIfNotFound: true);
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

    // TutorialInputs
    private readonly InputActionMap m_TutorialInputs;
    private ITutorialInputsActions m_TutorialInputsActionsCallbackInterface;
    private readonly InputAction m_TutorialInputs_Look;
    private readonly InputAction m_TutorialInputs_MouseScroll;
    private readonly InputAction m_TutorialInputs_Move;
    private readonly InputAction m_TutorialInputs_Run;
    private readonly InputAction m_TutorialInputs_Jump;
    private readonly InputAction m_TutorialInputs_Dash;
    private readonly InputAction m_TutorialInputs_Shoot;
    private readonly InputAction m_TutorialInputs_SpecialAbility_1;
    private readonly InputAction m_TutorialInputs_Pause;
    public struct TutorialInputsActions
    {
        private @PlayerInputSystem m_Wrapper;
        public TutorialInputsActions(@PlayerInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_TutorialInputs_Look;
        public InputAction @MouseScroll => m_Wrapper.m_TutorialInputs_MouseScroll;
        public InputAction @Move => m_Wrapper.m_TutorialInputs_Move;
        public InputAction @Run => m_Wrapper.m_TutorialInputs_Run;
        public InputAction @Jump => m_Wrapper.m_TutorialInputs_Jump;
        public InputAction @Dash => m_Wrapper.m_TutorialInputs_Dash;
        public InputAction @Shoot => m_Wrapper.m_TutorialInputs_Shoot;
        public InputAction @SpecialAbility_1 => m_Wrapper.m_TutorialInputs_SpecialAbility_1;
        public InputAction @Pause => m_Wrapper.m_TutorialInputs_Pause;
        public InputActionMap Get() { return m_Wrapper.m_TutorialInputs; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TutorialInputsActions set) { return set.Get(); }
        public void SetCallbacks(ITutorialInputsActions instance)
        {
            if (m_Wrapper.m_TutorialInputsActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnLook;
                @MouseScroll.started -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnMouseScroll;
                @MouseScroll.performed -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnMouseScroll;
                @MouseScroll.canceled -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnMouseScroll;
                @Move.started -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnJump;
                @Dash.started -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnDash;
                @Shoot.started -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnShoot;
                @SpecialAbility_1.started -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnSpecialAbility_1;
                @SpecialAbility_1.performed -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnSpecialAbility_1;
                @SpecialAbility_1.canceled -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnSpecialAbility_1;
                @Pause.started -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_TutorialInputsActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_TutorialInputsActionsCallbackInterface = instance;
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
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @SpecialAbility_1.started += instance.OnSpecialAbility_1;
                @SpecialAbility_1.performed += instance.OnSpecialAbility_1;
                @SpecialAbility_1.canceled += instance.OnSpecialAbility_1;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public TutorialInputsActions @TutorialInputs => new TutorialInputsActions(this);

    // Minimap
    private readonly InputActionMap m_Minimap;
    private IMinimapActions m_MinimapActionsCallbackInterface;
    private readonly InputAction m_Minimap_Look;
    private readonly InputAction m_Minimap_Map;
    public struct MinimapActions
    {
        private @PlayerInputSystem m_Wrapper;
        public MinimapActions(@PlayerInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_Minimap_Look;
        public InputAction @Map => m_Wrapper.m_Minimap_Map;
        public InputActionMap Get() { return m_Wrapper.m_Minimap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MinimapActions set) { return set.Get(); }
        public void SetCallbacks(IMinimapActions instance)
        {
            if (m_Wrapper.m_MinimapActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_MinimapActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_MinimapActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_MinimapActionsCallbackInterface.OnLook;
                @Map.started -= m_Wrapper.m_MinimapActionsCallbackInterface.OnMap;
                @Map.performed -= m_Wrapper.m_MinimapActionsCallbackInterface.OnMap;
                @Map.canceled -= m_Wrapper.m_MinimapActionsCallbackInterface.OnMap;
            }
            m_Wrapper.m_MinimapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Map.started += instance.OnMap;
                @Map.performed += instance.OnMap;
                @Map.canceled += instance.OnMap;
            }
        }
    }
    public MinimapActions @Minimap => new MinimapActions(this);

    // Pause
    private readonly InputActionMap m_Pause;
    private IPauseActions m_PauseActionsCallbackInterface;
    private readonly InputAction m_Pause_Pause;
    public struct PauseActions
    {
        private @PlayerInputSystem m_Wrapper;
        public PauseActions(@PlayerInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_Pause_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Pause; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PauseActions set) { return set.Get(); }
        public void SetCallbacks(IPauseActions instance)
        {
            if (m_Wrapper.m_PauseActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_PauseActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_PauseActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_PauseActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_PauseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public PauseActions @Pause => new PauseActions(this);

    // NoInputs
    private readonly InputActionMap m_NoInputs;
    private INoInputsActions m_NoInputsActionsCallbackInterface;
    public struct NoInputsActions
    {
        private @PlayerInputSystem m_Wrapper;
        public NoInputsActions(@PlayerInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_NoInputs; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NoInputsActions set) { return set.Get(); }
        public void SetCallbacks(INoInputsActions instance)
        {
            if (m_Wrapper.m_NoInputsActionsCallbackInterface != null)
            {
            }
            m_Wrapper.m_NoInputsActionsCallbackInterface = instance;
            if (instance != null)
            {
            }
        }
    }
    public NoInputsActions @NoInputs => new NoInputsActions(this);

    // Gameplay_BACKUP
    private readonly InputActionMap m_Gameplay_BACKUP;
    private IGameplay_BACKUPActions m_Gameplay_BACKUPActionsCallbackInterface;
    private readonly InputAction m_Gameplay_BACKUP_Look;
    private readonly InputAction m_Gameplay_BACKUP_MouseScroll;
    private readonly InputAction m_Gameplay_BACKUP_Move;
    private readonly InputAction m_Gameplay_BACKUP_Run;
    private readonly InputAction m_Gameplay_BACKUP_Jump;
    private readonly InputAction m_Gameplay_BACKUP_EnableTrap;
    private readonly InputAction m_Gameplay_BACKUP_Dash;
    private readonly InputAction m_Gameplay_BACKUP_Shoot;
    private readonly InputAction m_Gameplay_BACKUP_Map;
    private readonly InputAction m_Gameplay_BACKUP_SpecialAbility_1;
    private readonly InputAction m_Gameplay_BACKUP_Pause;
    public struct Gameplay_BACKUPActions
    {
        private @PlayerInputSystem m_Wrapper;
        public Gameplay_BACKUPActions(@PlayerInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_Gameplay_BACKUP_Look;
        public InputAction @MouseScroll => m_Wrapper.m_Gameplay_BACKUP_MouseScroll;
        public InputAction @Move => m_Wrapper.m_Gameplay_BACKUP_Move;
        public InputAction @Run => m_Wrapper.m_Gameplay_BACKUP_Run;
        public InputAction @Jump => m_Wrapper.m_Gameplay_BACKUP_Jump;
        public InputAction @EnableTrap => m_Wrapper.m_Gameplay_BACKUP_EnableTrap;
        public InputAction @Dash => m_Wrapper.m_Gameplay_BACKUP_Dash;
        public InputAction @Shoot => m_Wrapper.m_Gameplay_BACKUP_Shoot;
        public InputAction @Map => m_Wrapper.m_Gameplay_BACKUP_Map;
        public InputAction @SpecialAbility_1 => m_Wrapper.m_Gameplay_BACKUP_SpecialAbility_1;
        public InputAction @Pause => m_Wrapper.m_Gameplay_BACKUP_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay_BACKUP; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Gameplay_BACKUPActions set) { return set.Get(); }
        public void SetCallbacks(IGameplay_BACKUPActions instance)
        {
            if (m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnLook;
                @MouseScroll.started -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnMouseScroll;
                @MouseScroll.performed -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnMouseScroll;
                @MouseScroll.canceled -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnMouseScroll;
                @Move.started -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnJump;
                @EnableTrap.started -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnEnableTrap;
                @EnableTrap.performed -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnEnableTrap;
                @EnableTrap.canceled -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnEnableTrap;
                @Dash.started -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnDash;
                @Shoot.started -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnShoot;
                @Map.started -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnMap;
                @Map.performed -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnMap;
                @Map.canceled -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnMap;
                @SpecialAbility_1.started -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnSpecialAbility_1;
                @SpecialAbility_1.performed -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnSpecialAbility_1;
                @SpecialAbility_1.canceled -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnSpecialAbility_1;
                @Pause.started -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_Gameplay_BACKUPActionsCallbackInterface = instance;
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
    public Gameplay_BACKUPActions @Gameplay_BACKUP => new Gameplay_BACKUPActions(this);
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
    public interface ITutorialInputsActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnMouseScroll(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnSpecialAbility_1(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
    public interface IMinimapActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnMap(InputAction.CallbackContext context);
    }
    public interface IPauseActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
    public interface INoInputsActions
    {
    }
    public interface IGameplay_BACKUPActions
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
