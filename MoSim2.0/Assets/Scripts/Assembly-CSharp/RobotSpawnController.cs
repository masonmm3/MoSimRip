using UnityEngine;

public class RobotSpawnController : MonoBehaviour
{
	private int gamemode;

	private int cameraMode;

	private int blueRobot;

	private int redRobot;

	[SerializeField]
	private bool multiplayer;

	[SerializeField]
	private GameObject[] blueRobots;

	[SerializeField]
	private GameObject[] blueCameras;

	[SerializeField]
	private GameObject[] redRobots;

	[SerializeField]
	private GameObject[] redCameras;

	[SerializeField]
	private GameObject cameraBorder;

	private void Start()
	{
		cameraBorder.SetActive(value: false);
		gamemode = PlayerPrefs.GetInt("gamemode");
		cameraMode = PlayerPrefs.GetInt("cameraMode");
		redRobot = PlayerPrefs.GetInt("redRobotSettings");
		blueRobot = PlayerPrefs.GetInt("blueRobotSettings");
		if (gamemode == 1)
		{
			multiplayer = true;
		}
		HideAll();
		if (multiplayer)
		{
			cameraBorder.SetActive(value: true);
			redRobots[redRobot].SetActive(value: true);
			redCameras[cameraMode + 2].SetActive(value: true);
			if (cameraMode == 0)
			{
				redRobots[redRobot].GetComponent<SwerveDriveController>().startingReversed = !redRobots[redRobot].GetComponent<SwerveDriveController>().startingReversed;
			}
			blueRobots[blueRobot].SetActive(value: true);
			blueCameras[cameraMode + 2].SetActive(value: true);
			if (cameraMode == 0)
			{
				blueRobots[blueRobot].GetComponent<SwerveDriveController>().startingReversed = !blueRobots[blueRobot].GetComponent<SwerveDriveController>().startingReversed;
			}
		}
		else if (PlayerPrefs.GetString("alliance") == "red")
		{
			redRobots[redRobot].SetActive(value: true);
			redCameras[cameraMode].SetActive(value: true);
			if (cameraMode == 0)
			{
				redRobots[redRobot].GetComponent<SwerveDriveController>().startingReversed = !redRobots[redRobot].GetComponent<SwerveDriveController>().startingReversed;
			}
		}
		else
		{
			blueRobots[blueRobot].SetActive(value: true);
			blueCameras[cameraMode].SetActive(value: true);
			if (cameraMode == 0)
			{
				blueRobots[blueRobot].GetComponent<SwerveDriveController>().startingReversed = !blueRobots[blueRobot].GetComponent<SwerveDriveController>().startingReversed;
			}
		}
	}

	private void Update()
	{
	}

	private void HideAll()
	{
		GameObject[] array = blueRobots;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		array = redRobots;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		array = blueCameras;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
		array = redCameras;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(value: false);
		}
	}
}
