using UnityEngine;

public class SlapDownIntake : MonoBehaviour
{
	[SerializeField]
	private AllianceColor alliance;

	[SerializeField]
	private Transform intakePivot;

	private Quaternion stowRotation;

	[SerializeField]
	private float stowSpeed = 2f;

	[SerializeField]
	private float rotationSpeed = 45f;

	private bool atTarget;

	private bool isRotating;

	private bool isStowing;

	private void Start()
	{
		stowRotation = intakePivot.localRotation;
	}

	private void Update()
	{
		if ((alliance == AllianceColor.Red && SwerveDriveController.isRedIntaking && !isStowing) || (alliance == AllianceColor.Blue && SwerveDriveController.isBlueIntaking && !isStowing))
		{
			RotateIntake(110f);
		}
		else if (!isRotating)
		{
			StowIntake();
		}
		else
		{
			RotateIntake(110f);
		}
	}

	private void RotateIntake(float targetAngle)
	{
		if (!atTarget)
		{
			isRotating = true;
			Quaternion quaternion = Quaternion.Euler(targetAngle, 0f, 0f);
			intakePivot.localRotation = Quaternion.RotateTowards(intakePivot.localRotation, quaternion, rotationSpeed * Time.deltaTime);
			if (Quaternion.Angle(intakePivot.localRotation, quaternion) < 0.1f)
			{
				isRotating = false;
				atTarget = true;
			}
		}
	}

	private void StowIntake()
	{
		isStowing = true;
		atTarget = false;
		intakePivot.localRotation = Quaternion.RotateTowards(intakePivot.localRotation, stowRotation, stowSpeed * Time.deltaTime);
		if (intakePivot.localRotation == stowRotation)
		{
			isStowing = false;
		}
	}
}
