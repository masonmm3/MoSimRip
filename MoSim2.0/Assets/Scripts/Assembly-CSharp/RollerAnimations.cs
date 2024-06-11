using UnityEngine;

public class RollerAnimations : MonoBehaviour
{
	[SerializeField]
	private AllianceColor alliance;

	[SerializeField]
	private GameObject[] rollers;

	[SerializeField]
	private float speed;

	private void Update()
	{
		if ((alliance == AllianceColor.Blue && SwerveDriveController.isBlueIntaking) || (alliance == AllianceColor.Red && SwerveDriveController.isRedIntaking))
		{
			GameObject[] array = rollers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].transform.Rotate(Vector3.right, speed * Time.deltaTime);
			}
		}
	}
}
