/* CMDG ERNESTO HERNANDEZ
	PORTFOLIO: http://ernestohh.weebly.com/
*/
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Player))]
public class Player : NetworkBehaviour {

	#region Variables
	[SerializeField]
	private int maxHealth = 100;
	[SyncVar]
	private bool _isDead = false;
	public bool isDead
	{
		get { return _isDead; }
		protected set { _isDead = value; }
	}
	[SyncVar]
	public float currHealth;
	public Image healthbar;
	[SerializeField]
	private Behaviour[] disableOnDeath;
	private bool[] wasEnabled;
	#endregion

	#region Unity Methods
	public void Awake()
	{
		healthbar = GetComponentInChildren<Image>();
	}

	public void Setup()
	{
		wasEnabled = new bool[disableOnDeath.Length];
		for (int i = 0; i < wasEnabled.Length; i++)
		{
			wasEnabled[i] = disableOnDeath[i].enabled;
		}
		SetDefaults();
	}

	public void OnChangedHp()
	{
		healthbar.fillAmount = currHealth / 100;
	}

	[ClientRpc]
	public void RpcTakeDamage(int _amount)
	{
		if (isDead)
			return;
		currHealth -= _amount;
		Debug.Log(transform.name + " has "+ currHealth + " health.");
		if(currHealth <= 0)
		{
			Die();
		}
		OnChangedHp();
	}

	[ClientRpc]
	public void RpcTrapped(int _amount)
	{
		if (isDead)
			return;
		currHealth -= _amount;
		OnChangedHp();
		PlayerController _player = GetComponent<PlayerController>();
		StartCoroutine(Trapped(_player));
		Debug.Log(transform.name + " has " + currHealth + " health.");
		if (currHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		isDead = true;

		for (int i = 0; i < disableOnDeath.Length; i++)
		{
			disableOnDeath[i].enabled = false;
		}

		Collider _col = GetComponent<Collider>();
		if (_col != null)
			_col.enabled = false;

		StartCoroutine(Respawn());
	}

	private IEnumerator Trapped(PlayerController _player)
	{
		_player.speed = 0f;
		yield return new WaitForSeconds(3);
		_player.speed = 5f;
	}

	private IEnumerator Respawn()
	{
		yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);
		SetDefaults();
		Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
		transform.position = _spawnPoint.position;
		transform.rotation = _spawnPoint.rotation;
	}

	public void SetDefaults()
	{
		isDead = false;
		currHealth = maxHealth;
		OnChangedHp();
		for (int i = 0; i < disableOnDeath.Length; i++)
		{
			disableOnDeath[i].enabled = wasEnabled[i];
		}

		Collider _col = GetComponent<Collider>();
		if (_col != null)
			_col.enabled = true;
	}
	#endregion
}
