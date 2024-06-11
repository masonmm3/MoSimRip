using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
	public TextMeshProUGUI redAutoSpeakerScore;

	public TextMeshProUGUI redAutoAmpScore;

	public TextMeshProUGUI redAutoLeaveScore;

	public TextMeshProUGUI redAutoPenalty;

	public TextMeshProUGUI redTeleopSpeakerScore;

	public TextMeshProUGUI redTeleopAmpScore;

	public TextMeshProUGUI redStageScore;

	public TextMeshProUGUI redTeleopPenalty;

	public TextMeshProUGUI totalRedScore;

	public TextMeshProUGUI blueAutoSpeakerScore;

	public TextMeshProUGUI blueAutoAmpScore;

	public TextMeshProUGUI blueAutoLeaveScore;

	public TextMeshProUGUI blueAutoPenalty;

	public TextMeshProUGUI blueTeleopSpeakerScore;

	public TextMeshProUGUI blueTeleopAmpScore;

	public TextMeshProUGUI blueStageScore;

	public TextMeshProUGUI blueTeleopPenalty;

	public TextMeshProUGUI totalBlueScore;

	public TextMeshProUGUI redRobot;

	public TextMeshProUGUI blueRobot;

	private void Start()
	{
		redAutoAmpScore.text = "Amp: " + GameScoreTracker.RedAutoAmpPoints;
		redAutoSpeakerScore.text = "Speaker: " + GameScoreTracker.RedAutoSpeakerPoints;
		redAutoLeaveScore.text = "Mobility: " + GameScoreTracker.RedAutoLeavePoints;
		redAutoPenalty.text = "Penalty: " + GameScoreTracker.RedAutoPenaltyPoints;
		redTeleopAmpScore.text = "Amp: " + GameScoreTracker.RedTeleopAmpPoints;
		redTeleopSpeakerScore.text = "Speaker: " + GameScoreTracker.RedTeleopSpeakerPoints;
		redStageScore.text = "Stage: " + GameScoreTracker.RedStagePoints;
		redTeleopPenalty.text = "Penalty: " + GameScoreTracker.RedTeleopPenaltyPoints;
		totalRedScore.text = Score.redScore.ToString();
		blueAutoAmpScore.text = "Amp: " + GameScoreTracker.BlueAutoAmpPoints;
		blueAutoSpeakerScore.text = "Speaker: " + GameScoreTracker.BlueAutoSpeakerPoints;
		blueAutoLeaveScore.text = "Mobility: " + GameScoreTracker.BlueAutoLeavePoints;
		blueAutoPenalty.text = "Penalty: " + GameScoreTracker.BlueAutoPenaltyPoints;
		blueTeleopAmpScore.text = "Amp: " + GameScoreTracker.BlueTeleopAmpPoints;
		blueTeleopSpeakerScore.text = "Speaker: " + GameScoreTracker.BlueTeleopSpeakerPoints;
		blueStageScore.text = "Stage: " + GameScoreTracker.BlueStagePoints;
		blueTeleopPenalty.text = "Penalty: " + GameScoreTracker.BlueTeleopPenaltyPoints;
		totalBlueScore.text = Score.blueScore.ToString();
		int @int = PlayerPrefs.GetInt("blueRobotSettings");
		int int2 = PlayerPrefs.GetInt("redRobotSettings");
		if (PlayerPrefs.GetInt("gamemode") == 0)
		{
			if (PlayerPrefs.GetString("alliance") == "blue")
			{
				switch (@int)
				{
				case 0:
					blueRobot.text = "1690";
					redRobot.text = "";
					break;
				case 1:
					blueRobot.text = "930";
					redRobot.text = "";
					break;
				case 2:
					blueRobot.text = "1678";
					redRobot.text = "";
					break;
				}
			}
			else
			{
				switch (int2)
				{
				case 0:
					redRobot.text = "1690";
					blueRobot.text = "";
					break;
				case 1:
					redRobot.text = "930";
					blueRobot.text = "";
					break;
				case 2:
					redRobot.text = "1678";
					blueRobot.text = "";
					break;
				}
			}
		}
		else if (PlayerPrefs.GetInt("gamemode") == 1)
		{
			switch (@int)
			{
			case 0:
				blueRobot.text = "1690";
				break;
			case 1:
				blueRobot.text = "930";
				break;
			case 2:
				blueRobot.text = "1678";
				break;
			}
			switch (int2)
			{
			case 0:
				redRobot.text = "1690";
				break;
			case 1:
				redRobot.text = "930";
				break;
			case 2:
				redRobot.text = "1678";
				break;
			}
		}
	}
}
