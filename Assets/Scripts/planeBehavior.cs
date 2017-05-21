using UnityEngine;
using System.Collections;

public class planeBehavior : MonoBehaviour {
    public GameObject earth;
    private SpinFree spinController;
    private bool checking =true;
	// Use this for initialization
	void Start () {
        spinController = earth.GetComponent<SpinFree>();
	}

    // Update is called once per frame
    void Update() {

        gameObject.transform.Translate(0, 0, 3f * Time.deltaTime);
        if (checking) { 
        if (transform.position.z >= 170)
        {
                spinController.currentState = SpinFree.spinStates.nRightIUp;
                checking = false;
                Debug.Log("changed rotation to : " + spinController.currentState);
        }

    }
	}
}
