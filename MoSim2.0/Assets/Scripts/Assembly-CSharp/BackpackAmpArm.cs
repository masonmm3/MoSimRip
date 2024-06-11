using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class BackpackAmpArm : MonoBehaviour
{
	[SerializeField]
	private Transform ampStage;

	[SerializeField]
	private float ampMoveSpeed;

	[SerializeField]
	private float stowMoveSpeed;

	[SerializeField]
	private float ampAmount;

	private Vector3 originalPosition;

	private bool isMoving;

	private bool isStowed = true;

	[SerializeField]
	private DetectRingCollisions collisions;

	[SerializeField]
	private AudioSource source;

	[SerializeField]
	private AudioResource stall;

	[SerializeField]
	private GameObject hiddenRing;

	private void Start()
	{
		originalPosition = ampStage.localPosition;
	}

	private void Update()
	{
		if (collisions.isAmping && !isMoving)
		{
			hiddenRing.SetActive(value: true);
			StartCoroutine(AmpArm());
		}
		else if (!collisions.isAmping && !isMoving && !isStowed)
		{
			StartCoroutine(StowAmpArm());
		}
	}

	private IEnumerator AmpArm()
	{
		isMoving = true;
		isStowed = false;
		Vector3 vector = Quaternion.Euler(0f - -19.767f, 0f, 0f) * Vector3.up;
		Vector3 startPosition = ampStage.localPosition;
		Vector3 targetPosition = startPosition + vector * ampAmount;
		float num = Vector3.Distance(startPosition, targetPosition);
		float timeToMove = num / ampMoveSpeed;
		float elapsedTime = 0f;
		while (elapsedTime < timeToMove)
		{
			float t = elapsedTime / timeToMove;
			ampStage.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		ampStage.localPosition = targetPosition;
		source.resource = stall;
		source.Play();
		hiddenRing.SetActive(value: false);
		yield return new WaitForSeconds(0.98f);
		isMoving = false;
	}

	private IEnumerator StowAmpArm()
	{
		isMoving = true;
		Vector3 localDown = -ampStage.up;
		Vector3 targetPosition = originalPosition;
		float num = Vector3.Distance(ampStage.localPosition, targetPosition);
		float timeToMove = num / stowMoveSpeed;
		float elapsedTime = 0f;
		while (elapsedTime < timeToMove)
		{
			Vector3 normalized = localDown.normalized;
			ampStage.localPosition += Vector3.Project(normalized, targetPosition - ampStage.localPosition) * stowMoveSpeed * Time.deltaTime;
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		ampStage.localPosition = originalPosition;
		isMoving = false;
		isStowed = true;
	}
}
