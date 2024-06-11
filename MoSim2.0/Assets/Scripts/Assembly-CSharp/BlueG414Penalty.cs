using UnityEngine;

public class BlueG414Penalty : MonoBehaviour
{
	private bool alreadyGavePenalty;

	public AudioSource errorSound;

	private float penaltyWorth = 2f;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("noteShotByBlue") && BlueZoneControl.blueRobotInRedZone && !alreadyGavePenalty)
		{
			other.tag = "Ring";
			alreadyGavePenalty = true;
			if (GameTimer.timer > 0f)
			{
				if (GameTimer.timer > 135f)
				{
					GameScoreTracker.RedAutoPenaltyPoints += penaltyWorth;
					Score.redScore += penaltyWorth;
					errorSound.Play();
				}
				else
				{
					GameScoreTracker.RedTeleopPenaltyPoints += penaltyWorth;
					Score.redScore += penaltyWorth;
					errorSound.Play();
				}
			}
		}
		alreadyGavePenalty = false;
	}
}
