using UnityEngine;

public class ToggleScoreCard : MonoBehaviour
{
	public GameObject scoreCard;

	private bool isEnabled;

	public void ToggleCard()
	{
		if (!isEnabled)
		{
			scoreCard.SetActive(value: true);
			isEnabled = true;
		}
		else
		{
			scoreCard.SetActive(value: false);
			isEnabled = false;
		}
	}
}
