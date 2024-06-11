using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class GameTimer : MonoBehaviour
{
	public TextMeshProUGUI timerText;

	private const float MATCH_DURATION = 150f;

	public static float timer;

	public AudioSource player;

	public AudioResource autoStart;

	public AudioResource teleopStart;

	public AudioResource endgameStart;

	public AudioResource matchEnd;

	private bool playEndBuzzer = true;

	private bool playTeleopBuzzer = true;

	private bool playEndgameBuzzer = true;

	public static bool matchOver = false;

	private bool countDownTimer = true;

	public static bool canRobotMove = true;

	public GameObject button;

	public GameObject videoPlayer;

	public Amp allianceBlueAmp;

	public Amp allianceRedAmp;

	private void Start()
	{
		button.SetActive(value: false);
		canRobotMove = true;
		Speaker.noteScoredWorthBlue = 5f;
		Speaker.noteScoredWorthRed = 5f;
		allianceBlueAmp.noteScoredWorth = 2;
		allianceRedAmp.noteScoredWorth = 2;
		player.resource = autoStart;
		player.Play();
		ResetTimer();
	}

	private void Update()
	{
		if (timer > 0f)
		{
			if (countDownTimer)
			{
				timer -= Time.deltaTime;
			}
			if (timer < 1f && playEndBuzzer)
			{
				player.resource = matchEnd;
				player.Play();
				playEndBuzzer = false;
				timer = 0f;
				timerText.color = Color.red;
				matchOver = true;
			}
			else if (timer < 135.2f && playTeleopBuzzer)
			{
				StartCoroutine(Wait());
			}
			else if (timer < 20f && playEndgameBuzzer)
			{
				player.resource = endgameStart;
				player.Play();
				playEndgameBuzzer = false;
			}
			UpdateTimerDisplay(timer);
		}
		else
		{
			StartCoroutine(MatchEndDelay());
		}
	}

	private IEnumerator MatchEndDelay()
	{
		yield return new WaitForSeconds(4f);
		if (PlayerPrefs.GetFloat("endVideo") == 1f)
		{
			videoPlayer.SetActive(value: true);
		}
		else
		{
			button.SetActive(value: true);
		}
	}

	private IEnumerator Wait()
	{
		playTeleopBuzzer = false;
		countDownTimer = false;
		player.resource = matchEnd;
		player.Play();
		canRobotMove = false;
		yield return new WaitForSeconds(3f);
		player.resource = teleopStart;
		player.Play();
		Speaker.noteScoredWorthRed = 2f;
		Speaker.noteScoredWorthBlue = 2f;
		allianceBlueAmp.noteScoredWorth = 1;
		allianceRedAmp.noteScoredWorth = 1;
		canRobotMove = true;
		countDownTimer = true;
	}

	private void ResetTimer()
	{
		timer = 150f;
		timerText.color = Color.white;
	}

	private void UpdateTimerDisplay(float time)
	{
		float num = Mathf.FloorToInt(time / 60f);
		float num2 = Mathf.FloorToInt(time % 60f);
		string text = $"{num:00}:{num2:00}";
		timerText.text = text;
	}
}
