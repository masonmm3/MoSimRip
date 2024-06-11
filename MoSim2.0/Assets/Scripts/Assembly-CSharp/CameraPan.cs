using UnityEngine;

public class CameraPan : MonoBehaviour
{
	public Transform[] targets;

	public float smoothSpeed = 0.125f;

	public Vector2 rotationConstraints = new Vector2(-90f, 90f);

	private void Start()
	{
	}

	private void LateUpdate()
	{
		Transform enabledTarget = GetEnabledTarget();
		if (enabledTarget != null)
		{
			Quaternion b = Quaternion.LookRotation(enabledTarget.position - base.transform.position, Vector3.up);
			b.eulerAngles = new Vector3(Mathf.Clamp(b.eulerAngles.x, rotationConstraints.x, rotationConstraints.y), b.eulerAngles.y, 0f);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, smoothSpeed * Time.deltaTime);
		}
	}

	private Transform GetEnabledTarget()
	{
		Transform[] array = targets;
		foreach (Transform transform in array)
		{
			if (transform.gameObject.activeSelf)
			{
				return transform;
			}
		}
		Debug.LogError("No enabled targets found!");
		return null;
	}
}
