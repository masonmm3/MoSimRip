using System.Collections;
using UnityEngine;

public class RingBehaviour : MonoBehaviour
{
	private Vector3 originalScale = new Vector3(0.55f, 0.55f, 0.55f);

	private void Update()
	{
		if (base.transform.position.y < -20f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	public void DownSideForceLeft()
	{
		base.gameObject.GetComponent<ConstantForce>().force = new Vector3(base.gameObject.GetComponent<ConstantForce>().force.x, -0.2f, base.gameObject.GetComponent<ConstantForce>().force.z);
		StartCoroutine(Timer(goOtherWay: false));
	}

	public void DownSideForceRight()
	{
		base.gameObject.GetComponent<ConstantForce>().force = new Vector3(base.gameObject.GetComponent<ConstantForce>().force.x, -0.2f, base.gameObject.GetComponent<ConstantForce>().force.z);
		StartCoroutine(Timer(goOtherWay: true));
	}

	public IEnumerator UnSquishhhh()
	{
		Vector3 startScale = base.transform.localScale;
		float elapsedTime3 = 0f;
		float duration = 0.15f;
		while (elapsedTime3 < duration)
		{
			float t = elapsedTime3 / duration;
			base.transform.localScale = Vector3.Lerp(startScale, originalScale, t);
			elapsedTime3 += Time.deltaTime;
			yield return null;
		}
		elapsedTime3 = 0f;
		while (elapsedTime3 < duration)
		{
			float t2 = elapsedTime3 / duration;
			base.transform.localScale = Vector3.Lerp(originalScale, startScale, t2);
			elapsedTime3 += Time.deltaTime;
			yield return null;
		}
		elapsedTime3 = 0f;
		while (elapsedTime3 < duration)
		{
			float t3 = elapsedTime3 / duration;
			base.transform.localScale = Vector3.Lerp(startScale, originalScale, t3);
			elapsedTime3 += Time.deltaTime;
			yield return null;
		}
		base.transform.localScale = originalScale;
	}

	private IEnumerator Timer(bool goOtherWay)
	{
		yield return new WaitForSeconds(0.8f);
		if (goOtherWay)
		{
			base.gameObject.GetComponent<ConstantForce>().force = new Vector3(0.3f, 0f, base.gameObject.GetComponent<ConstantForce>().force.z);
		}
		else
		{
			base.gameObject.GetComponent<ConstantForce>().force = new Vector3(-0.3f, 0f, base.gameObject.GetComponent<ConstantForce>().force.z);
		}
		yield return new WaitForSeconds(1f);
		Object.Destroy(base.gameObject.GetComponent<ConstantForce>());
	}

	private void OnTriggerEnter(Collider other)
	{
		if ((other.CompareTag("Player") && base.tag == "noteShotByBlue" && !BlueZoneControl.blueRobotInRedZoneUpdated) || (other.CompareTag("RedPlayer") && base.tag == "noteShotByRed" && !RedZoneControl.redRobotInBlueZoneUpdated))
		{
			base.tag = "Ring";
		}
		else if (other.CompareTag("Ring"))
		{
			base.tag = "Ring";
		}
	}
}
