using UnityEngine;

[CreateAssetMenu]
public class RobotSelector : ScriptableObject
{
	public Robot[] robot;

	public int RobotCount => robot.Length;

	public Robot GetRobot(int index)
	{
		return robot[index];
	}
}
