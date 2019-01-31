using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerVie : NetworkBehaviour {

	public const int maxHealth = 100;

	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;

	public RectTransform healthBar;

	public override void OnStartAuthority()
	{
		Debug.Log ("PlayerVie OnStartAuthority");
		PlayerManager.instance.MyPlayerVie = this;
	}

	public void Awake(){
		Debug.LogError ("awake vie");
		//Debug.LogError (PlayerManager.instance);
		//Debug.LogError (PlayerManager.instance.serverPlayerNetwork);
		//PlayerManager.instance.serverPlayerNetwork.CmdStartSceneTour ();
		//NetworkServer.SpawnObjects ();
	}

    public void Start()
    {
		Debug.LogError ("playerVie start");
        //EventManager.AddListener("Partie terminee", finPartie);

        //pour que les unite aparaissent et partent dans la bonne direction
		/*if (hasAuthority) {
			Debug.LogError ("hasAutorhority");
			
			PlayerManager.instance.spawnPosition = this.gameObject.transform;
			//this.gameObject.tag = "Player1";
		}
        //ControllerJeu.spawnPoint = this.gameObject.transform;
		else {
			Debug.LogError ("has NOT Autorhority");

			PlayerManager.instance.chateauEnemy = this.gameObject.transform;
			//this.gameObject.tag = "Player2";
		}*/
        //ControllerJeu.chateauEnemy = this.gameObject.transform;



		/*foreach (PlayerVie playerVie in FindObjectsOfType<PlayerVie>()){
			if (isServer && playerVie.gameObject.tag == "Joueur1") {
				PlayerManager.instance.spawnPosition = this.gameObject.transform;
				Debug.LogError ("setTowerPlayer1");
			} else {
				PlayerManager.instance.chateauEnemy = this.gameObject.transform;
			}
		}*/

    }

	[ClientRpc]
	public void RpcInit()
	{
		Debug.LogError ("Init");
		if (hasAuthority) {
			Debug.LogError ("hasAutorhority");
			
			PlayerManager.instance.spawnPosition = this.gameObject.transform;
			//this.gameObject.tag = "Player1";
		}
        //ControllerJeu.spawnPoint = this.gameObject.transform;
		else {
			Debug.LogError ("has NOT Autorhority");

			PlayerManager.instance.chateauEnemy = this.gameObject.transform;
			//this.gameObject.tag = "Player2";
		}
	}

	public override void OnStartServer()
	{
		Debug.LogError ("playerVie start Server");

		/*if (this.tag == "Player2") {
			this.GetComponent<NetworkIdentity> ().AssignClientAuthority (PlayerManager.instance.otherPlayerNetwok.connectionToClient);
		}*/

	}


	public override void OnStartClient ()
	{
		Debug.LogError ("startClient");
		if (this.tag == "Player2") {
			//CmdTakeAuthority ();
			PlayerManager.instance.myPlayerNetwork.CmdAssignAuthorityTour();
		}
		//base.OnStartClient ();
	}

	[Command]
	private void CmdTakeAuthority()
	{
		//this.gameObject.GetComponent<NetworkIdentity> ().RemoveClientAuthority(PlayerManager.instance.myPlayerNetwork.gameObject.GetComponent<NetworkIdentity>().clientAuthorityOwner);


		NetworkConnection conn = PlayerManager.instance.otherPlayerNetwok.gameObject.GetComponent<NetworkIdentity> ().connectionToClient;
		this.gameObject.GetComponent<NetworkIdentity> ().AssignClientAuthority (conn);
		Debug.LogError ("authority set to : " + conn.hostId);



	}
    
    void OnChangeHealth(int health)
	{
        currentHealth = health;
		healthBar.sizeDelta = new Vector2 (health, healthBar.sizeDelta.y);
	}

	[ClientRpc]//Envoyé une fonction a tous les client a partir du serveur
	void RpcDamage(int amount)
	{
		Debug.LogError("Took damage:" + amount);
	}

	[Command]//Fonction envoyer du client vers le serveur(que le serveur traite)
	public void CmdTakeDamage(int amount)
	{
		Debug.Log ("TakeDamage");

		currentHealth -= amount;
		if (currentHealth <= 0) 
		{
			currentHealth = 0;
            RpcfinPartie();
			Debug.Log ("Perdu");
		}
		Debug.LogError (currentHealth);

		RpcDamage (amount);
	}

	[ClientRpc]
    public void RpcfinPartie()
    {
		Debug.LogError (currentHealth);
        //Time.timeScale = 0f;
		if (hasAuthority)// && currentHealth <= 0)
        {
            SceneManager.LoadScene("PlayerLoseScene");
        }

		else{
            SceneManager.LoadScene("PlayerWinScene");
        }
    }
}
