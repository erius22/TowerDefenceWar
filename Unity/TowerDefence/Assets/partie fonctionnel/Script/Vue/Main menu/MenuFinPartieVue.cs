using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFinPartieVue : MonoBehaviour {

    public void OnButtonRecommencer()
    {
        Debug.Log("OnButtonReturnMenu");
        MenuFinPartieController.LoadMenuMultijoueur();
    }

    public void OnButtonQuitter()
    {
        MenuFinPartieController.LoadMenuScene();
    }

}
