using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBehavior : MonoBehaviour {
    public double targetAspect = 1920/1080;
    public float targetCamSize;
	// Use this for initialization
	void Start () {
        double currentAspect = (double)Screen.width / (double)Screen.height;
        //Camera.main.orthographicSize = targetScreenWidth / currentAspect ;
        float newSize = (float)(((targetCamSize * 2 * targetAspect) / currentAspect)); ;
        Camera.main.orthographicSize = newSize;
        gameObject.transform.localPosition = new Vector3(0, 150, (newSize - 35) / 4.5f);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnGui()
    {
       
    }
}
