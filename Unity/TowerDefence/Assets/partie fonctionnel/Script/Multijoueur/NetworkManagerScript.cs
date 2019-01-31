using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManagerScript : NetworkManager {
    public static NetworkManagerScript instance;
	public int connectionAttempt = 0;

	private int MaxConnectionAttempts = 5;

    public void Awake()
    {
        instance = this;
    }

    public void JoinGame(){
        connectionAttempt = 0;
		NetworkManager.singleton.StartMatchMaker ();//demmare service de macht maker unity
		listMatch() ;

	}

	private void createMatch(){
		Debug.LogError ("creating match");
		NetworkManager.singleton.matchMaker.CreateMatch("roomName", 4, true, "", "", "", 0, 0, OnMatchCreate);
	}

	public void listMatch(){
		connectionAttempt++;

		Debug.LogError ("listMachAttemptNumber" + connectionAttempt);

		if (connectionAttempt > MaxConnectionAttempts) {
			createMatch();
			return;
		}

		NetworkManager.singleton.matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
	}


	public void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success) {
			Debug.LogError ("match created");
			StartHost (matchInfo);

		} else {
			Debug.LogError ("Create match failed");
		}
	}

	public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		

		if (matches == null) {
			Debug.LogError ("matches is null");
			listMatch ();
		} else if (matches.Count == 0) {
			Debug.LogError ("matches is 0");
			listMatch ();
		}
		else{
			Debug.LogError ("joining match");
			NetworkManager.singleton.matchMaker.JoinMatch(matches[0].networkId, "" , "", "", 0, 0, OnMatchJoined);
		}
	}

	public void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success) {
			Debug.LogError ("match Join");
			NetworkManager.singleton.StartClient (matchInfo);
		}
		else {
			Debug.Log ("Match Join Failed");
			listMatch ();
		}
	}

	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
	{
		base.OnServerAddPlayer (conn, playerControllerId);

		if (NetworkServer.connections.Count == 2) {
			ServerChangeScene ("FieldScene");
		}
	}
}
	