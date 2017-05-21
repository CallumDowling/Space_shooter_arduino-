using UnityEngine;
using System.Collections;

/// <summary>
/// Spin the object at a specified speed
/// </summary>
public class SpinFree : MonoBehaviour {
    
	public bool spin;
	//left hand rule
    public enum spinStates{iRightIUp, nRightIUp};
	public float speed = 10f;
    public GameObject playPlane;
    public Vector3 offset;

	//[HideInInspector]
	//public bool clockwise = true;
	
	[HideInInspector]
	public float directionChangeSpeed = 2f;
    [HideInInspector]
    public spinStates currentState = spinStates.nRightIUp;
    // Update is called once per frame
    void Update() {
        //if (direction < 1f) {
        //	direction += Time.deltaTime / (directionChangeSpeed / 2);
        //}
        gameObject.transform.position = playPlane.transform.position + offset;

        switch (currentState) {

            case spinStates.iRightIUp:
                transform.Rotate(Vector3.right, -(speed) * Time.deltaTime);
                transform.Rotate(Vector3.up, -(speed) * Time.deltaTime / 2);
                break;
            case spinStates.nRightIUp:
                transform.Rotate(Vector3.right, (speed) * Time.deltaTime);
                transform.Rotate(Vector3.up, (speed) * Time.deltaTime / 2);
                break;

        
        } 
        
	}

   

}