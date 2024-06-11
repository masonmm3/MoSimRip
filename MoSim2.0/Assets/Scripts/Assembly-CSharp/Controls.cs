using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Controls : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
{
	public struct RobotControlsActions
	{
		private Controls m_Wrapper;

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

		public RobotControlsActions(Controls wrapper)
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

	private int m_GamepadKeyboardSchemeIndex = -1;

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

	public InputControlScheme GamepadKeyboardScheme
	{
		get
		{
			if (m_GamepadKeyboardSchemeIndex == -1)
			{
				m_GamepadKeyboardSchemeIndex = asset.FindControlSchemeIndex("Gamepad/Keyboard");
			}
			return asset.controlSchemes[m_GamepadKeyboardSchemeIndex];
		}
	}

	public Controls()
	{
		asset = InputActionAsset.FromJson("{\n    \"name\": \"Controls\",\n    \"maps\": [\n        {\n            \"name\": \"RobotControls\",\n            \"id\": \"89a7e69c-f53b-4f6c-8858-8cc9ef3387ad\",\n            \"actions\": [\n                {\n                    \"name\": \"Translate\",\n                    \"type\": \"Value\",\n                    \"id\": \"f1728e11-e8a3-44e1-afa8-dc82bfa2b09c\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Rotate\",\n                    \"type\": \"Value\",\n                    \"id\": \"c1a011bd-4a6c-43e8-9628-9f67957f5bd6\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Shoot\",\n                    \"type\": \"Button\",\n                    \"id\": \"366e5f2a-50aa-4e72-9970-81185afa77ed\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Amp\",\n                    \"type\": \"Button\",\n                    \"id\": \"c6a5ad7d-fe75-4a29-99de-247f4ccb82a4\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"AmpSpeaker\",\n                    \"type\": \"Button\",\n                    \"id\": \"5bb25259-bb95-4bbb-acb9-b2858aeaca3f\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"RestartGame\",\n                    \"type\": \"Button\",\n                    \"id\": \"d8a775e3-36d2-407c-bda3-b40c9aef891f\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Intake\",\n                    \"type\": \"Button\",\n                    \"id\": \"a9b12e82-ded3-41f0-98ed-4abebfeb9858\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"Hold\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Menu\",\n                    \"type\": \"Button\",\n                    \"id\": \"5d2dc4e2-ab57-42cf-a74d-61aabd314d36\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"MoveCamera\",\n                    \"type\": \"Value\",\n                    \"id\": \"5df2fb3f-75ff-4f84-9b9e-e02231ca2068\",\n                    \"expectedControlType\": \"Dpad\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"AlignRobot\",\n                    \"type\": \"Button\",\n                    \"id\": \"90202e6a-1825-4230-9a97-f394ff7c541c\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Climb\",\n                    \"type\": \"Button\",\n                    \"id\": \"aae796ef-4906-4427-8b2d-d633ff769f3f\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"0a0ab686-a95e-408e-bdf1-1c03a7b38753\",\n                    \"path\": \"<Gamepad>/leftStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Translate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"2D Vector\",\n                    \"id\": \"2fbd881d-31dc-414b-9e9d-5d39e0bbe600\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Translate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"WASD [Keyboard]\",\n                    \"id\": \"70b544ef-70a1-44b2-827e-6ad86ecd6bc9\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Translate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"6dee444c-1ba6-4196-98aa-15e4812dacd7\",\n                    \"path\": \"<Keyboard>/w\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Translate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"7d8c43ea-2083-428a-83c7-e2d1f7f9f06f\",\n                    \"path\": \"<Keyboard>/s\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Translate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"e87b236d-7176-4311-b90f-fdb104b1046d\",\n                    \"path\": \"<Keyboard>/a\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Translate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"cb92fe9a-a4d2-4e1d-ba5e-ecbf3066f227\",\n                    \"path\": \"<Keyboard>/d\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Translate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f1b143fb-3c9f-490a-a07a-b42f6ae42bf4\",\n                    \"path\": \"<Gamepad>/rightStick\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Rotate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"Rotate\",\n                    \"id\": \"91b6ec23-4ea6-44ea-aec8-0c74f90a7b80\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Rotate\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"2701e04a-4b0f-493a-b5c0-dac93f7210b1\",\n                    \"path\": \"<Keyboard>/j\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Rotate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"b458d0fa-285c-4dfa-84c1-60220fdd0b7a\",\n                    \"path\": \"<Keyboard>/k\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Rotate\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"dc2ab3af-233f-4a37-95f4-cfe273aafc92\",\n                    \"path\": \"<Gamepad>/rightTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Shoot\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"cd4eefe6-b9b5-42c9-baa4-278f26cc7a71\",\n                    \"path\": \"<Keyboard>/space\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Shoot\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f241d2e7-81e8-44ee-845b-6c844f183cf2\",\n                    \"path\": \"<Gamepad>/buttonNorth\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Amp\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7efeffcf-1004-4280-9c9d-668a8bd573da\",\n                    \"path\": \"<Keyboard>/q\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Amp\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7bc44128-0b1c-47ad-95cb-89575e4cd16e\",\n                    \"path\": \"<Gamepad>/rightShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"AmpSpeaker\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"a26093cb-efa6-49d0-a6de-f31bca5aaeae\",\n                    \"path\": \"<Keyboard>/e\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"AmpSpeaker\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"bfe5428f-8c65-4087-845a-2d108585bcc1\",\n                    \"path\": \"<Gamepad>/start\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"RestartGame\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"4b247262-7361-4517-9302-8aa1d00cc4d7\",\n                    \"path\": \"<Keyboard>/r\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"RestartGame\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f47ea333-eea9-4257-a12d-db25c82be37e\",\n                    \"path\": \"<Gamepad>/leftTrigger\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Intake\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"f893db64-49b9-4f3e-8322-bcf5cc6af2a3\",\n                    \"path\": \"<Keyboard>/shift\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Intake\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"d4637fa5-ab88-4905-93c6-1c034a9361c0\",\n                    \"path\": \"<Gamepad>/select\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Menu\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1bba04c3-b371-4748-8e82-41a5c816516b\",\n                    \"path\": \"<Keyboard>/m\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Menu\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"791e1592-261f-4b17-8cf8-7759295ae96a\",\n                    \"path\": \"<Gamepad>/dpad\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"MoveCamera\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"2D Vector\",\n                    \"id\": \"01c0d6a1-6a95-4279-b766-16de82af644c\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"MoveCamera\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"MoveCamera [Keyboard]\",\n                    \"id\": \"c0f893f0-b3ef-441e-965e-9a4212980cf6\",\n                    \"path\": \"2DVector\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"MoveCamera\",\n                    \"isComposite\": true,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"up\",\n                    \"id\": \"12c6d0ee-22be-432f-9275-46291098483f\",\n                    \"path\": \"<Keyboard>/upArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"MoveCamera\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"down\",\n                    \"id\": \"f80bb10c-fa2f-4f15-920b-dbb4386389bc\",\n                    \"path\": \"<Keyboard>/downArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"MoveCamera\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"left\",\n                    \"id\": \"72613016-77d8-4ba6-af70-460bf471f106\",\n                    \"path\": \"<Keyboard>/leftArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"MoveCamera\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"right\",\n                    \"id\": \"26dbeaa1-5601-4bf1-a063-30f425adc4b9\",\n                    \"path\": \"<Keyboard>/rightArrow\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"MoveCamera\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": true\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"d1668e35-8b84-4258-b397-619618a41132\",\n                    \"path\": \"<Gamepad>/leftShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"AlignRobot\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"118ad971-88e2-4f4a-814e-6d4c6bd31379\",\n                    \"path\": \"<Gamepad>/leftShoulder\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"Gamepad/Keyboard\",\n                    \"action\": \"Climb\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        }\n    ],\n    \"controlSchemes\": [\n        {\n            \"name\": \"Gamepad/Keyboard\",\n            \"bindingGroup\": \"Gamepad/Keyboard\",\n            \"devices\": [\n                {\n                    \"devicePath\": \"<Gamepad>\",\n                    \"isOptional\": false,\n                    \"isOR\": false\n                },\n                {\n                    \"devicePath\": \"<Keyboard>\",\n                    \"isOptional\": false,\n                    \"isOR\": false\n                }\n            ]\n        }\n    ]\n}");
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
