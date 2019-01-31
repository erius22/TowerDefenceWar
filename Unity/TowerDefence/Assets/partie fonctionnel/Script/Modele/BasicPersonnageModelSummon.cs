using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicPersonnageModelSummon : MonoBehaviour {

    [SerializeField]
    protected int goldCost;
    [SerializeField]
    protected string PersonnageName;
    [SerializeField]
    protected int manaCost;
    [SerializeField]
    protected GameObject objectPersonngae;

	// Use this for initialization
	public virtual void Start () {


        EventManager.AddListener("Summon" + PersonnageName, OnSummon);

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    protected abstract void OnSummon();



    /*public int GetGoldCost()
    {
        return goldCost;
    }

    public string GetPersonnageName()
    {
        return PersonnageName;
    }

    public int GetManaCost()
    {
        return manaCost;
    }*/

    public GameObject GetObjectPersonnage()
    {
        return objectPersonngae;
    }

}
