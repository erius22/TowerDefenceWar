using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainOptionView : MonoBehaviour {

	public void OnButtonBackClick(){
		Debug.Log ("OnButtonPlayClick");
		MainOptionController.LoadMenuScene();
	}
}
