using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class PinningScript : MonoBehaviour
{
	public GameObject redPin;

	public GameObject bluePin;

	public TextMeshProUGUI redText;

	public TextMeshProUGUI blueText;

	public GameObject rtext;

	public GameObject btext;

	public AudioSource player;

	public AudioResource countdown;

	public AudioResource penalty;

	private Coroutine redCoroutine;

	private Coroutine blueCoroutine;

	private bool reachedEndOfGame;

	private void Update()
	{
		if (reachedEndOfGame)
		{
			return;
		}
		if (GameTimer.timer > 0f)
		{
			if (!IsTimerRunning(redCoroutine) && SwerveDriveController.isPinningBlue && SwerveDriveController.isTouchingWallColliderBlue && !SwerveDriveController.isTouchingWallColliderRed)
			{
				StartTimer(DoBlue, ref blueCoroutine);
			}
			else if (!IsTimerRunning(blueCoroutine) && SwerveDriveController.isPinningRed && SwerveDriveController.isTouchingWallColliderRed && !SwerveDriveController.isTouchingWallColliderBlue)
			{
				StartTimer(DoRed, ref redCoroutine);
			}
			else
			{
				StopTimers();
			}
		}
		else
		{
			reachedEndOfGame = true;
			StopTimers();
		}
	}

	private void StartTimer(Func<IEnumerator> timerMethod, ref Coroutine timerCoroutine)
	{
		if (timerCoroutine == null)
		{
			timerCoroutine = StartCoroutine(timerMethod());
		}
	}

	private bool IsTimerRunning(Coroutine timerCoroutine)
	{
		return timerCoroutine != null;
	}

	private void StopTimers()
	{
		player.Stop();
		if (redCoroutine != null)
		{
			StopCoroutine(redCoroutine);
			redCoroutine = null;
		}
		if (blueCoroutine != null)
		{
			StopCoroutine(blueCoroutine);
			blueCoroutine = null;
		}
		redPin.SetActive(value: false);
		bluePin.SetActive(value: false);
		rtext.SetActive(value: false);
		btext.SetActive(value: false);
	}

	private IEnumerator DoBlue()
	{
		do
		{
			yield return new WaitForSeconds(1f);
			if (SwerveDriveController.isPinningBlue && SwerveDriveController.isTouchingWallColliderBlue && !SwerveDriveController.isTouchingWallColliderRed)
			{
				rtext.SetActive(value: false);
				redPin.SetActive(value: false);
				btext.SetActive(value: true);
				bluePin.SetActive(value: true);
				player.resource = countdown;
				player.Play();
				for (int i = 4; i > 0; i--)
				{
					blueText.text = i.ToString();
					yield return new WaitForSeconds(1f);
				}
				Score.blueScore += 5f;
				player.resource = penalty;
				player.Play();
				if (GameTimer.timer > 135f)
				{
					GameScoreTracker.BlueAutoPenaltyPoints += 5f;
				}
				else
				{
					GameScoreTracker.BlueTeleopPenaltyPoints += 5f;
				}
				continue;
			}
			break;
		}
		while (bluePin.activeSelf);
	}

	private IEnumerator DoRed()
	{
		do
		{
			yield return new WaitForSeconds(1f);
			if (SwerveDriveController.isPinningRed && SwerveDriveController.isTouchingWallColliderRed && !SwerveDriveController.isTouchingWallColliderBlue)
			{
				btext.SetActive(value: false);
				rtext.SetActive(value: true);
				redPin.SetActive(value: true);
				bluePin.SetActive(value: false);
				player.resource = countdown;
				player.Play();
				for (int i = 4; i > 0; i--)
				{
					redText.text = i.ToString();
					yield return new WaitForSeconds(1f);
				}
				Score.redScore += 5f;
				player.resource = penalty;
				player.Play();
				if (GameTimer.timer > 135f)
				{
					GameScoreTracker.RedAutoPenaltyPoints += 5f;
				}
				else
				{
					GameScoreTracker.RedTeleopPenaltyPoints += 5f;
				}
				continue;
			}
			break;
		}
		while (redPin.activeSelf);
	}
}
