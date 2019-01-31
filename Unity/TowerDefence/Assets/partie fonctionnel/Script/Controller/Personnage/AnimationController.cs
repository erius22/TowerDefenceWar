using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AnimationController : NetworkBehaviour {

	public enum animationsStates
	{
		walk,
		idle,
		attack
	};

    private animationsStates currentAnimationsStates;

	private Animator anim;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();

		if (anim == null) {
			anim = GetComponentInChildren<Animator> ();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [ClientRpc]
    public void RpcAttackSpeedAnimation(float attackSpeed)
    {
        anim.SetFloat("AttackSpeed", attackSpeed);
    }

    [ClientRpc]
	public void RpcChangeAnimation(animationsStates newAnimation)
	{
        Debug.LogError(newAnimation);
        currentAnimationsStates = newAnimation;

        switch (newAnimation)
        {

            case animationsStates.walk:
                anim.SetTrigger("Walk");
                break;


            case animationsStates.attack:
                anim.SetTrigger("Attack");
                break;


            case animationsStates.idle:
                anim.SetTrigger("Idle");
                break;



            default:
                Debug.LogError("animation n'existe pas");
                break;

        }
	}

    public animationsStates getCurrentAnimation()
    {
        return currentAnimationsStates;
    }

}
