using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelePersonnage : MonoBehaviour {

    [SerializeField]
    private float movementSpeed,
        attackDamage,
        attackSpeed,
        hitPoint;

    [SerializeField]
    private int goldCost,
        manaCost,
        idPersonnage;

    [SerializeField]
    private string personnageName;

    [SerializeField]
    protected GameObject objectPersonngae;

	[SerializeField]
	public bool isLookingRight;

    [SerializeField]
    private AudioClip[] audioClipsAttack;

    [SerializeField]
    public float volumeAudioLowRange,
        volumeAudioHighRange;


    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public void TakeDamage(float degat)
    {
        hitPoint = hitPoint - degat;
    }

    public float getHitPoint()
    {
        return hitPoint;
    }

    public float GetAttackDamage()
    {
        return attackDamage;
    }

    public float getAttackSpeed()
    {
        return attackSpeed;
    }

    public int GetGoldCost()
    {
        return goldCost;
    }

    public string GetPersonnageName()
    {
        return personnageName;
    }

    public int GetManaCost()
    {
        return manaCost;
    }

    public GameObject GetObjectPersonnage()
    {
        return objectPersonngae;
    }

    public int getIdPersonnage()
    {
        return idPersonnage;
    }

    public AudioClip[] GetAudioClipsAttack()
    {
        return audioClipsAttack;
    }

}
