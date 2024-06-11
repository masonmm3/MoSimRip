using UnityEngine;

public class BlueZoneControl : MonoBehaviour
{
	public BoxCollider redZone;

	public GameObject[] blueRobots;

	public static bool blueRobotInRedZone;

	public static bool blueRobotInRedZoneUpdated;

	private void Start()
	{
		blueRobotInRedZone = false;
	}

	private void Update()
	{
		GameObject[] array = blueRobots;
		int num = 0;
		if (num < array.Length)
		{
			GameObject gameObject = array[num];
			if (gameObject.activeSelf && redZone.bounds.Intersects(gameObject.GetComponent<Collider>().bounds))
			{
				blueRobotInRedZoneUpdated = true;
			}
			else
			{
				blueRobotInRedZoneUpdated = false;
			}
		}
	}

	public void CheckZoneCollisions()
	{
		blueRobotInRedZone = false;
		GameObject[] array = blueRobots;
		foreach (GameObject gameObject in array)
		{
			if (gameObject.activeSelf && redZone.bounds.Intersects(gameObject.GetComponent<Collider>().bounds))
			{
				blueRobotInRedZone = true;
				break;
			}
		}
	}
}
