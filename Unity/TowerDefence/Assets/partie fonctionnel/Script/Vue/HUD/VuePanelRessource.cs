using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VuePanelRessource : MonoBehaviour {

    [SerializeField]
    private Text textGold;
    [SerializeField]
    private Text textMana;

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {

        textGold.text = "Or: " + RessourcePlayer.GetGoldPlayer();

        textMana.text = "Mana: " + RessourcePlayer.GetManaPlayer();
		
	}
}
