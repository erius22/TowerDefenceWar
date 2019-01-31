using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	void Update () {

		if (isLocalPlayer) 
		{
			var x = Input.GetAxis ("Horizontal") * Time.deltaTime * 10.0f;
			var y = Input.GetAxis ("Vertical") * Time.deltaTime * 10.0f;

			transform.Translate (0, y, 0);
			transform.Translate (x, 0, 0);
		}
	}

    public override void OnStartAuthority()
    {
		Debug.LogError("PlayerController OnStartAuthority");
		Debug.LogError (this.gameObject.name);
        PlayerManager.instance.myPlayerController = this;
    }

	[ClientRpc]
	public void RpcOnChangeAuthority()
	{
		if (hasAuthority) {
			PlayerManager.instance.myPlayerController = this;
		}
		Debug.Log (hasAuthority);

	}

    [Command]
    public void CmdSummon(int idPersonnage)
    {

		GameObject objectPersonnageTemporaire = FindObjectOfType<NetworkManager> ().spawnPrefabs [idPersonnage];
        var personnage = (GameObject)Instantiate (objectPersonnageTemporaire);

        if (hasAuthority)
        {
            //faire personnage attaque autre
			Debug.Log("invoke has authority");
            personnage.GetComponent<ControllerPersonnage> ().SetPositionChateauEnemy (PlayerManager.instance.chateauEnemy);
            personnage.transform.position = PlayerManager.instance.spawnPosition.transform.position;
            //personnage.tag = "Player1";

        }
        else
        {
			Debug.Log ("invoke doesnt have authority");
            personnage.GetComponent<ControllerPersonnage> ().SetPositionChateauEnemy (PlayerManager.instance.spawnPosition);
            personnage.transform.position = PlayerManager.instance.chateauEnemy.transform.position;
            //personnage.tag = "Player2";
        }

		//ControllerJeu.instance.SummonPersonnage(idPersonnage);
		//ControllerJeu.instance.SummonPersonnage(objectPersonnageTemporaire);
		Debug.LogError("position at prespawn :");
		Debug.LogError(personnage.transform.position);
		
		NetworkServer.Spawn (personnage);

        //change tag
        if (hasAuthority)
        {
            personnage.GetComponent<ControllerPersonnage>().RpcSetTag("Player1");
        }
        else
        {
            personnage.GetComponent<ControllerPersonnage>().RpcSetTag("Player2");
        }

        Debug.LogError ("position after spawn :");
		Debug.LogError (personnage.transform.position);

    }

}
