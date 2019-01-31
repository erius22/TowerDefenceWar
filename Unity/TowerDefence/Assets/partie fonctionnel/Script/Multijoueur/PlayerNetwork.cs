using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerNetwork : NetworkBehaviour {


	// Use this for initialization
	void Start () {
		
		DontDestroyOnLoad (this.gameObject);			
		/*if (isServer && hasAuthority) {
			PlayerManager.instance.serverPlayerNetwork = this;
		} else if (isClient) {
			PlayerManager.instance.clientPlayerNetwork = this;
			CmdLancerMatch ();
		} else {
			PlayerManager.instance.serverPlayerNetwork = this;
		}*/

		if (hasAuthority) {
			PlayerManager.instance.myPlayerNetwork = this;
		} else {
			PlayerManager.instance.otherPlayerNetwok = this;
		}


		if (hasAuthority) {
			if (isServer) {
				PlayerManager.instance.serverPlayerNetwork = this;
			} else {
				PlayerManager.instance.clientPlayerNetwork = this;
				PlayerManager.instance.serverPlayerNetwork = PlayerManager.instance.otherPlayerNetwok;
				//CmdLancerMatch ();
			}
		}

	}
	
	// Update is called once per frame
	void Update () {

	}


	[Command]
	public void CmdLancerMatch(){
		Debug.LogError ("serverLancerScene");
		SceneManager.LoadScene ("FieldScene");
		//RpcLancerMatch ();

	}	

	[ClientRpc]
	public void RpcLancerMatch(){
		if (!isServer) {
			Debug.LogError ("clientLancerScene");
			SceneManager.LoadScene ("FieldScene");
		}
	}

	[Command]
	public void CmdSpawnTour(){
		//RpcLancerMatch ();
		Debug.LogError("spawnTour");
		NetworkServer.SpawnObjects ();
	}

	[Command]
	public void CmdAssignAuthorityTour()
	{
		//this.gameObject.GetComponent<NetworkIdentity> ().AssignClientAuthority (conn);

		NetworkConnection conn = PlayerManager.instance.otherPlayerNetwok.gameObject.GetComponent<NetworkIdentity> ().connectionToClient;


		foreach(PlayerVie playerVie in FindObjectsOfType<PlayerVie>()){

			if (playerVie.tag == "Player2") {
				
				playerVie.gameObject.GetComponent<NetworkIdentity> ().AssignClientAuthority (conn);
				Debug.LogError ("authority set to : " + conn.hostId);

			}
			if (playerVie.tag == "Player1") {

			}

			playerVie.RpcInit ();
			playerVie.gameObject.GetComponent<PlayerController> ().RpcOnChangeAuthority ();
		}
	}

	[Command]
	public void CmdLancerMatchClient(){
		CmdSpawnTour ();
		RpcLancerMatch ();
	}

}
