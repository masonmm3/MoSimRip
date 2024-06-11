using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class DetectRingCollisions : MonoBehaviour
{
	private GameObject touchedRing;

	public GameObject prefabToInstantiate;

	public Transform shootingSpawnPoint;

	public Transform ampSpawnPoint;

	public float shootingSpeed = 10f;

	public float ampSpeed = 10f;

	public float noteDrag;

	public AudioSource player;

	public AudioResource shootSound;

	public AudioResource spedUpSound;

	public float shootingLatency;

	public float intakeLatency;

	public float ampingLatency;

	public Collider intakeCollider;

	public GameObject hiddenNote;

	public static bool hasRingInRobotRed;

	public static bool hasRingInRobotBlue;

	private bool ringWithinIntakeCollider;

	public bool isShooting;

	public bool isAmping;

	private float shootValue;

	private bool ampValue;

	public bool isRedRobot;

	public BlueZoneControl blueZone;

	public RedZoneControl redZone;

	private bool canShoot = true;

	private void Start()
	{
		hiddenNote.SetActive(value: true);
		hasRingInRobotRed = true;
		hasRingInRobotBlue = true;
	}

	private void Update()
	{
		if (!GameTimer.canRobotMove || isShooting || isAmping)
		{
			return;
		}
		if (!isRedRobot)
		{
			if (shootValue > 0f && hasRingInRobotBlue && !isAmping)
			{
				if (!isShooting && canShoot)
				{
					isShooting = true;
					StartCoroutine(Shooting());
				}
			}
			else if (ampValue && hasRingInRobotBlue && !isAmping && !isShooting)
			{
				isAmping = true;
				StartCoroutine(Amping());
			}
			if (ringWithinIntakeCollider && !hasRingInRobotBlue && SwerveDriveController.isBlueIntaking)
			{
				Object.Destroy(touchedRing.gameObject);
				hiddenNote.SetActive(value: true);
				hasRingInRobotBlue = true;
				LedStripController.blueFlash = true;
				ringWithinIntakeCollider = false;
				StartCoroutine(CantShootWhenRunning());
			}
			return;
		}
		if (shootValue > 0f && hasRingInRobotRed && !isAmping)
		{
			if (!isShooting && canShoot)
			{
				isShooting = true;
				StartCoroutine(Shooting());
			}
		}
		else if (ampValue && hasRingInRobotRed && !isAmping && !isShooting)
		{
			isAmping = true;
			StartCoroutine(Amping());
		}
		if (ringWithinIntakeCollider && !hasRingInRobotRed && SwerveDriveController.isRedIntaking)
		{
			Object.Destroy(touchedRing.gameObject);
			hiddenNote.SetActive(value: true);
			hasRingInRobotRed = true;
			LedStripController.redFlash = true;
			ringWithinIntakeCollider = false;
			StartCoroutine(CantShootWhenRunning());
		}
	}

	private void ShootRing()
	{
		hiddenNote.SetActive(value: false);
		GameObject gameObject = Object.Instantiate(prefabToInstantiate, shootingSpawnPoint.position, shootingSpawnPoint.rotation);
		Rigidbody component = gameObject.GetComponent<Rigidbody>();
		float num = SwerveDriveController.velocity.magnitude * 0.5f;
		float y = SwerveDriveController.velocity.magnitude * 0.4f;
		Vector3 vector = Quaternion.Euler(0f, y, 0f) * shootingSpawnPoint.forward;
		Vector3 velocity = (shootingSpeed + num) * vector;
		component.drag = noteDrag;
		component.velocity = velocity;
		if (!isRedRobot)
		{
			blueZone.CheckZoneCollisions();
			hasRingInRobotBlue = false;
			gameObject.tag = "noteShotByBlue";
		}
		else
		{
			redZone.CheckZoneCollisions();
			hasRingInRobotRed = false;
			gameObject.tag = "noteShotByRed";
		}
		gameObject.transform.localScale = new Vector3(0.4f, 0.55f, 0.7f);
		gameObject.GetComponent<RingBehaviour>().StartCoroutine(gameObject.GetComponent<RingBehaviour>().UnSquishhhh());
	}

	private void AmpRing()
	{
		hiddenNote.SetActive(value: false);
		GameObject gameObject = Object.Instantiate(prefabToInstantiate, ampSpawnPoint.position, ampSpawnPoint.rotation);
		Rigidbody component = gameObject.GetComponent<Rigidbody>();
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		component.drag = noteDrag;
		component.velocity = velocity + ampSpawnPoint.forward.normalized * ampSpeed;
		if (!isRedRobot)
		{
			hasRingInRobotBlue = false;
		}
		else
		{
			hasRingInRobotRed = false;
		}
		gameObject.transform.localScale = new Vector3(0.45f, 0.45f, 0.65f);
		gameObject.GetComponent<RingBehaviour>().StartCoroutine(gameObject.GetComponent<RingBehaviour>().UnSquishhhh());
	}

	private IEnumerator Shooting()
	{
		player.resource = spedUpSound;
		player.Play();
		yield return new WaitForSeconds(shootingLatency);
		ShootRing();
		isShooting = false;
	}

	private IEnumerator CantShootWhenRunning()
	{
		canShoot = false;
		yield return new WaitForSeconds(intakeLatency);
		canShoot = true;
	}

	private IEnumerator Amping()
	{
		yield return new WaitForSeconds(ampingLatency);
		player.resource = shootSound;
		player.Play();
		AmpRing();
		isAmping = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (intakeCollider.bounds.Intersects(other.bounds) && (other.gameObject.CompareTag("Ring") || other.gameObject.CompareTag("noteShotByRed") || other.gameObject.CompareTag("noteShotByBlue")))
		{
			other.tag = "Ring";
			touchedRing = other.gameObject;
			ringWithinIntakeCollider = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Ring"))
		{
			ringWithinIntakeCollider = false;
		}
	}

	public void OnShoot(InputAction.CallbackContext ctx)
	{
		shootValue = ctx.ReadValue<float>();
	}

	public void OnAmp(InputAction.CallbackContext ctx)
	{
		ampValue = ctx.action.triggered;
	}
}
