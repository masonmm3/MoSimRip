using UnityEngine;

public class GameScoreTracker : MonoBehaviour
{
	public static float BlueTeleopSpeakerPoints { get; set; }

	public static float BlueAutoSpeakerPoints { get; set; }

	public static float BlueTeleopAmpPoints { get; set; }

	public static float BlueAutoAmpPoints { get; set; }

	public static float BlueAutoLeavePoints { get; set; }

	public static float BlueAutoPenaltyPoints { get; set; }

	public static float BlueTeleopPenaltyPoints { get; set; }

	public static float BlueStagePoints { get; set; }

	public static float RedTeleopSpeakerPoints { get; set; }

	public static float RedAutoSpeakerPoints { get; set; }

	public static float RedTeleopAmpPoints { get; set; }

	public static float RedAutoAmpPoints { get; set; }

	public static float RedAutoLeavePoints { get; set; }

	public static float RedAutoPenaltyPoints { get; set; }

	public static float RedTeleopPenaltyPoints { get; set; }

	public static float RedStagePoints { get; set; }

	private void Start()
	{
		ResetScore();
	}

	public static void ResetScore()
	{
		BlueTeleopSpeakerPoints = 0f;
		BlueAutoSpeakerPoints = 0f;
		BlueTeleopAmpPoints = 0f;
		BlueAutoAmpPoints = 0f;
		BlueAutoLeavePoints = 0f;
		BlueAutoPenaltyPoints = 0f;
		BlueTeleopPenaltyPoints = 0f;
		BlueStagePoints = 0f;
		RedTeleopSpeakerPoints = 0f;
		RedAutoSpeakerPoints = 0f;
		RedTeleopAmpPoints = 0f;
		RedAutoAmpPoints = 0f;
		RedAutoLeavePoints = 0f;
		RedAutoPenaltyPoints = 0f;
		RedTeleopPenaltyPoints = 0f;
		RedStagePoints = 0f;
	}
}
