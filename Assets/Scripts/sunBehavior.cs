using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunBehavior : MonoBehaviour {
    public GameObject earth;
	// Use this for initialization
	void Start () {
        transform.LookAt(earth.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
