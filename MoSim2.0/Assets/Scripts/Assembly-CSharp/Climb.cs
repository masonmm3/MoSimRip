using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Climb : MonoBehaviour
{
	public GameObject rightFirstStagePivot;

	public GameObject rightSecondStagePivot;

	public GameObject leftFirstStagePivot;

	public GameObject leftSecondStagePivot;

	private bool climb;

	private bool isClimbing;

	private bool climbed;

	public float rotationSpeed = 1f;

	private void Update()
	{
		if (GameTimer.timer > 0f)
		{
			if (climb && !isClimbing && !climbed)
			{
				isClimbing = true;
				StartCoroutine(RotateTowardsClimb(rightFirstStagePivot, up: false));
				StartCoroutine(RotateTowardsClimb(rightSecondStagePivot, up: true));
				StartCoroutine(RotateTowardsClimb(leftFirstStagePivot, up: false));
				StartCoroutine(RotateTowardsClimb(leftSecondStagePivot, up: true));
			}
			else if (climbed && climb && !isClimbing)
			{
				isClimbing = true;
				StartCoroutine(RotateTowardsHome(rightFirstStagePivot, up: false));
				StartCoroutine(RotateTowardsHome(rightSecondStagePivot, up: true));
				StartCoroutine(RotateTowardsHome(leftFirstStagePivot, up: false));
				StartCoroutine(RotateTowardsHome(leftSecondStagePivot, up: true));
			}
		}
	}

	private IEnumerator RotateTowardsClimb(GameObject pivot, bool up)
	{
		Quaternion startRotation = pivot.transform.localRotation;
		Quaternion endRotation = (up ? (startRotation * Quaternion.Euler(-90f, 0f, 0f)) : (startRotation * Quaternion.Euler(-180f, 0f, 0f)));
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * rotationSpeed;
			pivot.transform.localRotation = Quaternion.Slerp(startRotation, endRotation, t);
			yield return null;
		}
		isClimbing = false;
		climbed = true;
	}

	private IEnumerator RotateTowardsHome(GameObject pivot, bool up)
	{
		Quaternion startRotation = pivot.transform.localRotation;
		Quaternion endRotation = (up ? (startRotation * Quaternion.Euler(90f, 0f, 0f)) : (startRotation * Quaternion.Euler(180f, 0f, 0f)));
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * rotationSpeed;
			pivot.transform.localRotation = Quaternion.Slerp(startRotation, endRotation, t);
			yield return null;
		}
		isClimbing = false;
		climbed = false;
	}

	public void OnClimb(InputAction.CallbackContext ctx)
	{
		climb = ctx.action.triggered;
	}
}
