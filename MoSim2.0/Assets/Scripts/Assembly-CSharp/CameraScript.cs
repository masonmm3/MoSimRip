using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public Transform[] targets;

	private Vector3 offset;

	private Transform target;

	private void Start()
	{
		target = GetEnabledTarget();
		offset = base.transform.position - target.position;
	}

	private void LateUpdate()
	{
		if (target != null)
		{
			base.transform.position = target.position + offset;
			base.transform.rotation = Quaternion.LookRotation(-offset.normalized, Vector3.up);
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
