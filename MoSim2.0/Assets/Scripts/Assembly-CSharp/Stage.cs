using UnityEngine;

public class Stage : MonoBehaviour
{
	public bool isRedStage;

	public Collider stageCollider;

	public bool robotIsInStage;

	private void Update()
	{
		if (GameTimer.timer < 20f)
		{
			stageCollider.enabled = true;
		}
		else
		{
			stageCollider.enabled = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (GameTimer.timer > 0f && GameTimer.timer < 20f && stageCollider.bounds.Intersects(other.bounds) && ((isRedStage && other.gameObject.CompareTag("RedPlayer")) || (!isRedStage && other.gameObject.CompareTag("Player"))) && !robotIsInStage)
		{
			if (isRedStage)
			{
				GameScoreTracker.RedStagePoints += 1f;
				Score.redScore += 1f;
				robotIsInStage = true;
			}
			else
			{
				GameScoreTracker.BlueStagePoints += 1f;
				Score.blueScore += 1f;
				robotIsInStage = true;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (robotIsInStage && GameTimer.timer < 20f && GameTimer.timer > 0f)
		{
			if (isRedStage && other.gameObject.CompareTag("RedPlayer"))
			{
				GameScoreTracker.RedStagePoints -= 1f;
				Score.redScore -= 1f;
				robotIsInStage = false;
			}
			else if (!isRedStage && other.gameObject.CompareTag("Player"))
			{
				GameScoreTracker.BlueStagePoints -= 1f;
				Score.blueScore -= 1f;
				robotIsInStage = false;
			}
		}
	}
}
