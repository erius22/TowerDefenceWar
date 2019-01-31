using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HUDController {

	public static void DecreaseMyPlayerHealth(){
		Debug.Log ("DecreaseMyPlayerHealth");
		Debug.Log (PlayerManager.instance.MyPlayerVie != null);
		if (PlayerManager.instance.MyPlayerVie != null) {
			//PlayerManager.instance.MyPlayerVie.TakeDamage (10);
			PlayerManager.instance.MyPlayerVie.CmdTakeDamage (10);

		}
	}
}
