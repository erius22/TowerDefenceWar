using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RangeDetection : MonoBehaviour {


    private ControllerPersonnage controllerPersonnage;

    private void Start()
    {
        controllerPersonnage = GetComponentInParent<ControllerPersonnage>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        controllerPersonnage.OnCollisionEnterRangeDetectRadius(collider);
    }

}
