using System.Collections;
using UnityEngine;

public class BotFollowSwerve : MonoBehaviour
{
	public Transform target;

	public float acceleration = 30f;

	public float maxSpeed = 50f;

	public float rotationSpeed = 50f;

	public float pinningTimeLimit = 2f;

	public float moveAwayDuration = 0.1f;

	public float targetVelocityThreshold = 0.1f;

	public GameObject enemyDockLocation;

	private Rigidbody rb;

	private bool isPinning;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (GameTimer.timer < 135f)
		{
			Vector3 normalized = (target.position - base.transform.position).normalized;
			if (rb.velocity.magnitude < maxSpeed)
			{
				rb.AddForce(normalized * acceleration, ForceMode.Acceleration);
			}
			Debug.DrawLine(target.position, rb.position, Color.green);
			if (isPinning)
			{
				StartCoroutine(PinningTimer());
				isPinning = false;
			}
			if (target.GetComponent<Rigidbody>().velocity.magnitude > 0.1f)
			{
				Roll(0f - target.GetComponent<Rigidbody>().angularVelocity.y);
			}
		}
	}

	private IEnumerator MoveAwayCoroutine()
	{
		Vector3 normalized = (enemyDockLocation.transform.position - base.transform.position).normalized;
		rb.AddForce(normalized * acceleration * 1.5f, ForceMode.Acceleration);
		yield return new WaitForSeconds(moveAwayDuration);
	}

	private IEnumerator PinningTimer()
	{
		yield return new WaitForSeconds(pinningTimeLimit);
		StartCoroutine(MoveAwayCoroutine());
	}

	private void Roll(float targetAngularVelocity)
	{
		rb.angularVelocity = base.transform.up * targetAngularVelocity * rotationSpeed;
	}

	private void OnCollisionStay(Collision collision)
	{
		if (collision.transform == target && target.GetComponent<Rigidbody>().velocity.magnitude <= targetVelocityThreshold)
		{
			isPinning = true;
		}
	}
}
