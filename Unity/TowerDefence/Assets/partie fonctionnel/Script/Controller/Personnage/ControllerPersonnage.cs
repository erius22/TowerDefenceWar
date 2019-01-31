using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ControllerPersonnage : NetworkBehaviour {

    public enum statusPersonnage { walk, attack, moveToAttack};

    [SerializeField]//a enlever
    private statusPersonnage activeStatus;

    private float second;


    //private float speed;

    //a changer pour que sa soit automatique
	[SerializeField]//a enlever
    private GameObject target;
    [SerializeField]
    //[SyncVar]
    private Transform chateau;

    [SyncVar(hook = "OnSetChateauPosition")]
    private Vector3 chateauPosition;

    //[SyncVar]
    private ModelePersonnage modele;

	[SerializeField]
	AnimationController animationController;

    private SpriteRenderer sprite;

    private AudioSource audioSource;


    // Use this for initialization
    void Start () {
		Debug.Log ("allo");

        activeStatus = statusPersonnage.walk;

        modele = GetComponent<ModelePersonnage>();
        
        if (isServer)
        {
            Debug.LogError("server start");
            Debug.LogError(modele);
        }
        else
        {
            Debug.LogError("client start");
            Debug.LogError(modele);
        }

		animationController = GetComponent<AnimationController> ();
		if (animationController == null) {
			animationController = GetComponentInChildren<AnimationController> ();
		}
		Debug.LogError (animationController);

        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null)
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
        }


        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = GetComponentInChildren<AudioSource>();
        }

    }

    public override void OnStartClient()
    {
        Start();
        CmdOnClientStart();
    }

    [Command]
    private void CmdOnClientStart()
    {
        checkForFlip();
    }

    // Update is called once per frame
    void Update () {

        Debug.LogError("activeStatus");
        Debug.LogError(activeStatus);


		if (this.gameObject.GetComponent<NetworkBehaviour> ().isServer) {

            switch (activeStatus)
            {
                case statusPersonnage.walk:

                    transform.position = Vector3.MoveTowards(transform.position, chateauPosition,
                        modele.GetMovementSpeed() * Time.deltaTime);
                    if (animationController && animationController.getCurrentAnimation() != AnimationController.animationsStates.walk)
                    {
                        animationController.RpcChangeAnimation(AnimationController.animationsStates.walk);
                    }
                    break;

                case statusPersonnage.attack:

                    //Debug.Log("attack");
                    //target.GetComponent<ControllerPersonnage>().GetModele().TakeDamage(modele.GetAttackDamage());
                    //Debug.Log(target);
                    //Debug.Log(target.GetComponent<ControllerPersonnage>().GetModele());
                    //Debug.Log(modele.GetAttackDamage());

                    if (target && target.GetComponent<ControllerPersonnage>())
                    {
                        //Debug.Log("attaque personnage");

                        second += Time.deltaTime * modele.getAttackSpeed();

                        if (second >= 1)
                        {
                            //gestion animation
                            if (animationController)
                            {
                                animationController.RpcChangeAnimation(AnimationController.animationsStates.attack);
                                animationController.RpcAttackSpeedAnimation(modele.getAttackSpeed());
                            }
                            //gestion son
                            if (audioSource)
                            {
                                RpcPlayAudioClipAttack();
                            }

                            //gestion action
                            second = 0;
                            target.GetComponent<ControllerPersonnage>().GetModele().TakeDamage(modele.GetAttackDamage());
                        }
                        else
                        {
                            if (animationController)
                            {
                                animationController.RpcChangeAnimation(AnimationController.animationsStates.idle);
                            }
                        }

                        //Debug.Log (target.GetComponent<ControllerPersonnage> ().GetModele ().getHitPoint ());

                    }
                    else if (target && target.GetComponent<PlayerVie>())
                    {
                        //Debug.Log ("attack chateau");
                        second += Time.deltaTime * modele.getAttackSpeed();


                        if (second >= 1)
                        {
                            //gestion animation
                            if (animationController)
                            {
                                animationController.RpcChangeAnimation(AnimationController.animationsStates.attack);
                                animationController.RpcAttackSpeedAnimation(modele.getAttackSpeed());
                            }

                            //gestion son
                            if (audioSource)
                            {
                                RpcPlayAudioClipAttack();
                            }

                            //gestion action
                            second = 0;
                            target.GetComponent<PlayerVie>().CmdTakeDamage((int)modele.GetAttackDamage());
                        }
                        else
                        {
                            if (animationController)
                            {
                                animationController.RpcChangeAnimation(AnimationController.animationsStates.idle);
                            }
                        }


                    }
                    else
                    { //target est mort
                      //Debug.Log("target est mouru!!!!");
                        target = null;
                        activeStatus = statusPersonnage.walk;
                    }



                    break;

                case statusPersonnage.moveToAttack:

                    if (target)
                    {
                        if (animationController && animationController.getCurrentAnimation() != AnimationController.animationsStates.walk)
                        {
                            animationController.RpcChangeAnimation(AnimationController.animationsStates.walk);
                        }
                        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, modele.GetMovementSpeed() * Time.deltaTime);
                    }
                    else
                    {
                        target = null;
                        activeStatus = statusPersonnage.walk;
                    }
                    break;
            } //fin switch case

			RpcTargetChange (target);

            //checkForFlip();


        }//fin if isServer

        checkForFlip();

        if (modele.getHitPoint() <= 0)
		{
			DestroyObject(this.gameObject);
		}



	}

    private void checkForFlip()
    {
        Debug.LogError("flip");
        if (modele.isLookingRight &&
    (chateauPosition.x < this.gameObject.transform.position.x || (target && target.transform.position.x < this.gameObject.transform.position.x)))
        {

            RpcFlip();
        }

        else if (!modele.isLookingRight &&
            (chateauPosition.x > this.gameObject.transform.position.x || (target && target.transform.position.x > this.gameObject.transform.position.x)))
        {
            RpcFlip();
        }
    }

	[ClientRpc]
	private void RpcTargetChange(GameObject newTarget)
	{
		target = newTarget;
	}

    [ClientRpc]
    private void RpcPlayAudioClipAttack()
    {
        if (modele.GetAudioClipsAttack().Length > 0)
        {
            int indexAudioClipAttack = UnityEngine.Random.Range(0, modele.GetAudioClipsAttack().Length - 1);
            float volume = UnityEngine.Random.Range(modele.volumeAudioLowRange, modele.volumeAudioHighRange);
            audioSource.PlayOneShot(modele.GetAudioClipsAttack()[indexAudioClipAttack], volume);
        }
    }

    //[ClientRpc]
    private void RpcFlip()
	{
		Debug.LogError ("flip");
		modele.isLookingRight = !modele.isLookingRight;
		sprite.flipX = modele.isLookingRight;

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("allo");
    }

    private bool ColliderIsEnemy(Collider2D collider)
    {

		if (this.gameObject.tag == "Player1" && collider.gameObject.tag == "Player2") {

			return true;

		} else if (this.gameObject.tag == "Player2" && collider.gameObject.tag == "Player1") {

			return true;
		}

        return false;
    }

    private bool ColliderIsValid(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<ControllerPersonnage>() || collider.gameObject.GetComponent<PlayerVie>())
        {
            return true;
        }

        return false;
    }

	//appeler quand un object est dans le grand cercle de detection de l'unite
    public void OnCollisionEnterRangeDetectRadius(Collider2D collider)
    {

        if (ColliderIsEnemy(collider) && ColliderIsValid(collider))
        {
            if (target == null)
            {
                target = collider.gameObject;
            }
            activeStatus = statusPersonnage.moveToAttack;
        }
    }


	//appeler quand un object est dans le petit cercle pour definir le range de l'attaque
    public void IsInAttackRange(Collider2D collider)
    {
        if (ColliderIsEnemy(collider) && ColliderIsValid(collider))
        {
			if (target == null) {
				target = collider.gameObject;
            }
            activeStatus = statusPersonnage.attack;
        }

    }


    public ModelePersonnage GetModele()
    {
        return modele;
    }

    public void SetPositionChateauEnemy(Transform position)
    {
        chateau = position;
        OnSetChateauPosition(position.transform.position);
    }

    private void OnSetChateauPosition(Vector3 newChateauPosition)
    {
        chateauPosition = newChateauPosition;
    }

    [ClientRpc]
    public void RpcSetTag(string newTag)
    {
        this.gameObject.tag = newTag;
    }

}
