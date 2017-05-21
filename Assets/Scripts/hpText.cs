using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class hpText : MonoBehaviour {

    public GameObject Player;
   


	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            if (Player.gameObject.GetComponent<playerBehavior>() != null)
            {
                gameObject.GetComponent<Text>().text = "Health : " + Player.gameObject.GetComponent<playerBehavior>().hp.ToString();
            }
        }
    }
}
