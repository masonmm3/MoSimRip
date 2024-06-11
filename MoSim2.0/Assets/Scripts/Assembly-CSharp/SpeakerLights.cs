using System.Collections;
using UnityEngine;

public class SpeakerLights : MonoBehaviour
{
	public Material[] lightMats;

	private bool doWholeLightArrayThing = true;

	public bool isRedSpeaker;

	private bool stopLights;

	private void Start()
	{
		if (!isRedSpeaker)
		{
			Material[] array = lightMats;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetColor("_EmissionColor", Color.blue * 10f);
			}
		}
		else
		{
			Material[] array = lightMats;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetColor("_EmissionColor", Color.red * 10f);
			}
		}
	}

	private void Update()
	{
		if (isRedSpeaker)
		{
			if (SwerveDriveController.isRedAmped && doWholeLightArrayThing)
			{
				stopLights = false;
				Material[] array = lightMats;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetColor("_EmissionColor", Color.yellow * 5f);
				}
				StartCoroutine(BlackOutLightsOneByOne());
				doWholeLightArrayThing = false;
			}
			else if (!SwerveDriveController.isRedAmped)
			{
				stopLights = true;
			}
		}
		else if (SwerveDriveController.isAmped && doWholeLightArrayThing)
		{
			stopLights = false;
			Material[] array = lightMats;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetColor("_EmissionColor", Color.yellow * 5f);
			}
			StartCoroutine(BlackOutLightsOneByOne());
			doWholeLightArrayThing = false;
		}
		else if (!SwerveDriveController.isAmped)
		{
			stopLights = true;
		}
	}

	private IEnumerator BlackOutLightsOneByOne()
	{
		Material[] array = lightMats;
		foreach (Material material in array)
		{
			if (stopLights)
			{
				stopLights = false;
				break;
			}
			material.SetColor("_EmissionColor", Color.black);
			yield return new WaitForSeconds(1f);
		}
		if (!isRedSpeaker)
		{
			Material[] array2 = lightMats;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].SetColor("_EmissionColor", Color.blue * 10f);
			}
		}
		else
		{
			Material[] array2 = lightMats;
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j].SetColor("_EmissionColor", Color.red * 10f);
			}
		}
		doWholeLightArrayThing = true;
	}
}
