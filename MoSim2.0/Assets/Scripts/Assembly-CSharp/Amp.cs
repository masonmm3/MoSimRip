using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Amp : MonoBehaviour
{
	[SerializeField]
	private AllianceColor alliance;

	[SerializeField]
	private GameObject animationNotePrefab;

	[SerializeField]
	private Transform spawnPoint;

	[SerializeField]
	public Collider ampCollider;

	[SerializeField]
	public Material ampFlashMat;

	[SerializeField]
	public Material ampLevelOneMat;

	[SerializeField]
	public Material ampLevelTwoMat;

	[SerializeField]
	public AudioSource source;

	[SerializeField]
	public AudioResource scoreSound;

	[SerializeField]
	private bool doAmpAnimation;

	private bool isFlashing;

	private bool alreadyProcessed;

	private const int NUM_OF_FLASHES_WHEN_AMPED = 10;

	private const int AUTO_NOTE_WORTH = 2;

	public int noteScoredWorth { get; set; }

	public int numOfStoredRings { get; set; }

	private void Start()
	{
		ampFlashMat.SetColor("_EmissionColor", Color.black);
		numOfStoredRings = 0;
		noteScoredWorth = 2;
	}

	private void Update()
	{
		alreadyProcessed = false;
		if ((alliance == AllianceColor.Red && SwerveDriveController.isRedAmped) || (alliance == AllianceColor.Blue && SwerveDriveController.isAmped))
		{
			if (!isFlashing)
			{
				StartCoroutine(FlashYellow());
			}
			return;
		}
		isFlashing = false;
		if (numOfStoredRings > 0 && numOfStoredRings < 2)
		{
			if (alliance == AllianceColor.Blue)
			{
				ampLevelOneMat.SetColor("_EmissionColor", Color.blue * 10f);
			}
			else
			{
				ampLevelOneMat.SetColor("_EmissionColor", Color.red * 10f);
			}
		}
		else if (numOfStoredRings > 1)
		{
			if (alliance == AllianceColor.Blue)
			{
				ampLevelTwoMat.SetColor("_EmissionColor", Color.blue * 10f);
			}
			else
			{
				ampLevelTwoMat.SetColor("_EmissionColor", Color.red * 10f);
			}
		}
		else if (numOfStoredRings == 0)
		{
			ampLevelOneMat.SetColor("_EmissionColor", Color.black);
			ampLevelTwoMat.SetColor("_EmissionColor", Color.black);
		}
	}

	public IEnumerator FlashYellow()
	{
		isFlashing = true;
		for (int i = 0; i < 10; i++)
		{
			if (!isFlashing)
			{
				break;
			}
			ampFlashMat.SetColor("_EmissionColor", Color.yellow * 10f);
			yield return new WaitForSeconds(0.5f);
			ampFlashMat.SetColor("_EmissionColor", Color.black);
			yield return new WaitForSeconds(0.5f);
		}
		isFlashing = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.CompareTag("Ring") || alreadyProcessed || !ampCollider.bounds.Intersects(other.bounds))
		{
			return;
		}
		if (GameTimer.timer > 0f)
		{
			if (GameTimer.timer > 135f)
			{
				if (alliance == AllianceColor.Red)
				{
					GameScoreTracker.RedAutoAmpPoints += noteScoredWorth;
				}
				else if (alliance == AllianceColor.Blue)
				{
					GameScoreTracker.BlueAutoAmpPoints += noteScoredWorth;
				}
			}
			else if (alliance == AllianceColor.Red)
			{
				GameScoreTracker.RedTeleopAmpPoints += noteScoredWorth;
			}
			else if (alliance == AllianceColor.Blue)
			{
				GameScoreTracker.BlueTeleopAmpPoints += noteScoredWorth;
			}
			if (alliance == AllianceColor.Red)
			{
				Score.redScore += noteScoredWorth;
			}
			else if (alliance == AllianceColor.Blue)
			{
				Score.blueScore += noteScoredWorth;
			}
			if ((alliance == AllianceColor.Red && !SwerveDriveController.isRedAmped) || (alliance == AllianceColor.Blue && !SwerveDriveController.isAmped))
			{
				numOfStoredRings++;
			}
			if (doAmpAnimation)
			{
				GameObject gameObject = Object.Instantiate(animationNotePrefab, spawnPoint.position, spawnPoint.rotation);
				gameObject.GetComponent<Rigidbody>().velocity += Vector3.down * 5f;
				if (alliance == AllianceColor.Blue)
				{
					gameObject.gameObject.GetComponent<RingBehaviour>().DownSideForceLeft();
				}
				else
				{
					gameObject.gameObject.GetComponent<RingBehaviour>().DownSideForceRight();
				}
			}
			alreadyProcessed = true;
		}
		source.resource = scoreSound;
		source.Play();
		Object.Destroy(other.gameObject);
	}
}
