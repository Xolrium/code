/* CMDG ERNESTO HERNANDEZ
	PORTFOLIO: http://ernestohh.weebly.com/
*/
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerShoot : NetworkBehaviour {

	#region Variables
	private const string TAG = "Player";
	public PlayerWeapon weapon;
	[SerializeField]
	private LayerMask mask;
	[SerializeField]
	private Camera cam;
	[SerializeField]
	private ParticleSystem muzzleFlash;
	[SerializeField]
	private GameObject grenade;
	[SerializeField]
	private GameObject trap;
	[SerializeField]
	private LineRenderer lineRenderer;
    private float lineTimer = 0.6f;
	private bool lineBool;
	private AudioSource audioS;
	public AudioClip shotClip;
	public Animator anim;
	#endregion

	#region Unity Methods
	private void Start()
	{
		anim = GetComponentInChildren<Animator>();
		audioS = GetComponent<AudioSource>();
		lineRenderer = gameObject.AddComponent<LineRenderer>();
		if(cam == null)
		{
			Debug.Log("No camera referenced");
			this.enabled = false;
		}
	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Shoot();
			lineRenderer.enabled = true;
			lineBool = true;
			anim.SetBool("isShooting", true);
		}
		if (Input.GetKeyDown(KeyCode.Space))
			ThrowGrenade();
		if (Input.GetMouseButtonDown(1))
			PlaceTrap();
		if (lineBool)
		{
			lineTimer -= Time.deltaTime;
		}
		if (lineTimer <= 0)
		{
			lineBool = false;
			lineRenderer.enabled = false;
			lineTimer = 1f;
			anim.SetBool("isShooting", false);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Grenade")
		{
			HitByGrenade(transform.name);
			Destroy(other.gameObject);
		}
		if(other.tag == "Trap")
		{
			TrapHit(transform.name);
			Destroy(other.gameObject);
		}
	}

	private IEnumerator DestroyGrenadeOverTime(GameObject _grenade)
	{
		yield return new WaitForSeconds(3);
		Destroy(_grenade.gameObject);
	}
	#endregion

	#region Commands
	[Command]
	private void CmdPlaceTrap()
	{
		RpcPlaceTrap();
	}

	[Command]
	private void CmdOnShoot()
	{
		audioS.PlayOneShot(shotClip);
		RpcMuzzleFlash();
	}

	[Command]
	private void CmdOnGrenade()
	{
		RpcOnGrenadeEffect();
	}

	[Command]
	private void CmdPlayerShot(string _playerID, int _damage)
	{
		Debug.Log(_playerID + " has been shot");
		Player _player = GameManager.GetPlayer(_playerID);
		_player.RpcTakeDamage(10);
	}

	[Command]
	private void CmdHitByGrenade(string _playerID)
	{
		Player _player = GameManager.GetPlayer(_playerID);
		_player.RpcTakeDamage(30);
	}

	[Command]
	private void CmdTrapHit(string _playerID)
	{
		Player _player = GameManager.GetPlayer(_playerID);
		_player.RpcTrapped(20);
	}
	#endregion

	#region ClientRpc
	[ClientRpc]
	private void RpcPlaceTrap()
	{
		Debug.Log("Placing Trap");
		Instantiate(trap, new Vector3(transform.position.x, 0.4f, transform.position.z) + transform.forward, transform.rotation);
	}

	[ClientRpc]
	private void RpcOnGrenadeEffect()
	{
		Debug.Log("Throwing Grenade");
		GameObject _gren;
		_gren = Instantiate(grenade, transform.position + transform.forward, transform.rotation) as GameObject;
		_gren.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
		StartCoroutine(DestroyGrenadeOverTime(_gren));
	}

	[ClientRpc]
	private void RpcMuzzleFlash()
	{
		muzzleFlash.Play();
	}
	#endregion

	#region Client
	[Client]
	private void PlaceTrap()
	{
		if (!isLocalPlayer)
			return;

		CmdPlaceTrap();
	}

	[Client]
	private void ThrowGrenade()
	{
		if (!isLocalPlayer)
			return;

		CmdOnGrenade();
	}

	[Client]
	private void TrapHit(string _playerID)
	{
		if (!isLocalPlayer)
			return;
		CmdTrapHit(_playerID);
	}

	[Client]
	private void HitByGrenade(string _playerID)
	{
		if (!isLocalPlayer)
			return;
		CmdHitByGrenade(_playerID);
	}

	[Client]
	private void Shoot()
	{
		if (!isLocalPlayer)
			return;

        CmdOnShoot();

		RaycastHit hit;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
		{
			lineRenderer.SetPosition(0, new Vector3(cam.transform.position.x, cam.transform.position.y - 0.5f, cam.transform.position.z));
			lineRenderer.SetPosition(1, hit.point);
			lineRenderer.useWorldSpace = true;
			lineRenderer.positionCount = 2;
			lineRenderer.startWidth = 1;
			lineRenderer.endWidth = 1;
			lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
			lineRenderer.startColor = Color.blue;
			lineRenderer.endColor = Color.black;
			if (hit.collider.tag == TAG)
				CmdPlayerShot(hit.collider.name, weapon.damage);
		}
	}
	#endregion
}
