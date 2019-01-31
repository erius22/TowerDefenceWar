using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScriptArcherAttack : NetworkBehaviour {

	public GameObject prefabFlecheChien;

    [Command]
	public void CmdAttaqueFleche () {
        if (PlayerManager.instance.myPlayerController.isServer)
        {
            Debug.Log("TODO fleche apparitrion");

            GameObject fleche = Instantiate(prefabFlecheChien, transform.position, Quaternion.identity);

            bool isLookingRigh;
            isLookingRigh = this.gameObject.transform.parent.gameObject.GetComponent<ModelePersonnage>().isLookingRight;
            Debug.LogError(this.gameObject.transform.parent.gameObject.GetComponent<ModelePersonnage>().isLookingRight);
            //fleche.GetComponent<DogArrow>().RpcSetIsLookingRight(isLookingRigh);

            NetworkServer.Spawn(fleche);

            if (this.gameObject.tag == "Player1" || this.transform.parent.gameObject.tag == "Player1")
            {
                fleche.tag = "Player1";
            }
            else if (this.gameObject.tag == "Player2" || this.transform.parent.gameObject.tag == "Player2")
            {
                fleche.tag = "Player2";
            }
            fleche.GetComponent<DogArrow>().RpcSetIsLookingRight(isLookingRigh);

        }
    }
}
