using System.Collections;
using TMPro;
using UnityEngine;

public class SourceRingLoader : MonoBehaviour
{
	public Transform spawnPointCenter;

	public Transform spawnPointLeft;

	public Transform spawnPointRight;

	public GameObject prefabToInstantiate;

	public bool isRedSource;

	private string Playertag = "Player";

	private bool canSpawnRing = true;

	private bool isSpawning;

	public BoxCollider sourceCollider;

	public float numberOfRingsInSource;

	public bool isActiveSource;

	private int lastRandomNum = -1;

	[SerializeField]
	private TMP_Text counter;

	[SerializeField]
	private int noteCount = 45;

	private void Start()
	{
		if (isRedSource)
		{
			Playertag = "RedPlayer";
		}
	}

	private void Update()
	{
		if (!isActiveSource)
		{
			return;
		}
		numberOfRingsInSource = 0f;
		Collider[] array = Physics.OverlapBox(sourceCollider.bounds.center, sourceCollider.bounds.extents, Quaternion.identity);
		foreach (Collider collider in array)
		{
			if (collider.CompareTag("Ring") || collider.CompareTag("noteShotByRed") || collider.CompareTag("noteShotByBlue"))
			{
				numberOfRingsInSource += 1f;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag(Playertag) && canSpawnRing && numberOfRingsInSource < 2f && !isSpawning && noteCount > 0)
		{
			isSpawning = true;
			int num;
			do
			{
				num = Random.Range(1, 4);
			}
			while (num == lastRandomNum);
			lastRandomNum = num;
			Transform transform = spawnPointCenter;
			switch (num)
			{
			case 2:
				transform = spawnPointLeft;
				break;
			case 3:
				transform = spawnPointRight;
				break;
			}
			noteCount--;
			counter.text = noteCount.ToString();
			Object.Instantiate(prefabToInstantiate, transform.position, transform.rotation);
			StartCoroutine(WaitForRingSpawn());
		}
	}

	private IEnumerator WaitForRingSpawn()
	{
		yield return new WaitForSeconds(0.2f);
		isSpawning = false;
	}
}
