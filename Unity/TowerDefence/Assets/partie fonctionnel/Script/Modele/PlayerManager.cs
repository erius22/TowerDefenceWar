using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	public static PlayerManager instance;

	[HideInInspector]
	public PlayerVie MyPlayerVie;

    public Transform spawnPosition;

    public Transform chateauEnemy;

	//[HideInInspector]
	public PlayerNetwork serverPlayerNetwork;
	//[HideInInspector]
	public PlayerNetwork clientPlayerNetwork;

	public PlayerNetwork myPlayerNetwork;
	public PlayerNetwork otherPlayerNetwok;

	void Awake(){
		instance = this;
		Debug.LogError ("PlayerManager instiate");
		DontDestroyOnLoad (this.gameObject);
	}

    public PlayerController myPlayerController;


}
