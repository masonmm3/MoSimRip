using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Speaker : MonoBehaviour
{
	public Collider speakerCollider;

	public AudioSource source;

	public AudioResource scoreSound;

	public AudioResource ampedScoreSound;

	public static float noteScoredWorthRed = 5f;

	public static float noteScoredWorthBlue = 5f;

	public float noteWorthRed;

	public float noteWorthBlue;

	public static float numOfNotesScoredDuringAmpPeriodRed = 0f;

	public static float numOfNotesScoredDuringAmpPeriodBlue = 0f;

	public float numOfNotesBlue;

	public float numOfNotesRed;

	private bool trackNotes;

	public float score;

	private bool alreadyProcessed;

	public bool isRedSpeaker;

	private bool stopAmp;

	public SwerveDriveController[] drives;

	private void Start()
	{
		numOfNotesScoredDuringAmpPeriodRed = 0f;
		numOfNotesScoredDuringAmpPeriodBlue = 0f;
	}

	private void Update()
	{
		numOfNotesBlue = numOfNotesScoredDuringAmpPeriodBlue;
		numOfNotesRed = numOfNotesScoredDuringAmpPeriodRed;
		noteWorthRed = noteScoredWorthRed;
		noteWorthBlue = noteScoredWorthBlue;
		if (SwerveDriveController.isAmped && !isRedSpeaker)
		{
			trackNotes = true;
			source.resource = ampedScoreSound;
		}
		else if (SwerveDriveController.isRedAmped && isRedSpeaker)
		{
			trackNotes = true;
			source.resource = ampedScoreSound;
		}
		else
		{
			trackNotes = false;
			source.resource = scoreSound;
		}
		alreadyProcessed = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (((!other.gameObject.CompareTag("Ring") || alreadyProcessed) && (!other.gameObject.CompareTag("noteShotByRed") || alreadyProcessed) && (!other.gameObject.CompareTag("noteShotByBlue") || alreadyProcessed)) || !speakerCollider.bounds.Intersects(other.bounds))
		{
			return;
		}
		if (GameTimer.timer > 0f)
		{
			if (isRedSpeaker)
			{
				if (GameTimer.timer > 135f)
				{
					StartCoroutine(ScoreDelay(isRed: true, isAuto: true));
				}
				else
				{
					StartCoroutine(ScoreDelay(isRed: true, isAuto: false));
				}
			}
			else if (GameTimer.timer > 135f)
			{
				StartCoroutine(ScoreDelay(isRed: false, isAuto: true));
			}
			else
			{
				StartCoroutine(ScoreDelay(isRed: false, isAuto: false));
			}
		}
		Object.Destroy(other.gameObject);
		alreadyProcessed = true;
	}

	private IEnumerator ScoreDelay(bool isRed, bool isAuto)
	{
		yield return new WaitForSeconds(1.5f);
		source.Play();
		if (GameTimer.timer > 0f)
		{
			if (isRed)
			{
				if (isAuto)
				{
					GameScoreTracker.RedAutoSpeakerPoints += noteScoredWorthRed;
					Score.redScore += noteScoredWorthRed;
				}
				else
				{
					GameScoreTracker.RedTeleopSpeakerPoints += noteScoredWorthRed;
					Score.redScore += noteScoredWorthRed;
				}
			}
			else if (isAuto)
			{
				GameScoreTracker.BlueAutoSpeakerPoints += noteScoredWorthBlue;
				Score.blueScore += noteScoredWorthBlue;
			}
			else
			{
				GameScoreTracker.BlueTeleopSpeakerPoints += noteScoredWorthBlue;
				Score.blueScore += noteScoredWorthBlue;
			}
		}
		if (!trackNotes)
		{
			yield break;
		}
		if (isRedSpeaker)
		{
			numOfNotesScoredDuringAmpPeriodRed += 1f;
			if (numOfNotesScoredDuringAmpPeriodRed >= 4f)
			{
				SwerveDriveController[] array = drives;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].StopAmplifiedSpeaker();
				}
				numOfNotesScoredDuringAmpPeriodRed = 0f;
			}
			yield break;
		}
		numOfNotesScoredDuringAmpPeriodBlue += 1f;
		if (numOfNotesScoredDuringAmpPeriodBlue >= 4f)
		{
			SwerveDriveController[] array = drives;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].StopAmplifiedSpeaker();
			}
			numOfNotesScoredDuringAmpPeriodBlue = 0f;
		}
	}
}
