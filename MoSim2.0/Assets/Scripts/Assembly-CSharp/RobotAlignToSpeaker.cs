using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotAlignToSpeaker : MonoBehaviour
{
	[SerializeField]
	private AllianceColor alliance;

	public Transform target;

	public GameObject shooterPivot;

	public Rigidbody robotRigidbody;

	public float shooterSpeed = 600f;

	public float rotationSpeed = 50f;

	public float ampRotationSpeed = 100f;

	public float ampDuration;

	public float downwardOffset;

	public float maxAimDistance = 40f;

	private Quaternion pivotLocalStartingRot;

	private Coroutine stowCoroutine;

	private Coroutine unstowCoroutine;

	public bool stowedShooter;

	private float alignWholeRobot;

	private Quaternion targetRotation;

	private bool canDoAlign = true;

	private bool amp;

	private bool pass;

	private bool isPassing;

	private bool isAmping;

	public bool isShooting;

	[SerializeField]
	private bool robotThatRotatesUpForAmping;

	[SerializeField]
	private bool robotHasUniquePassAngle;

	[SerializeField]
	private bool is1678;

	public DetectRingCollisions ringCollisions;

	private void Start()
	{
		ringCollisions = base.gameObject.GetComponent<DetectRingCollisions>();
		pivotLocalStartingRot = shooterPivot.transform.localRotation;
	}

	private void Update()
	{
		if (!GameTimer.canRobotMove)
		{
			return;
		}
		if (!amp && !isAmping && !isPassing)
		{
			float num = Vector3.Distance(shooterPivot.transform.position, target.position);
			if (num <= maxAimDistance && !stowedShooter)
			{
				if (num <= 20f && is1678)
				{
					downwardOffset = 17.6f;
				}
				else if (is1678)
				{
					downwardOffset = 16.5f;
				}
				RotateTurret();
				stowedShooter = false;
			}
			else if (num > maxAimDistance && robotHasUniquePassAngle && pass)
			{
				StartCoroutine(Pass());
				stowedShooter = false;
			}
			else if (num > maxAimDistance && !stowedShooter && stowCoroutine == null)
			{
				stowCoroutine = StartCoroutine(StowTurret());
			}
			if (stowedShooter && num <= maxAimDistance && unstowCoroutine == null)
			{
				unstowCoroutine = StartCoroutine(UnstowTurret());
			}
			if (alignWholeRobot > 0f && canDoAlign && num <= maxAimDistance)
			{
				canDoAlign = false;
				RotateRobotToTarget();
			}
			else if (alignWholeRobot == 0f)
			{
				canDoAlign = true;
			}
		}
		else if (amp && DetectRingCollisions.hasRingInRobotBlue && !isAmping && !ringCollisions.isShooting && robotThatRotatesUpForAmping)
		{
			StartCoroutine(AmplifyRotation());
		}
	}

	private IEnumerator Pass()
	{
		isPassing = true;
		Quaternion startRotation = shooterPivot.transform.localRotation;
		Quaternion targetRotation = Quaternion.Euler(-12f, 0f, 0f);
		float elapsedTime2 = 0f;
		float duration = 0.3f;
		while (elapsedTime2 < duration)
		{
			shooterPivot.transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime2 / duration);
			elapsedTime2 += Time.deltaTime;
			yield return null;
		}
		shooterPivot.transform.localRotation = targetRotation;
		yield return new WaitForSeconds(0.2f);
		elapsedTime2 = 0f;
		while (elapsedTime2 < duration)
		{
			shooterPivot.transform.localRotation = Quaternion.Slerp(targetRotation, startRotation, elapsedTime2 / duration);
			elapsedTime2 += Time.deltaTime;
			yield return null;
		}
		shooterPivot.transform.localRotation = startRotation;
		pass = false;
		isPassing = false;
	}

	private IEnumerator AmplifyRotation()
	{
		isAmping = true;
		Quaternion startRotation = shooterPivot.transform.localRotation;
		Quaternion targetRotation = Quaternion.Euler(-100f, 0f, 0f);
		float elapsedTime2 = 0f;
		float duration = ampDuration;
		while (elapsedTime2 < duration)
		{
			shooterPivot.transform.localRotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime2 / duration);
			elapsedTime2 += Time.deltaTime;
			yield return null;
		}
		shooterPivot.transform.localRotation = targetRotation;
		yield return new WaitForSeconds(0.2f);
		elapsedTime2 = 0f;
		while (elapsedTime2 < duration)
		{
			shooterPivot.transform.localRotation = Quaternion.Slerp(targetRotation, startRotation, elapsedTime2 / duration);
			elapsedTime2 += Time.deltaTime;
			yield return null;
		}
		shooterPivot.transform.localRotation = startRotation;
		amp = false;
		isAmping = false;
	}

	private void RotateRobotToTarget()
	{
		if (!isShooting)
		{
			isShooting = true;
			Vector3 forward = target.position - base.transform.position;
			forward.y = 0f;
			targetRotation = Quaternion.LookRotation(forward, Vector3.up);
			StartCoroutine(RotateTowardsTarget(targetRotation));
		}
	}

	private IEnumerator RotateTowardsTarget(Quaternion targetRotation)
	{
		if (alliance == AllianceColor.Blue)
		{
			SwerveDriveController.canBlueRotate = false;
		}
		else
		{
			SwerveDriveController.canRedRotate = false;
		}
		while (Quaternion.Angle(robotRigidbody.rotation, targetRotation) > 0.1f)
		{
			robotRigidbody.rotation = Quaternion.RotateTowards(robotRigidbody.rotation, targetRotation, rotationSpeed * Time.deltaTime);
			yield return null;
		}
		if (alliance == AllianceColor.Blue)
		{
			SwerveDriveController.canBlueRotate = true;
		}
		else
		{
			SwerveDriveController.canRedRotate = true;
		}
		isShooting = false;
	}

	private void RotateTurret()
	{
		Vector3 vector = target.position - Vector3.up * downwardOffset;
		float maxDegreesDelta = shooterSpeed * Time.deltaTime;
		Vector3 eulerAngles = Quaternion.LookRotation(vector - shooterPivot.transform.position, Vector3.up).eulerAngles;
		eulerAngles.y = shooterPivot.transform.rotation.eulerAngles.y;
		eulerAngles.z = shooterPivot.transform.rotation.eulerAngles.z;
		Quaternion to = Quaternion.Euler(eulerAngles);
		shooterPivot.transform.rotation = Quaternion.RotateTowards(shooterPivot.transform.rotation, to, maxDegreesDelta);
	}

	private IEnumerator StowTurret()
	{
		float duration = 0.2f;
		float elapsedTime = 0f;
		Quaternion startPivotLocalRotation = shooterPivot.transform.localRotation;
		while (elapsedTime < duration)
		{
			float t = elapsedTime / duration;
			shooterPivot.transform.localRotation = Quaternion.Slerp(startPivotLocalRotation, pivotLocalStartingRot, t);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		shooterPivot.transform.localRotation = pivotLocalStartingRot;
		stowCoroutine = null;
		stowedShooter = true;
	}

	private IEnumerator UnstowTurret()
	{
		stowedShooter = false;
		float duration = 0.2f;
		float elapsedTime = 0f;
		shooterSpeed = 0f;
		while (elapsedTime < duration)
		{
			shooterSpeed += 50f;
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		shooterSpeed = 600f;
		unstowCoroutine = null;
	}

	public void OnShoot(InputAction.CallbackContext ctx)
	{
		alignWholeRobot = ctx.ReadValue<float>();
	}

	public void OnPass(InputAction.CallbackContext ctx)
	{
		pass = ctx.action.triggered;
	}

	public void OnAmp(InputAction.CallbackContext ctx)
	{
		amp = ctx.action.triggered;
	}
}
