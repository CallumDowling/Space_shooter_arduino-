using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyAltitude : MonoBehaviour
{

    public GameObject Player;
    public GameObject cam;
    Quaternion rotation;

    void Awake()
    {
        rotation = transform.rotation;
    }
    
    

    // Use this for initialization
    void Start()
    {
        gameObject.gameObject.GetComponent<TextMesh>().color = new Color(Random.Range(.75f, 1f), Random.Range(.75f, 1f), Random.Range(.75f, 1f), 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            gameObject.GetComponent<TextMesh>().text =(Player.gameObject.transform.position.y*2187).ToString("F0");
            
        }

        
    }

    void LateUpdate()
    {
        transform.rotation = rotation;
    }
}
