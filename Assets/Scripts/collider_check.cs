using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider_check : MonoBehaviour {

    BoxCollider boxCollider;

	// Use this for initialization
	void Start () {
        boxCollider = new BoxCollider();
        layer = 0; 
	}

    [HideInInspector]
    public int layer; 

    [HideInInspector]
    public bool HasCollision;
    private void OnTriggerStay(Collider other)
    {
        HasCollision = true;
        layer = other.gameObject.layer;
        //Debug.Log(layer);
    }

    private void OnTriggerExit(Collider other)
    {
        HasCollision = false;

    }


}
