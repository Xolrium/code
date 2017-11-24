/* CMDG ERNESTO HERNANDEZ
	PORTFOLIO: http://ernestohh.weebly.com/
*/
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

	#region Variables
	[SerializeField]
	private Behaviour[] componentsToDisable;
	[SerializeField]
	private string remoteLayerName = "RemotePlayer";
	[SerializeField]
	private GameObject playerUI;
	private GameObject playerUIInstance;

	private Camera sceneCamera;
	#endregion

	#region Unity Methods
	void Start () 
	{
		if (!isLocalPlayer)
		{
			DisableComponentens();
			AssignRemoteLayer();
		}
		else
		{
			sceneCamera = Camera.main;
			if(sceneCamera != null)
			{
				sceneCamera.gameObject.SetActive(false);
			}
			//playerUIInstance = Instantiate(playerUI);
			//playerUIInstance.name = playerUI.name;
		}

		GetComponent<Player>().Setup();
	}

	public override void OnStartClient()
	{
		base.OnStartClient();

		string _netID = GetComponent<NetworkIdentity>().netId.ToString();
		Player _player = GetComponent<Player>();

		GameManager.RegisterPlayer(_netID, _player);
	}

	private void AssignRemoteLayer()
	{
		gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
	}

	private void DisableComponentens()
	{
		for (int i = 0; i < componentsToDisable.Length; i++)
		{
			componentsToDisable[i].enabled = false;
		}
	}

	private void OnDisable()
	{
		Destroy(playerUIInstance);

		if(sceneCamera != null)
		{
			sceneCamera.gameObject.SetActive(true);
		}

		GameManager.UnregisterPlayer(transform.name);
	}
	#endregion
}
