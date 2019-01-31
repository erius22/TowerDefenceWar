using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ControllerJeu : NetworkBehaviour {

    public static ControllerJeu instance;

    public static Transform spawnPoint;
    public static Transform chateauEnemy;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {

        foreach (VueBoutonPersonnage vueBouton in FindObjectsOfType<VueBoutonPersonnage>())
        {
            //ClientScene.RegisterPrefab(vueBouton.GetModelePersonnage().GetObjectPersonnage());

            //EventManager.AddListener("Summon" + vueBouton.GetModelePersonnage().GetPersonnageName(),
                //delegate { SummonPersonnage(vueBouton.GetModelePersonnage()); });
                //delegate { CmdSummonPersonnage(vueBouton.GetModelePersonnage().GetObjectPersonnage()); });
                //delegate { CmdSummonPersonnage(vueBouton.GetModelePersonnage().getIdPersonnage()); });

        }


    }

    // Update is called once per frame
    void Update () {


	}


    /*private void SummonPersonnage(ModelePersonnage personnageModel)
    {
        //Debug.Log(personnageModel.gameObject);
        GameObject personnage = Instantiate(personnageModel.GetObjectPersonnage(), spawnPoint.transform.position, Quaternion.identity);
        Debug.Log(personnage);
        personnage.GetComponent<ControllerPersonnage>().SetPositionChateauEnemy(chateauEnemy);
    }*/

    /*[Command]
    private void CmdSummonPersonnage(GameObject personnage)
    {
        //Debug.Log(personnageModel.gameObject);
        //GameObject personnage = Instantiate(personnageModel.GetObjectPersonnage(), spawnPoint.transform.position, Quaternion.identity);
        //Debug.Log(personnage);
        //personnage.GetComponent<ControllerPersonnage>().SetPositionChateauEnemy(chateauEnemy);
        //NetworkServer.Spawn(personnage);
        //Debug.Log(chateauEnemy);

        //Debug.Log(targetChateauEnemy);

        Debug.Log(PlayerManager.instance.chateauEnemy);

        if (!isServer)
        {
            return;
        }

        

        var personnageObject = (GameObject)Instantiate(personnage, PlayerManager.instance.spawnPosition.position, Quaternion.identity);

        personnageObject.GetComponent<ControllerPersonnage>().SetPositionChateauEnemy(PlayerManager.instance.chateauEnemy.transform);


        //FindObjectOfType<NetworkManager>().spawnPrefabs[0];

        NetworkServer.Spawn(personnageObject);



    }*/




	//bon code d'invocation pour client vers serveur et inverse !!!!!!!!!!!!!!!!!!!!!!!!
	public void SummonPersonnage(int idPersonnage) //, Transform spawnPosition, Transform positionChateauEnemy)
    {


        if (!isServer)
        {
            return;
        }

        Debug.Log(idPersonnage);

		var personnageObject = (GameObject)Instantiate(FindObjectOfType<NetworkManager> ().spawnPrefabs[idPersonnage], PlayerManager.instance.spawnPosition.transform.position, Quaternion.identity);

		personnageObject.GetComponent<ControllerPersonnage> ().SetPositionChateauEnemy (PlayerManager.instance.chateauEnemy.transform);

		NetworkServer.Spawn (personnageObject);


        //var personnageObject = (GameObject)Instantiate(personnage, PlayerManager.instance.spawnPosition.position, Quaternion.identity);

        //personnageObject.GetComponent<ControllerPersonnage>().SetPositionChateauEnemy(PlayerManager.instance.chateauEnemy.transform);




        //NetworkServer.Spawn(personnageObject);



    }

	//test
	public void SummonPersonnage(GameObject personnage) //, Transform spawnPosition, Transform positionChateauEnemy)
	{


		if (!isServer)
		{
			return;
		}

		Debug.Log(personnage);

		var personnageObject = (GameObject)Instantiate(personnage, personnage.transform.position, Quaternion.identity);

		//personnageObject.GetComponent<ControllerPersonnage> ().SetPositionChateauEnemy (PlayerManager.instance.chateauEnemy.transform);

		NetworkServer.Spawn (personnageObject);


		//var personnageObject = (GameObject)Instantiate(personnage, PlayerManager.instance.spawnPosition.position, Quaternion.identity);

		//personnageObject.GetComponent<ControllerPersonnage>().SetPositionChateauEnemy(PlayerManager.instance.chateauEnemy.transform);




		//NetworkServer.Spawn(personnageObject);



	}

    /*[ClientRpc]
    private void RpcPersonnageSpawned(GameObject personnage)
    {

    }*/

}
