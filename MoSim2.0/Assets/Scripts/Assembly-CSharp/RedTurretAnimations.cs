using UnityEngine;

public class RedTurretAnimations : MonoBehaviour
{
	public GameObject mainRoller;

	public GameObject beltRoller;

	public GameObject miniRoller;

	public GameObject intakeRoller;

	public float intakingAnimationsSpeed = 100f;

	private void Update()
	{
		if (SwerveDriveController.isRedIntaking)
		{
			mainRoller.transform.Rotate(Vector3.right, intakingAnimationsSpeed * Time.deltaTime);
			intakeRoller.transform.Rotate(Vector3.left, intakingAnimationsSpeed * Time.deltaTime);
			beltRoller.transform.Rotate(Vector3.left, intakingAnimationsSpeed * Time.deltaTime);
			miniRoller.transform.Rotate(Vector3.left, intakingAnimationsSpeed * Time.deltaTime);
		}
	}
}
