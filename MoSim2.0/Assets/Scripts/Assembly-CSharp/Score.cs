using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
	public static float blueScore;

	public static float redScore;

	public TextMeshProUGUI redScoreText;

	public TextMeshProUGUI blueScoreText;

	private void Start()
	{
		blueScore = 0f;
		redScore = 0f;
	}

	private void Update()
	{
		redScoreText.text = redScore.ToString();
		blueScoreText.text = blueScore.ToString();
	}
}
