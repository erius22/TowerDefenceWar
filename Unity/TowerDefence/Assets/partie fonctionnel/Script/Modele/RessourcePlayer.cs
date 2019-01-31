using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourcePlayer : MonoBehaviour {

    [SerializeField]
    private int startGold = 0;
    [SerializeField]
    private int startMana = 0;
    private static int goldPlayer = 0;
    private float goldGainPerSecond = 5;
    private static int manaPlayer = 0;
	private float manaGainPerSecond = 5;
    private float secondGainGold = 0;
	private float secondGainMana = 0;


    // Use this for initialization
    void Start () {

        goldPlayer = startGold;
        manaPlayer = startMana;

        foreach(VueBoutonPersonnage vueBouton in FindObjectsOfType<VueBoutonPersonnage>())
        {
           // Debug.Log(personnageModel.GetGoldCost());

            EventManager.AddListener("Summon" + vueBouton.GetModelePersonnage().GetPersonnageName(),
                delegate { ReduceGold(vueBouton.GetModelePersonnage().GetGoldCost()); });

            //Debug.Log(vueBouton.GetModelePersonnage().GetGoldCost());

            EventManager.AddListener("Summon" + vueBouton.GetModelePersonnage().GetPersonnageName(),
                delegate { ReduceMana(vueBouton.GetModelePersonnage().GetManaCost()); });
        }


    }


    // Update is called once per frame
    void Update () {

        secondGainGold += Time.deltaTime * goldGainPerSecond;

        if (secondGainGold >= 1)
        {
            secondGainGold = 0;
            goldPlayer++;

        }

		secondGainMana += Time.deltaTime * manaGainPerSecond;

		if (secondGainMana >= 1)
		{
			secondGainMana = 0;
			manaPlayer++;

		}

		
	}

    private static void ReduceGold(int costGold)
    {
        goldPlayer = goldPlayer - costGold;
    }


    public static int GetGoldPlayer()
    {
        return goldPlayer;
    }

    public static void ReduceMana(int costMana)
    {
        manaPlayer = manaPlayer - costMana;
    }


    public static int GetManaPlayer()
    {
        return manaPlayer;
    }

}
