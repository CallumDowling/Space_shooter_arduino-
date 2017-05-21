using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class altitudeText : MonoBehaviour
{

    public GameObject Player;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {


            if (Player.gameObject.GetComponent<playerBehavior>() != null)
            {
                gameObject.GetComponent<Text>().text = "Altitude : " + (Player.gameObject.transform.position.y*2187).ToString("F0");
            }
        }
    }
}
