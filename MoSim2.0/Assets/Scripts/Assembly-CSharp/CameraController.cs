using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
	public Controls controls;

	public Rigidbody rb;

	public float moveSpeed;

	private Vector2 translateValue;

	private Vector3 startingDirection;

	private Vector3 startingRotation;

	private void Start()
	{
		startingDirection = base.transform.forward;
		startingRotation = base.transform.right;
		controls = new Controls();
		controls.RobotControls.Enable();
	}

	private void FixedUpdate()
	{
		Vector3 vector = startingDirection * translateValue.y + startingRotation * translateValue.x;
		rb.AddForce(vector * moveSpeed);
	}

	public void OnMove(InputAction.CallbackContext ctx)
	{
		translateValue = ctx.ReadValue<Vector2>();
	}
}
