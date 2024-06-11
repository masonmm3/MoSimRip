using System.Collections;
using UnityEngine;

public class LedStripController : MonoBehaviour
{
	public Material ledStripMaterial;

	private int numOfFlashes = 3;

	public static bool redFlash;

	public static bool blueFlash;

	public bool isRedFlashing;

	public bool isBlueFlashing;

	public bool isRedRobot;

	public float intensity = 200f;

	private void Start()
	{
		redFlash = false;
		blueFlash = false;
	}

	private void Update()
	{
		if (isRedRobot)
		{
			if (redFlash)
			{
				redFlash = false;
				if (!isRedFlashing)
				{
					StartCoroutine(FlashRed());
				}
			}
			else if (!isRedFlashing)
			{
				if (DetectRingCollisions.hasRingInRobotRed)
				{
					ledStripMaterial.SetColor("_EmissionColor", Color.green * intensity);
				}
				else
				{
					ledStripMaterial.SetColor("_EmissionColor", Color.red * intensity);
				}
			}
		}
		else if (blueFlash)
		{
			blueFlash = false;
			if (!isBlueFlashing)
			{
				StartCoroutine(FlashBlue());
			}
		}
		else if (!isBlueFlashing)
		{
			if (DetectRingCollisions.hasRingInRobotBlue)
			{
				ledStripMaterial.SetColor("_EmissionColor", Color.green * intensity);
			}
			else
			{
				ledStripMaterial.SetColor("_EmissionColor", Color.red * intensity);
			}
		}
	}

	public IEnumerator FlashRed()
	{
		isRedFlashing = true;
		for (int i = 0; i < numOfFlashes; i++)
		{
			ledStripMaterial.SetColor("_EmissionColor", Color.yellow * 0f);
			yield return new WaitForSeconds(0.12f);
			ledStripMaterial.SetColor("_EmissionColor", Color.yellow * intensity);
			yield return new WaitForSeconds(0.12f);
		}
		isRedFlashing = false;
	}

	public IEnumerator FlashBlue()
	{
		isBlueFlashing = true;
		for (int i = 0; i < numOfFlashes; i++)
		{
			ledStripMaterial.SetColor("_EmissionColor", Color.yellow * 0f);
			yield return new WaitForSeconds(0.12f);
			ledStripMaterial.SetColor("_EmissionColor", Color.yellow * intensity);
			yield return new WaitForSeconds(0.12f);
		}
		isBlueFlashing = false;
	}
}
