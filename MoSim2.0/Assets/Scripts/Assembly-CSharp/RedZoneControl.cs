using UnityEngine;

public class RedZoneControl : MonoBehaviour
{
	public BoxCollider blueZone;

	public GameObject[] redRobots;

	public static bool redRobotInBlueZone;

	public static bool redRobotInBlueZoneUpdated;

	private void Start()
	{
		redRobotInBlueZone = false;
	}

	private void Update()
	{
		GameObject[] array = redRobots;
		int num = 0;
		if (num < array.Length)
		{
			GameObject gameObject = array[num];
			if (gameObject.activeSelf && blueZone.bounds.Intersects(gameObject.GetComponent<Collider>().bounds))
			{
				redRobotInBlueZoneUpdated = true;
			}
			else
			{
				redRobotInBlueZoneUpdated = false;
			}
		}
	}

	public void CheckZoneCollisions()
	{
		redRobotInBlueZone = false;
		GameObject[] array = redRobots;
		foreach (GameObject gameObject in array)
		{
			if (gameObject.activeSelf && blueZone.bounds.Intersects(gameObject.GetComponent<Collider>().bounds))
			{
				redRobotInBlueZone = true;
				break;
			}
		}
	}
}
