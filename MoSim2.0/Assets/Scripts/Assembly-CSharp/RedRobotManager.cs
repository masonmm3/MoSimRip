using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RedRobotManager : MonoBehaviour
{
	public RobotSelector rs;

	public TextMeshProUGUI nameText;

	public Image robotImage;

	private int selectedOption;

	private void Start()
	{
		if (PlayerPrefs.HasKey("redRobotSettings"))
		{
			selectedOption = PlayerPrefs.GetInt("redRobotSettings");
		}
		UpdateCharacter(selectedOption);
	}

	public void NextOption()
	{
		selectedOption++;
		if (selectedOption >= rs.RobotCount)
		{
			selectedOption = 0;
		}
		UpdateCharacter(selectedOption);
		PlayerPrefs.SetInt("redRobotSettings", selectedOption);
	}

	public void BackOption()
	{
		selectedOption--;
		if (selectedOption < 0)
		{
			selectedOption = rs.RobotCount - 1;
		}
		UpdateCharacter(selectedOption);
		PlayerPrefs.SetInt("redRobotSettings", selectedOption);
	}

	private void UpdateCharacter(int selectedOption)
	{
		Robot robot = rs.GetRobot(selectedOption);
		robotImage.sprite = robot.robotImage;
		nameText.text = robot.robotName;
	}
}
