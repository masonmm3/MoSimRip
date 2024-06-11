using UnityEngine;

public class StageCollisionDetector : MonoBehaviour
{
	public bool robotInStage;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") || other.CompareTag("RedPlayer"))
		{
			robotInStage = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player") || other.CompareTag("RedPlayer"))
		{
			robotInStage = false;
		}
	}
}
