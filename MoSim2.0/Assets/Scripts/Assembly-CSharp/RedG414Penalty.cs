using UnityEngine;

public class RedG414Penalty : MonoBehaviour
{
	private bool alreadyGavePenalty;

	public AudioSource errorSound;

	private float penaltyWorth = 2f;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("noteShotByRed") && RedZoneControl.redRobotInBlueZone && !alreadyGavePenalty)
		{
			other.tag = "Ring";
			alreadyGavePenalty = true;
			if (GameTimer.timer > 0f)
			{
				if (GameTimer.timer > 135f)
				{
					GameScoreTracker.BlueAutoPenaltyPoints += penaltyWorth;
					Score.blueScore += penaltyWorth;
					errorSound.Play();
				}
				else
				{
					GameScoreTracker.BlueTeleopPenaltyPoints += penaltyWorth;
					Score.blueScore += penaltyWorth;
					errorSound.Play();
				}
			}
		}
		alreadyGavePenalty = false;
	}
}
