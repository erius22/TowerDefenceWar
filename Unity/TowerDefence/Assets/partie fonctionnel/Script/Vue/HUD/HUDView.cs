﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDView : MonoBehaviour {

	public void OnTestButtonClick(){
		HUDController.DecreaseMyPlayerHealth ();
	}
}
