using System.Collections;
using UnityEngine;

public class PenaltyManager : MonoBehaviour
{
	[SerializeField]
	private Collider[] colliders;

	[SerializeField]
	private Collider autoMiddleLineCollider;

	[SerializeField]
	private AudioSource errorSound;

	[SerializeField]
	private bool redAlliance;

	[SerializeField]
	private float penaltyCooldown;

	[SerializeField]
	private GameObject[] players;

	[SerializeField]
	private GameObject[] enemys;

	[SerializeField]
	private bool playerInsidePenaltyZone;

	[SerializeField]
	private bool playerInsideStage;

	[SerializeField]
	private bool playerPastAutoLine;

	[SerializeField]
	private StageCollisionDetector stage;

	private bool scoreUpdated;

	private bool isTimerCounting;

	private float penaltyWorth = 5f;

	private void Update()
	{
		if (!scoreUpdated && !isTimerCounting)
		{
			UpdateScore();
		}
		else if (!SwerveDriveController.robotsTouching && !isTimerCounting)
		{
			scoreUpdated = false;
			isTimerCounting = false;
		}
		CheckForCollisions();
	}

	private void CheckForCollisions()
	{
		playerInsidePenaltyZone = false;
		Collider[] array = colliders;
		GameObject[] array2;
		foreach (Collider collider in array)
		{
			array2 = players;
			foreach (GameObject gameObject in array2)
			{
				GameObject[] array3 = enemys;
				foreach (GameObject gameObject2 in array3)
				{
					if (collider.bounds.Intersects(gameObject.GetComponent<Collider>().bounds) || collider.bounds.Intersects(gameObject2.GetComponent<Collider>().bounds))
					{
						playerInsidePenaltyZone = true;
						break;
					}
				}
			}
		}
		playerInsideStage = stage.robotInStage;
		array2 = players;
		foreach (GameObject gameObject3 in array2)
		{
			playerPastAutoLine = autoMiddleLineCollider.bounds.Intersects(gameObject3.GetComponent<Collider>().bounds);
			if (playerPastAutoLine)
			{
				break;
			}
		}
	}

	private void UpdateScore()
	{
		if (!SwerveDriveController.robotsTouching)
		{
			return;
		}
		if (playerInsidePenaltyZone)
		{
			if (GameTimer.timer > 135f && GameTimer.timer > 0f)
			{
				errorSound.Play();
				AddScore(isAutoPoints: true, addScoreToOpponent: false);
			}
			else if (GameTimer.timer > 0f)
			{
				errorSound.Play();
				AddScore(isAutoPoints: false, addScoreToOpponent: false);
			}
		}
		if (playerInsideStage && GameTimer.timer < 20f && GameTimer.timer > 0f)
		{
			errorSound.Play();
			AddScore(isAutoPoints: false, addScoreToOpponent: false);
		}
		if (playerPastAutoLine && GameTimer.timer > 135f)
		{
			errorSound.Play();
			AddScore(isAutoPoints: true, addScoreToOpponent: true);
		}
	}

	private void AddScore(bool isAutoPoints, bool addScoreToOpponent)
	{
		if (redAlliance)
		{
			if (isAutoPoints)
			{
				if (addScoreToOpponent)
				{
					GameScoreTracker.BlueAutoPenaltyPoints += penaltyWorth;
				}
				else
				{
					GameScoreTracker.RedAutoPenaltyPoints += penaltyWorth;
				}
			}
			else if (addScoreToOpponent)
			{
				GameScoreTracker.BlueTeleopPenaltyPoints += penaltyWorth;
			}
			else
			{
				GameScoreTracker.RedTeleopPenaltyPoints += penaltyWorth;
			}
			if (addScoreToOpponent)
			{
				Score.blueScore += penaltyWorth;
			}
			else
			{
				Score.redScore += penaltyWorth;
			}
		}
		else
		{
			if (isAutoPoints)
			{
				if (addScoreToOpponent)
				{
					GameScoreTracker.RedAutoPenaltyPoints += penaltyWorth;
				}
				else
				{
					GameScoreTracker.BlueAutoPenaltyPoints += penaltyWorth;
				}
			}
			else if (addScoreToOpponent)
			{
				GameScoreTracker.RedTeleopPenaltyPoints += penaltyWorth;
			}
			else
			{
				GameScoreTracker.BlueTeleopPenaltyPoints += penaltyWorth;
			}
			if (addScoreToOpponent)
			{
				Score.redScore += penaltyWorth;
			}
			else
			{
				Score.blueScore += penaltyWorth;
			}
		}
		scoreUpdated = true;
		StartCoroutine(NoPenaltysWhenThisIsRunning());
	}

	private IEnumerator NoPenaltysWhenThisIsRunning()
	{
		isTimerCounting = true;
		yield return new WaitForSeconds(penaltyCooldown);
		isTimerCounting = false;
	}
}
