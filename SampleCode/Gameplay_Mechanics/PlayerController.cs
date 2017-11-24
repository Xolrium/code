/* Script made by: Ernesto Hernandez 
		Portfolio http://ernestohh.weebly.com/
												*/
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
	private PlayerMotor motor;
	[SerializeField]
	public float speed = 5;
	[SerializeField]
	private float sensitivity = 8;
	private AudioSource aS;
	public AudioClip communicate;

	private void Awake()
	{
		aS = GetComponent<AudioSource>();
		motor = GetComponent<PlayerMotor>();
	}

	private void Update()
	{
		float _xMov = Input.GetAxisRaw("Horizontal");
		float _zMov = Input.GetAxisRaw("Vertical");

		if (_xMov != 0 || _zMov != 0)
		{
			if (!aS.isPlaying)
			{
				aS.Play();
			}
			aS.volume = 1f;
			aS.pitch = Random.Range(-1, 1);
		}
		else
			aS.volume = 0f;

		Vector3 _movHorizontal = transform.right * _xMov;
		Vector3 _movVertical = transform.forward * _zMov;

		Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;
		motor.Move(_velocity);

		float _yRot = Input.GetAxisRaw("Mouse X");
		Vector3 _rotation = new Vector3(0, _yRot, 0) * sensitivity;
		motor.Rotate(_rotation);

		float _xRot = Input.GetAxisRaw("Mouse Y");
		Vector3 _cameraRotation = new Vector3(_xRot, 0, 0) * sensitivity;
		motor.CameraRotation(_cameraRotation);

        if (Input.GetButton("LCC"))
			speed = 10f;
		else
			speed = 5f;
	}
}