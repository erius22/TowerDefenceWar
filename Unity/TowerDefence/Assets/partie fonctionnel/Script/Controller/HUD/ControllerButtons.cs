using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerButtons {


    public static void OnClickSummon(ModelePersonnage personnage)
    {
        bool isSummonable = true;

        if (RessourcePlayer.GetGoldPlayer() - personnage.GetGoldCost() < 0)
        {
            Debug.Log("Pas asser d'or!!!!!");
            isSummonable = false;

        }
        if (RessourcePlayer.GetManaPlayer() - personnage.GetManaCost() < 0)
        {
            Debug.Log("Pas asser de mana!!!!!!");
            isSummonable = false;
        }
        
        if (isSummonable)
        {
            EventManager.TriggerEvent("Summon" + personnage.GetPersonnageName());
            PlayerManager.instance.myPlayerController.CmdSummon(personnage.getIdPersonnage());
        }


    }

}
