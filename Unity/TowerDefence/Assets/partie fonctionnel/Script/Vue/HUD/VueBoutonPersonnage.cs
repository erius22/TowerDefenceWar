using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VueBoutonPersonnage : MonoBehaviour {

    //[SerializeField]
    //private BasicPersonnageModelSummon ModelPersonnage;
    //[SerializeField]
    private Text textGold;
    //[SerializeField]
    private Text textMana;
    [SerializeField]
    private ModelePersonnage modelePersonnage;


    // Use this for initialization
    void Start () {

        //textGold = GetComponentInChildren<Text>();

        foreach (Text text in GetComponentsInChildren<Text>())
        {

            if (text.name == "GoldCostText")
            {
                textGold = text;
            }

            else if (text.name == "ManaCostText")
            {
                textMana = text;
            }
        }


        //textGold.text = "" + ModelPersonnage.GetGoldCost();
        //textMana.text = "" + ModelPersonnage.GetManaCost();

        textGold.text = "" + modelePersonnage.GetGoldCost();
        textMana.text = "" + modelePersonnage.GetManaCost();
        
        GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnClick()
    {
        ControllerButtons.OnClickSummon(modelePersonnage);
    }

    public ModelePersonnage GetModelePersonnage()
    {
        return modelePersonnage;
    }

}
