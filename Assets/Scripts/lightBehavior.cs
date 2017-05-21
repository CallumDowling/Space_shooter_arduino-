using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightBehavior : MonoBehaviour {
    public int speed;
    public GameObject earth;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(earth.transform.position + new Vector3(50, 0, 0), earth.transform.up, speed * Time.deltaTime);
        //transform.Rotate(earth.transform.right, (speed) * Time.deltaTime);
        //transform.Rotate(earth.transform.up, (speed) * Time.deltaTime);
    }
}
