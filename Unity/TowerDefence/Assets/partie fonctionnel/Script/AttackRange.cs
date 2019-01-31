using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour {

    private ControllerPersonnage controllerPersonnage;

    private void Start()
    {
        controllerPersonnage = GetComponentInParent<ControllerPersonnage>();

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        controllerPersonnage.IsInAttackRange(collider);
		//Debug.LogError (collider.gameObject.name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("yo");
    }


}
