using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SwerveDriveController : MonoBehaviour
{
	public AudioSource redCountDown;

	public AudioSource blueCountDown;

	public AudioSource robotPlayer;

	public AudioResource intakeSound;

	public float moveSpeed = 20f;

	public float rotationSpeed = 15f;

	public bool isRedRobot;

	public bool areRobotsTouching;

	public bool startingReversed;

	public bool is930;

	public static bool canBlueRotate;

	public static bool canRedRotate;

	public static bool isTouchingWallColliderRed;

	public bool redTouchingWall;

	public static bool isTouchingWallColliderBlue;

	public bool blueTouchingWall;

	public static Vector3 velocity;

	public static bool robotsTouching;

	public static bool isPinningRed;

	public static bool isPinningBlue;

	public static bool isAmped;

	public static bool isRedAmped;

	public static bool isRedIntaking;

	public static bool isBlueIntaking;

	private Rigidbody rb;

	private Vector2 translateValue;

	private Vector2 rotateValue;

	private Vector3 startingDirection;

	private Vector3 startingRotation;

	private float intakeValue;

	private bool doAmpSpeaker;

	[SerializeField]
	private Amp allianceAmp;

	private void Start()
	{
		moveSpeed -= moveSpeed * (PlayerPrefs.GetFloat("movespeed") / 100f);
		rotationSpeed -= rotationSpeed * (PlayerPrefs.GetFloat("rotatespeed") / 100f);
		canBlueRotate = true;
		canRedRotate = true;
		isTouchingWallColliderRed = false;
		isTouchingWallColliderBlue = false;
		isPinningRed = false;
		isPinningBlue = false;
		robotsTouching = false;
		velocity = new Vector3(0f, 0f, 0f);
		isAmped = false;
		isRedAmped = false;
		isRedIntaking = false;
		isBlueIntaking = false;
		rb = GetComponent<Rigidbody>();
		if (!startingReversed)
		{
			startingDirection = base.gameObject.transform.forward;
			startingRotation = base.gameObject.transform.right;
		}
		else
		{
			startingDirection = -base.gameObject.transform.forward;
			startingRotation = -base.gameObject.transform.right;
		}
	}

	private void Update()
	{
		redTouchingWall = isTouchingWallColliderRed;
		blueTouchingWall = isTouchingWallColliderBlue;
		areRobotsTouching = robotsTouching;
		if (!isRedRobot)
		{
			if (robotsTouching && isTouchingWallColliderBlue)
			{
				isPinningBlue = true;
			}
			else
			{
				isPinningBlue = false;
			}
		}
		else if (robotsTouching && isTouchingWallColliderRed)
		{
			isPinningRed = true;
		}
		else
		{
			isPinningRed = false;
		}
		if (!isRedRobot)
		{
			if (doAmpSpeaker && allianceAmp.numOfStoredRings >= 2 && GameTimer.timer < 135f && GameTimer.timer > 0f)
			{
				blueCountDown.Play();
				isAmped = true;
				AmplifySpeaker();
				allianceAmp.numOfStoredRings = 0;
			}
		}
		else if (doAmpSpeaker && allianceAmp.numOfStoredRings >= 2 && GameTimer.timer < 135f && GameTimer.timer > 0f)
		{
			redCountDown.Play();
			isRedAmped = true;
			AmplifySpeaker();
			allianceAmp.numOfStoredRings = 0;
		}
		if (intakeValue > 0f && GameTimer.canRobotMove)
		{
			robotPlayer.resource = intakeSound;
			if (!isRedRobot)
			{
				isBlueIntaking = true;
			}
			else
			{
				isRedIntaking = true;
			}
		}
		else if (!isRedRobot)
		{
			isBlueIntaking = false;
		}
		else
		{
			isRedIntaking = false;
		}
		if (!isRedRobot)
		{
			if (isBlueIntaking && !robotPlayer.isPlaying)
			{
				robotPlayer.Play();
			}
			else if (!isBlueIntaking && robotPlayer.isPlaying)
			{
				robotPlayer.Stop();
			}
		}
		else if (isRedIntaking && !robotPlayer.isPlaying)
		{
			robotPlayer.Play();
		}
		else if (!isRedIntaking && robotPlayer.isPlaying)
		{
			robotPlayer.Stop();
		}
		if (isRedRobot && !isRedAmped && GameTimer.timer < 135f)
		{
			Speaker.noteScoredWorthRed = 2f;
			StopCoroutine(StartTimer());
			if (redCountDown.isPlaying)
			{
				redCountDown.Stop();
			}
		}
		else if (!isRedRobot && !isAmped && GameTimer.timer < 135f)
		{
			Speaker.noteScoredWorthBlue = 2f;
			StopCoroutine(StartTimer());
			if (blueCountDown.isPlaying)
			{
				blueCountDown.Stop();
			}
		}
	}

	private void FixedUpdate()
	{
		if (GameTimer.canRobotMove)
		{
			Vector3 vector = startingDirection * translateValue.y + startingRotation * translateValue.x;
			Vector3 torque = new Vector3(0f, rotateValue.x * rotationSpeed, 0f);
			rb.AddForce(vector * moveSpeed);
			if ((isRedRobot && canRedRotate) || (!isRedRobot && canBlueRotate))
			{
				rb.AddTorque(torque);
			}
			velocity = rb.velocity;
		}
	}

	private void AmplifySpeaker()
	{
		StartCoroutine(StartTimer());
	}

	public void StopAmplifiedSpeaker()
	{
		StopCoroutine(StartTimer());
		if (isRedRobot)
		{
			isRedAmped = false;
		}
		else
		{
			isAmped = false;
		}
	}

	private IEnumerator StartTimer()
	{
		if (!isRedRobot)
		{
			Speaker.noteScoredWorthBlue = 5f;
			allianceAmp.noteScoredWorth = 2;
		}
		else
		{
			Speaker.noteScoredWorthRed = 5f;
			allianceAmp.noteScoredWorth = 2;
		}
		yield return new WaitForSeconds(10f);
		if (!isRedRobot)
		{
			Speaker.numOfNotesScoredDuringAmpPeriodBlue = 0f;
			Speaker.noteScoredWorthBlue = 2f;
			allianceAmp.noteScoredWorth = 1;
			isAmped = false;
		}
		else
		{
			Speaker.numOfNotesScoredDuringAmpPeriodRed = 0f;
			Speaker.noteScoredWorthRed = 2f;
			allianceAmp.noteScoredWorth = 1;
			isRedAmped = false;
		}
	}

	public void OnTranslate(InputAction.CallbackContext ctx)
	{
		translateValue = ctx.ReadValue<Vector2>();
	}

	public void OnRotate(InputAction.CallbackContext ctx)
	{
		rotateValue = ctx.ReadValue<Vector2>();
	}

	public void OnIntake(InputAction.CallbackContext ctx)
	{
		intakeValue = ctx.ReadValue<float>();
	}

	public void OnAmpSpeaker(InputAction.CallbackContext ctx)
	{
		doAmpSpeaker = ctx.action.triggered;
	}

	public void OnRestart(InputAction.CallbackContext ctx)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isRedRobot)
		{
			if (other.gameObject.CompareTag("RedPlayer"))
			{
				robotsTouching = true;
			}
			else if (other.gameObject.CompareTag("Field") || other.gameObject.CompareTag("Wall"))
			{
				isTouchingWallColliderBlue = true;
			}
		}
		else if (other.gameObject.CompareTag("Player"))
		{
			robotsTouching = true;
		}
		else if (other.gameObject.CompareTag("Field") || other.gameObject.CompareTag("Wall"))
		{
			isTouchingWallColliderRed = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!isRedRobot)
		{
			if (other.gameObject.CompareTag("RedPlayer"))
			{
				robotsTouching = false;
			}
			else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Field"))
			{
				if (!isRedRobot)
				{
					isTouchingWallColliderBlue = false;
				}
				else
				{
					isTouchingWallColliderRed = false;
				}
			}
		}
		else if (other.gameObject.CompareTag("Player"))
		{
			robotsTouching = false;
		}
		else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Field"))
		{
			if (!isRedRobot)
			{
				isTouchingWallColliderBlue = false;
			}
			else
			{
				isTouchingWallColliderRed = false;
			}
		}
	}
}
