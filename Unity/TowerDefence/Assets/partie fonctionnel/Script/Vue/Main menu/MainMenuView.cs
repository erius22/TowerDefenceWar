using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuView : MonoBehaviour {

	public void OnButtonPlayClick(){
		Debug.Log ("OnButtonPlayClick");
		MainMenuController.LoadPlayScene();
	}

	public void OnButtonMultiPlayerClick(){
		Debug.Log ("OnButtonMultiPlayerClick");
		MainMenuController.LoadMultiPlayerScene();
	}

	public void OnButtonOptionClick(){
		Debug.Log ("OnButtonOptionClick");
		MainMenuController.LoadOptionScene ();
	}

	public void OnButtonQuitClick(){
		MainMenuController.QuitScene();
	}

}
