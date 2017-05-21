using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBehavior : MonoBehaviour
{
    public enum laserSource{Player,
                     Enemy };

    public laserSource source;
    public GameObject fire;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<laserBehavior>() == null)
        {
            if (collision.gameObject.GetComponent<playerBehavior>() != null && source == laserSource.Enemy)
            {
                collision.gameObject.GetComponent<playerBehavior>().damageMessage(5);

            }
            else if(collision.gameObject.GetComponent<Aibehavoir>() != null && source == laserSource.Player)
            {
                if(collision.gameObject.tag != "cannon")
                {
                    collision.gameObject.GetComponent<Aibehavoir>().damageMessage(5);
                }
                
            }

            Destroy(gameObject);
        }

    }
}
