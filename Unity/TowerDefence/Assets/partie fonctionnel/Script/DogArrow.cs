using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody2D))]
public class DogArrow : NetworkBehaviour {

	[SerializeField]
	private float force;

	private Rigidbody2D myRigidBody;

	private Vector2 direction;

    private bool isLookingRight;

    [SerializeField]
    private bool isLookingRightAtBase;

    [SerializeField]
    private float damage;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> (); 
	}
	void FixedUpdate()
	{
        if (isLookingRight)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
		myRigidBody.velocity = direction *force *Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isServer && ColliderIsEnemy(collider))
        {
            if (collider.gameObject.GetComponent<ControllerPersonnage>())
            {
                //attaque personnage
                //collision.gameObject.GetComponent<ControllerPersonnage>().GetModele().TakeDamage(/*degat du chien*/);
                collider.gameObject.GetComponent<ControllerPersonnage>().GetModele().TakeDamage(damage);
                RpcBecomeInvisible();
            }

            else if (collider.gameObject.GetComponent<PlayerVie>())
            {
                //attaque tour
                collider.gameObject.GetComponent<PlayerVie>().CmdTakeDamage((int)damage);
                RpcBecomeInvisible();
            }
        }
    }

    private bool ColliderIsEnemy(Collider2D collider)
    {
        //Debug.Log ("this.tag : " + this.gameObject.tag);
        //if collider.gameObject !hasAuthority
        /*if (collider.gameObject.tag == "Enemy")
        {
            return true;
        }*/

        if (this.gameObject.tag == "Player1" && collider.gameObject.tag == "Player2")
        {

            return true;

        }
        else if (this.gameObject.tag == "Player2" && collider.gameObject.tag == "Player1")
        {

            return true;
        }

        return false;
    }

    [ClientRpc]
    private void RpcBecomeInvisible()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
	}
	void OnBecameInvisible()
	{
		Destroy (gameObject);
	}

    [ClientRpc]
    public void RpcSetIsLookingRight(bool newIsLookingRight)
    {
        isLookingRight = newIsLookingRight;
        checkForFlip();
    }
    private void checkForFlip()
    {
        if (isLookingRightAtBase)
        {
            GetComponent<SpriteRenderer>().flipX = !isLookingRight;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = isLookingRight;
        }
    }

    [ClientRpc]
    public void RpcSetTag(string newTag)
    {
        this.gameObject.tag = newTag;
    }
}
