using System.Collections;
using UnityEngine;

public class RedTurretAlign : MonoBehaviour
{
	public Transform target;

	public GameObject shooterPivot;

	public GameObject turret;

	public float shooterSpeed = 20f;

	public float turretSpeed = 20f;

	public float minShooterAngle = -90f;

	public float maxShooterAngle = 90f;

	public float maxAimDistance = 10f;

	private Quaternion turretLocalStartingRot;

	private Quaternion pivotLocalStartingRot;

	private Coroutine stowCoroutine;

	private Coroutine unstowCoroutine;

	public bool stowedShooter;

	private void Start()
	{
		turretLocalStartingRot = turret.transform.localRotation;
		pivotLocalStartingRot = shooterPivot.transform.localRotation;
	}

	private void Update()
	{
		float num = Vector3.Distance(shooterPivot.transform.position, target.position);
		if (num <= maxAimDistance && !SwerveDriveController.isRedIntaking && !stowedShooter)
		{
			RotateTurret();
			stowedShooter = false;
		}
		else if (!stowedShooter && stowCoroutine == null)
		{
			stowCoroutine = StartCoroutine(StowTurret());
		}
		if (stowedShooter && num <= maxAimDistance && unstowCoroutine == null)
		{
			unstowCoroutine = StartCoroutine(UnstowTurret());
		}
	}

	private void RotateTurret()
	{
		Vector3 forward = target.position - shooterPivot.transform.position;
		float y = Mathf.Atan2(forward.x, forward.z) * 57.29578f + 180f;
		float maxDegreesDelta = shooterSpeed * Time.deltaTime;
		float maxDegreesDelta2 = turretSpeed * Time.deltaTime;
		Quaternion to = Quaternion.LookRotation(forward, Vector3.up);
		to *= Quaternion.Euler(0f, 180f, 0f);
		shooterPivot.transform.rotation = Quaternion.RotateTowards(shooterPivot.transform.rotation, to, maxDegreesDelta);
		Quaternion to2 = Quaternion.Euler(0f, y, 0f);
		turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, to2, maxDegreesDelta2);
	}

	private IEnumerator StowTurret()
	{
		float duration = 0.2f;
		float elapsedTime = 0f;
		Quaternion startTurretLocalRotation = turret.transform.localRotation;
		Quaternion startPivotLocalRotation = shooterPivot.transform.localRotation;
		while (elapsedTime < duration)
		{
			float t = elapsedTime / duration;
			turret.transform.localRotation = Quaternion.Slerp(startTurretLocalRotation, turretLocalStartingRot, t);
			shooterPivot.transform.localRotation = Quaternion.Slerp(startPivotLocalRotation, pivotLocalStartingRot, t);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		turret.transform.localRotation = turretLocalStartingRot;
		shooterPivot.transform.localRotation = pivotLocalStartingRot;
		stowCoroutine = null;
		stowedShooter = true;
	}

	private IEnumerator UnstowTurret()
	{
		stowedShooter = false;
		float duration = 0.2f;
		float elapsedTime2 = 0f;
		turretSpeed = 0f;
		shooterSpeed = 0f;
		while (elapsedTime2 < duration)
		{
			turretSpeed += 50f;
			elapsedTime2 += Time.deltaTime;
			yield return null;
		}
		elapsedTime2 = 0f;
		while (elapsedTime2 < duration)
		{
			shooterSpeed += 50f;
			elapsedTime2 += Time.deltaTime;
			yield return null;
		}
		turretSpeed = 2000f;
		shooterSpeed = 600f;
		unstowCoroutine = null;
	}
}
