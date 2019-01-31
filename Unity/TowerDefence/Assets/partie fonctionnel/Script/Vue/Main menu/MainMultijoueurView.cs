using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMultijoueurView : MonoBehaviour {

    public void OnButtonReturnMenu()
    {
        Debug.Log("OnButtonReturnMenu");
        MainMultijoueurController.LoadMenuScene();
    }
}
