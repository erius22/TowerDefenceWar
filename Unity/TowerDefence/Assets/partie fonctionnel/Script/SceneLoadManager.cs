using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SceneLoadManager : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		/*if (PlayerManager.instance.serverPlayerNetwork.hasAuthority) {

			Debug.LogError ("isServer");

			if (this.gameObject.scene.isLoaded) {

				Debug.Log ("sceneLoaded");

				PlayerManager.instance.serverPlayerNetwork.CmdLancerMatchClient ();

				Destroy (this.gameObject);


			}

		} else{// if (PlayerManager.instance.clientPlayerNetwork.hasAuthority) {
			Debug.LogError ("isClient");

			if (this.gameObject.scene.isLoaded) {

				Debug.LogError ("sceneLoaded");

				//PlayerManager.instance.myPlayerNetwork.CmdSpawnTour ();
				NetworkServer.SpawnObjects();
				Destroy (this.gameObject);

			}
		}*/
		
	}
}
