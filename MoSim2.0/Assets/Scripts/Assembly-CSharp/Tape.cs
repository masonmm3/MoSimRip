using UnityEngine;

public class Tape : MonoBehaviour
{
	public bool triggeredMobilityScore;

	public bool isRedTape;

	private void Start()
	{
		triggeredMobilityScore = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isRedTape)
		{
			if (other.gameObject.CompareTag("Player") && GameTimer.timer > 135f && !triggeredMobilityScore)
			{
				triggeredMobilityScore = true;
				Score.blueScore += 2f;
				GameScoreTracker.BlueAutoLeavePoints += 2f;
			}
		}
		else if (other.gameObject.CompareTag("RedPlayer") && GameTimer.timer > 135f && !triggeredMobilityScore)
		{
			triggeredMobilityScore = true;
			Score.redScore += 2f;
			GameScoreTracker.RedAutoLeavePoints += 2f;
		}
	}
}
