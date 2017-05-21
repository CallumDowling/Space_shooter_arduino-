using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooterBehavior : Aibehavoir {
    public GameObject projectile;
    public float shootInterval;
    public float lastShot =0f ;
    public float shootSpeed;
    public Color colour;

    // Use this for initialization
    public override void Start () {
        base.Start();
	}

    // Update is called once per frame
    public override void Update () {
        base.Update();
        //Debug.Log(Time.time + " " + lastShot + " " + shootInterval);
        if (Time.time - lastShot >= shootInterval)
        {

            if (Player != null) { 
                if (Vector3.Distance(transform.position, Player.position) <= MinDist)
                {
                    

                    GameObject t = Instantiate(projectile);
                    //Debug.Log("shot bullet");
                    t.GetComponent<laserBehavior>().source = laserBehavior.laserSource.Enemy;
                    t.transform.rotation = gameObject.transform.rotation;
                    t.gameObject.GetComponent<Renderer>().material.color = colour;
                    t.gameObject.GetComponentInChildren<Light>().color = colour;
                    //Debug.Log("shot bullet, colour changed to" + t.gameObject.GetComponentInChildren<Light>().color);

                    t.transform.position = gameObject.transform.position + gameObject.transform.forward * 2;

                    t.transform.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * shootSpeed;


                    Destroy(t, MinDist/ shootSpeed);
                    lastShot = Time.time;
                }
            }
        }

    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }



}
