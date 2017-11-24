/* Script made by: Ernesto Hernandez 
		Portfolio http://ernestohh.weebly.com/
												*/
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
	private Rigidbody rb;
	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private Vector3 cameraRotation = Vector3.zero;
	[SerializeField]
	private AudioSource aS;
	[SerializeField]
	private AudioClip communicate;
	[SerializeField]
	private AudioClip walking;
	[SerializeField]
	private AudioClip swimming;

	[SerializeField]
	private Camera cam;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void Move(Vector3 _velocity)
	{
		velocity = _velocity;
	}

	public void Rotate(Vector3 _rotation)
	{
		rotation = _rotation;
	}

	public void CameraRotation(Vector3 _cameraRotation)
	{
		cameraRotation = _cameraRotation;
	}

	private void Update()
	{
		PerformSound();
	}

	private void FixedUpdate()
	{
		PerformMovement();
		PerformRotation();
	}

	private void PerformMovement()
	{
		if (velocity != Vector3.zero)
		{
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		}
        Vector3 dir = (new Vector3(transform.position.x, transform.position.y + 50, transform.position.z) - transform.position).normalized;
        if (Input.GetButton("A"))
		{
			aS.PlayOneShot(swimming);
			rb.useGravity = false;
			rb.MovePosition(rb.position + dir * 10 * Time.deltaTime);
		}
        if(Input.GetButtonDown("B"))
		{
			rb.MovePosition(rb.position - new Vector3(0, 1, 0) * Time.fixedDeltaTime);
			rb.useGravity = true;
		}
    }

	private void PerformRotation()
	{
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
		if(cam != null)
		{
			cam.transform.Rotate(cameraRotation);
		}
	}

	private void PerformSound()
	{
		if (Input.GetButtonDown("Y"))
		{
			aS.PlayOneShot(communicate);
		}
	}
}
