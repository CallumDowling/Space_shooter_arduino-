using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aibehavoir : MonoBehaviour
{
    public GameObject explosion;
    public Transform Player;
    public int MoveSpeed;
    public int targetingRange;
    public int MinDist;
    public bool alive = true;



    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        if (Player != null)
        {

            if (alive)
            {
                if (Vector3.Distance(transform.position, Player.position) <= targetingRange)
                {
                    transform.LookAt(Player);

                    if (Vector3.Distance(transform.position, Player.position) >= MinDist)
                    {

                        transform.position += transform.forward * MoveSpeed * Time.deltaTime;


                    }
                }
            }

        }
        if (gameObject.transform.position.y <= -700)
        {
            GameObject e = Instantiate(explosion);
            e.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.layer != LayerMask.NameToLayer("enemy") && collision.gameObject.tag != "laser")
        {
            //Debug.Log(collision.gameObject.layer);
            GameObject e = Instantiate(explosion);
            e.transform.position = gameObject.transform.position;
            Destroy(gameObject);

        }
        //Destroy(gameObject);
    }



    public void damageMessage(int damage)
    {
        if (gameObject.tag != "cannon")
        {

            //Debug.Log("Hi there 5");
            Rigidbody r = gameObject.GetComponent<Rigidbody>();
            r.useGravity = true;
            r.constraints = RigidbodyConstraints.None;
            r.angularVelocity = Random.insideUnitSphere * 30;
            //gameObject.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            alive = false;
        }
        else
        {
            GameObject e = Instantiate(explosion);
            e.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }



    }


}
