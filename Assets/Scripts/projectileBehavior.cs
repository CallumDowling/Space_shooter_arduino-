using UnityEngine;
using System.Collections;

public class projectileBehavior : MonoBehaviour
{

    public int velocity;

    public void Update()
    {
        transform.position += new Vector3(0,0,velocity) * Time.deltaTime;
    }
}