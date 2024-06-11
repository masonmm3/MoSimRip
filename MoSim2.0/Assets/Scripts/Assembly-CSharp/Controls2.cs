using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Controls2 : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
{
	public struct RobotControlsActions
	{
		private Controls2 m_Wrapper;

		public InputAction Translate => m_Wrapper.m_RobotControls_Translate;

		public InputAction Rotate => m_Wrapper.m_RobotControls_Rotate;

		public InputAction Shoot => m_Wrapper.m_RobotControls_Shoot;

		public InputAction Amp => m_Wrapper.m_RobotControls_Amp;

		public InputAction AmpSpeaker => m_Wrapper.m_RobotControls_AmpSpeaker;

		public InputAction RestartGame => m_Wrapper.m_RobotControls_RestartGame;

		public InputAction Intake => m_Wrapper.m_RobotControls_Intake;

		public InputAction Menu => m_Wrapper.m_RobotControls_Menu;

		public InputAction MoveCamera => m_Wrapper.m_RobotControls_MoveCamera;

		public InputAction AlignRobot => m_Wrapper.m_RobotControls_AlignRobot;

		public InputAction Climb => m_Wrapper.m_RobotControls_Climb;

		public bool enabled => Get().enabled;

		public RobotControlsActions(Controls2 wrapper)
		{
			m_Wrapper = wrapper;
		}

		public InputActionMap Get()
		{
			return m_Wrapper.m_RobotControls;
		}

		public void Enable()
		{
			Get().Enable();
		}

		public void Disable()
		{
			Get().Disable();
		}

		public static implicit operator InputActionMap(RobotControlsActions set)
		{
			return set.Get();
		}

		public void AddCallbacks(IRobotControlsActions instance)
		{
			if (instance != null && !m_Wrapper.m_RobotControlsActionsCallbackInterfaces.Contains(instance))
			{
				m_Wrapper.m_RobotControlsActionsCallbackInterfaces.Add(instance);
				Translate.started += instance.OnTranslate;
				Translate.performed += instance.OnTranslate;
				Translate.canceled += instance.OnTranslate;
				Rotate.started += instance.OnRotate;
				Rotate.performed += instance.OnRotate;
				Rotate.canceled += instance.OnRotate;
				Shoot.started += instance.OnShoot;
				Shoot.performed += instance.OnShoot;
				Shoot.canceled += instance.OnShoot;
				Amp.started += instance.OnAmp;
				Amp.performed += instance.OnAmp;
				Amp.canceled += instance.OnAmp;
				AmpSpeaker.started += instance.OnAmpSpeaker;
				AmpSpeaker.performed += instance.OnAmpSpeaker;
				AmpSpeaker.canceled += instance.OnAmpSpeaker;
				RestartGame.started += instance.OnRestartGame;
				RestartGame.performed += instance.OnRestartGame;
				RestartGame.canceled += instance.OnRestartGame;
				Intake.started += instance.OnIntake;
				Intake.performed += instance.OnIntake;
				Intake.canceled += instance.OnIntake;
				Menu.started += instance.OnMenu;
				Menu.performed += instance.OnMenu;
				Menu.canceled += instance.OnMenu;
				MoveCamera.started += instance.OnMoveCamera;
				MoveCamera.performed += instance.OnMoveCamera;
				MoveCamera.canceled += instance.OnMoveCamera;
				AlignRobot.started += instance.OnAlignRobot;
				AlignRobot.performed += instance.OnAlignRobot;
				AlignRobot.canceled += instance.OnAlignRobot;
				Climb.started += instance.OnClimb;
				Climb.performed += instance.OnClimb;
				Climb.canceled += instance.OnClimb;
			}
		}

		private void UnregisterCallbacks(IRobotControlsActions instance)
		{
			Translate.started -= instance.OnTranslate;
			Translate.performed -= instance.OnTranslate;
			Translate.canceled -= instance.OnTranslate;
			Rotate.started -= instance.OnRotate;
			Rotate.performed -= instance.OnRotate;
			Rotate.canceled -= instance.OnRotate;
			Shoot.started -= instance.OnShoot;
			Shoot.performed -= instance.OnShoot;
			Shoot.canceled -= instance.OnShoot;
			Amp.started -= instance.OnAmp;
			Amp.performed -= instance.OnAmp;
			Amp.canceled -= instance.OnAmp;
			AmpSpeaker.started -= instance.OnAmpSpeaker;
			AmpSpeaker.performed -= instance.OnAmpSpeaker;
			AmpSpeaker.canceled -= instance.OnAmpSpeaker;
			RestartGame.started -= instance.OnRestartGame;
			RestartGame.performed -= instance.OnRestartGame;
			RestartGame.canceled -= instance.OnRestartGame;
			Intake.started -= instance.OnIntake;
			Intake.performed -= instance.OnIntake;
			Intake.canceled -= instance.OnIntake;
			Menu.started -= instance.OnMenu;
			Menu.performed -= instance.OnMenu;
			Menu.canceled -= instance.OnMenu;
			MoveCamera.started -= instance.OnMoveCamera;
			MoveCamera.performed -= instance.OnMoveCamera;
			MoveCamera.canceled -= instance.OnMoveCamera;
			AlignRobot.started -= instance.OnAlignRobot;
			AlignRobot.performed -= instance.OnAlignRobot;
			AlignRobot.canceled -= instance.OnAlignRobot;
			Climb.started -= instance.OnClimb;
			Climb.performed -= instance.OnClimb;
			Climb.canceled -= instance.OnClimb;
		}

		public void RemoveCallbacks(IRobotControlsActions instance)
		{
			if (m_Wrapper.m_RobotControlsActionsCallbackInterfaces.Remove(instance))
			{
				UnregisterCallbacks(instance);
			}
		}

		public void SetCallbacks(IRobotControlsActions instance)
		{
			foreach (IRobotControlsActions robotControlsActionsCallbackInterface in m_Wrapper.m_RobotControlsActionsCallbackInterfaces)
			{
				UnregisterCallbacks(robotControlsActionsCallbackInterface);
			}
			m_Wrapper.m_RobotControlsActionsCallbackInterfaces.Clear();
			AddCallbacks(instance);
		}
	}

	public interface IRobotControlsActions
	{
		void OnTranslate(InputAction.CallbackContext context);

		void OnRotate(InputAction.CallbackContext context);

		void OnShoot(InputAction.CallbackContext context);

		void OnAmp(InputAction.CallbackContext context);

		void OnAmpSpeaker(InputAction.CallbackContext context);

		void OnRestartGame(InputAction.CallbackContext context);

		void OnIntake(InputAction.CallbackContext context);

		void OnMenu(InputAction.CallbackContext context);

		void OnMoveCamera(InputAction.CallbackContext context);

		void OnAlignRobot(InputAction.CallbackContext context);

		void OnClimb(InputAction.CallbackContext context);
	}

	private readonly InputActionMap m_RobotControls;

	private List<IRobotControlsActions> m_RobotControlsActionsCallbackInterfaces = new List<IRobotControlsActions>();

	private readonly InputAction m_RobotControls_Translate;

	private readonly InputAction m_RobotControls_Rotate;

	private readonly InputAction m_RobotControls_Shoot;

	private readonly InputAction m_RobotControls_Amp;

	private readonly InputAction m_RobotControls_AmpSpeaker;

	private readonly InputAction m_RobotControls_RestartGame;

	private readonly InputAction m_RobotControls_Intake;

	private readonly InputAction m_RobotControls_Menu;

	private readonly InputAction m_RobotControls_MoveCamera;

	private readonly InputAction m_RobotControls_AlignRobot;

	private readonly InputAction m_RobotControls_Climb;

	private int m_Gamepad1SchemeIndex = -1;

	private int m_Gamepad2SchemeIndex = -1;

	private int m_KeyboardSchemeIndex = -1;

	public InputActionAsset asset { get; }

	public InputBinding? bindingMask
	{
		get
		{
			return asset.bindingMask;
		}
		set
		{
			asset.bindingMask = value;
		}
	}

	public ReadOnlyArray<InputDevice>? devices
	{
		get
		{
			return asset.devices;
		}
		set
		{
			asset.devices = value;
		}
	}

	public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

	public IEnumerable<InputBinding> bindings => asset.bindings;

	public RobotControlsActions RobotControls => new RobotControlsActions(this);

	public InputControlScheme Gamepad1Scheme
	{
		get
		{
			if (m_Gamepad1SchemeIndex == -1)
			{
				m_Gamepad1SchemeIndex = asset.FindControlSchemeIndex("Gamepad 1");
			}
			return asset.controlSchemes[m_Gamepad1SchemeIndex];
		}
	}

	public InputControlScheme Gamepad2Scheme
	{
		get
		{
			if (m_Gamepad2SchemeIndex == -1)
			{
				m_Gamepad2SchemeIndex = asset.FindControlSchemeIndex("Gamepad 2");
			}
			return asset.controlSchemes[m_Gamepad2SchemeIndex];
		}
	}

	public InputControlScheme KeyboardScheme
	{
		get
		{
			if (m_KeyboardSchemeIndex == -1)
			{
				m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
			}
			return asset.controlSchemes[m_KeyboardSchemeIndex];
		}
	}

	public Controls2()
	{
		asset = InputActionAsset.FromJson("{\n    \"name\": \"Controls 2\",\n    \"maps\": [\n        {\n            \"name\": \"RobotControls\",\n            \"id\": \"89a7e69c-f53b-4f6c-8858-8cc9ef3387ad\",\n            \"actions\": [\n                {\n                    \"name\": \"Translate\",\n                    \"type\": \"Value\",\n                    \"id\": \"f1728e11-e8a3-44e1-afa8-dc82bfa2b09c\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Rotate\",\n                    \"type\": \"Value\",\n                    \"id\": \"c1a011bd-4a6c-43e8-9628-9f67957f5bd6\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Shoot\",\n                    \"type\": \"Button\",\n                    \"id\": \"366e5f2a-50aa-4e72-9970-81185afa77ed\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Amp\",\n                    \"type\": \"Button\",\n                    \"id\": \"c6a5ad7d-fe75-4a29-99de-247f4ccb82a4\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"AmpSpeaker\",\n                    \"type\": \"Button\",\n                    \"id\": \"5bb25259-bb95-4bbb-acb9-b2858aeaca3f\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"RestartGame\",\n                    \"type\": \"Button\",\n                    \"id\": \"d8a775e3-36d2-407c-bda3-b40c9aef891f\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Intake\",\n                    \"type\": \"Button\",\n                    \"id\": \"a9b12e82-ded3-41f0-98ed-4abebfeb9858\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"Hold\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Menu\",\n                    \"type\": \"Button\",\n                    \"id\": \"5d2dc4e2-ab57-42cf-a74d-61aabd314d36\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"MoveCamera\",\n                    \"type\": \"Value\",\n                    \"id\": \"5df2fb3f-75ff-4f84-9b9e-e02231ca2068\",\n                    \"expectedControlType\": \"Dpad\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"AlignRobot\",\n                    \"type\": \"Button\",\n                    \"id\": \"551e135a-cc86-49a3-b180-2fad0983ba73\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Climb\",\n                    \"type\": \"Button\",\n                    \"id\": \"a48eddac-c518-4fdb-b989-836f60ae3765\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"0a0ab686-a95e-408e-bdf1-1c03a7b38753\",\n                    \"path\": \"<Gamepad>/leftStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 1\",\n                    \"action\": \"Translate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"c52b80be-6d77-4050-9cfb-6a57ec5e1d2f\",\n                    \"path\": \"<Gamepad>/leftStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 2\",\n                    \"action\": \"Translate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"WASD\",\n                    \"id\": \"5392e0f7-681b-40d5-908f-ddc16e1f9ed8\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Translate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"e6019125-78c0-4ff6-bf91-38c2dab01e3f\",\n                    \"path\": \"<Keyboard>/w\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Translate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"c564f48b-d057-4edc-a7e1-a9112f451bc0\",\n                    \"path\": \"<Keyboard>/s\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Translate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"bfc1bf36-1d80-43e6-9d8c-2a1c4e5cde7f\",\n                    \"path\": \"<Keyboard>/a\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Translate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"533c5bc0-ae2b-4972-8808-9a59aceb613f\",\n                    \"path\": \"<Keyboard>/d\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Translate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f1b143fb-3c9f-490a-a07a-b42f6ae42bf4\",\n                    \"path\": \"<Gamepad>/rightStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 1\",\n                    \"action\": \"Rotate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e594408e-e0f9-4b66-8022-498362f72874\",\n                    \"path\": \"<Gamepad>/rightStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 2\",\n                    \"action\": \"Rotate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"2D Vector\",\n                    \"id\": \"214f89c0-59bf-4c93-8429-93afd04033be\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Rotate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"283531c5-975d-4b2e-9eb7-4024cdd01a28\",\n                    \"path\": \"<Keyboard>/j\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Rotate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"b30bd61f-0c29-4fcc-b7b2-a4e7835f1c6b\",\n                    \"path\": \"<Keyboard>/k\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Rotate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"dc2ab3af-233f-4a37-95f4-cfe273aafc92\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 1\",\n                    \"action\": \"Shoot\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9ea94dd4-65d8-47b8-9b2e-1df91eae67cf\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 2\",\n                    \"action\": \"Shoot\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"cc2d4390-c173-4cbb-912f-01122dd62b72\",\n                    \"path\": \"<Keyboard>/space\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Shoot\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f241d2e7-81e8-44ee-845b-6c844f183cf2\",\n                    \"path\": \"<Gamepad>/buttonNorth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 1\",\n                    \"action\": \"Amp\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"5dfc7e20-29aa-4674-b753-6651aa30c4ae\",\n                    \"path\": \"<Gamepad>/buttonNorth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 2\",\n                    \"action\": \"Amp\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"383f7abf-646a-46ec-b81c-19fbd7d38193\",\n                    \"path\": \"<Keyboard>/q\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Amp\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7bc44128-0b1c-47ad-95cb-89575e4cd16e\",\n                    \"path\": \"<Gamepad>/rightShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 1\",\n                    \"action\": \"AmpSpeaker\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"2fd45210-7dee-4c77-85a1-dabdf4ffeb62\",\n                    \"path\": \"<Gamepad>/rightShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 2\",\n                    \"action\": \"AmpSpeaker\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3125c252-324b-4599-9736-21f13eabe87e\",\n                    \"path\": \"<Keyboard>/e\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"AmpSpeaker\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"bfe5428f-8c65-4087-845a-2d108585bcc1\",\n                    \"path\": \"<Gamepad>/start\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 1\",\n                    \"action\": \"RestartGame\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"eb49a980-c968-4c1b-ae1e-2bfb962a5d09\",\n                    \"path\": \"<Gamepad>/start\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 2\",\n                    \"action\": \"RestartGame\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"4af189f4-ab48-4681-b9f9-b6660472161d\",\n                    \"path\": \"<Keyboard>/r\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"RestartGame\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f47ea333-eea9-4257-a12d-db25c82be37e\",\n                    \"path\": \"<Gamepad>/leftTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 1\",\n                    \"action\": \"Intake\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"a2b35f5c-f857-4c81-a09d-ae68acd19b85\",\n                    \"path\": \"<Gamepad>/leftTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 2\",\n                    \"action\": \"Intake\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"0ea4dfd5-66ae-4ef0-b039-c71ff2c4f85f\",\n                    \"path\": \"<Keyboard>/shift\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Intake\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"d4637fa5-ab88-4905-93c6-1c034a9361c0\",\n                    \"path\": \"<Gamepad>/select\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 1\",\n                    \"action\": \"Menu\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9c699e8b-9341-421d-b1e5-f7111bc5df2f\",\n                    \"path\": \"<Gamepad>/select\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 2\",\n                    \"action\": \"Menu\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"cc992416-6326-4bc2-8b68-296926bd52a7\",\n                    \"path\": \"<Keyboard>/m\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"Menu\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"791e1592-261f-4b17-8cf8-7759295ae96a\",\n                    \"path\": \"<Gamepad>/dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 1\",\n                    \"action\": \"MoveCamera\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9e87e802-b0e9-4cd2-b0db-f257a14d17f0\",\n                    \"path\": \"<Gamepad>/dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 2\",\n                    \"action\": \"MoveCamera\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"MoveCamera\",\n                    \"id\": \"69caadae-d5ed-4b5e-9152-55bdd0ee24b7\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"MoveCamera\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"e0f50e64-4ad1-416f-8a80-f02e6f7474f6\",\n                    \"path\": \"<Keyboard>/upArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"MoveCamera\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"6eafb7e0-65ca-471d-be67-398dedc4b540\",\n                    \"path\": \"<Keyboard>/downArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"MoveCamera\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"b9d193d9-4e0e-4bee-998a-e6f0777b462d\",\n                    \"path\": \"<Keyboard>/leftArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"MoveCamera\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"f0c07745-ed30-454e-bd05-42c57c8ee169\",\n                    \"path\": \"<Keyboard>/rightArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Keyboard\",\n                    \"action\": \"MoveCamera\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"35e880b0-9e50-416f-8465-0a3310693edc\",\n                    \"path\": \"<Gamepad>/leftShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 2\",\n                    \"action\": \"AlignRobot\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"4a854851-3cfa-4eba-9926-fe8c0183b879\",\n                    \"path\": \"<Gamepad>/leftShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad 2\",\n                    \"action\": \"Climb\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        }\n    ],\n    \"controlSchemes\": [\n        {\n            \"name\": \"Gamepad 1\",\n            \"bindingGroup\": \"Gamepad 1\",\n            \"devices\": [\n                {\n                    \"devicePath\": \"<Gamepad>\",\n                    \"isOptional\": true,\n                    \"isOR\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"Gamepad 2\",\n            \"bindingGroup\": \"Gamepad 2\",\n            \"devices\": [\n                {\n                    \"devicePath\": \"<Gamepad>\",\n                    \"isOptional\": false,\n                    \"isOR\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"Keyboard\",\n            \"bindingGroup\": \"Keyboard\",\n            \"devices\": [\n                {\n                    \"devicePath\": \"<Keyboard>\",\n                    \"isOptional\": true,\n                    \"isOR\": false\n                }\n            ]\n        }\n    ]\n}");
		m_RobotControls = asset.FindActionMap("RobotControls", throwIfNotFound: true);
		m_RobotControls_Translate = m_RobotControls.FindAction("Translate", throwIfNotFound: true);
		m_RobotControls_Rotate = m_RobotControls.FindAction("Rotate", throwIfNotFound: true);
		m_RobotControls_Shoot = m_RobotControls.FindAction("Shoot", throwIfNotFound: true);
		m_RobotControls_Amp = m_RobotControls.FindAction("Amp", throwIfNotFound: true);
		m_RobotControls_AmpSpeaker = m_RobotControls.FindAction("AmpSpeaker", throwIfNotFound: true);
		m_RobotControls_RestartGame = m_RobotControls.FindAction("RestartGame", throwIfNotFound: true);
		m_RobotControls_Intake = m_RobotControls.FindAction("Intake", throwIfNotFound: true);
		m_RobotControls_Menu = m_RobotControls.FindAction("Menu", throwIfNotFound: true);
		m_RobotControls_MoveCamera = m_RobotControls.FindAction("MoveCamera", throwIfNotFound: true);
		m_RobotControls_AlignRobot = m_RobotControls.FindAction("AlignRobot", throwIfNotFound: true);
		m_RobotControls_Climb = m_RobotControls.FindAction("Climb", throwIfNotFound: true);
	}

	public void Dispose()
	{
		UnityEngine.Object.Destroy(asset);
	}

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

	public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
	{
		return asset.FindAction(actionNameOrId, throwIfNotFound);
	}

	public int FindBinding(InputBinding bindingMask, out InputAction action)
	{
		return asset.FindBinding(bindingMask, out action);
	}
}
