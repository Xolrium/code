/* CMDG ERNESTO HERNANDEZ
	PORTFOLIO: http://ernestohh.weebly.com/
*/
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	#region Variables
	[SerializeField]
	public float speed = 5f;
	[SerializeField]
	private float lookSpeed = 3f;

	private PlayerMotor motor;
	#endregion

	#region Unity Methods
	private void Start()
	{
		motor = GetComponent<PlayerMotor>();
	}

	private void Update()
	{
		Move();
		Rotation();
			CameraRotation();
	}

	private void Move()
	{
		float _movX = Input.GetAxisRaw("Horizontal");
		float _movZ = Input.GetAxisRaw("Vertical");

		Vector3 _movHorizontal = transform.right * _movX;
		Vector3 _movVertical = transform.forward * _movZ;

		Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

		motor.Move(_velocity);
	}

	private void Rotation()
	{
		float _yRot = Input.GetAxisRaw("Mouse X");

		Vector3 _rotation = new Vector3(0, _yRot, 0) * lookSpeed;

		motor.Rotate(_rotation);
	}

	private void CameraRotation()
	{
		float _xRot = Input.GetAxisRaw("Mouse Y");

		float _camRotationX = _xRot * lookSpeed;

		motor.RotateCamera(_camRotationX);
	}
	#endregion
}
