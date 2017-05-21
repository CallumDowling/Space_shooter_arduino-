using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<playerBehavior>() != null)
        {
            Debug.Log("powerup collided");
            collision.transform.GetComponent<playerBehavior>().shootInterval /= 2;
            Destroy(gameObject);
        }
    }


}
