/* CMDG ERNESTO HERNANDEZ
	PORTFOLIO: http://ernestohh.weebly.com/
*/
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

	#region Variables
	[SerializeField]
	private Camera cam;

	public Animator anim;

	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private float cameraRotationX = 0f;
	private float currentCameraRotationX = 0f;
	[SerializeField]
	private float cameraRotationLimit = 85f;

	private Rigidbody rb;
	#endregion

	 #region Unity Methods
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
	}

	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}

	public void Rotate(Vector3 _rotation)
	{
		rotation = _rotation;
	}

	public void RotateCamera(float _cameraRotation)
	{
		cameraRotationX = _cameraRotation;
	}

	private void PerformMovement()
	{
		if(velocity != Vector3.zero)
		{
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
			anim.SetBool("isRunning", true);
		} else
		{
			anim.SetBool("isRunning", false);
		}
	}

	private void PerformRotation()
	{
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
		if (cam != null)
		{
			currentCameraRotationX -= cameraRotationX;
			currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

			cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
		}
	}

	private void FixedUpdate()
	{
		PerformMovement();
		PerformRotation();
	}
	#endregion
}
