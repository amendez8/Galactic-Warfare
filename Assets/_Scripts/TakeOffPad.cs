using UnityEngine;
using System.Collections;

public class TakeOffPad : MonoBehaviour {
    private bool takeOff = false;
    private float speed = 5.0f;
	// Use this for initialization
	void Start () {

	}                     
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("d") || Input.GetKey("a") || Input.GetKey("w") || Input.GetKey("s"))
            takeOff = true;
        if(takeOff == true)
            transform.Translate(Vector3.up * speed * Time.deltaTime);
	}
}
