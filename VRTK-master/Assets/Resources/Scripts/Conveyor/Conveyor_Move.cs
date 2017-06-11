using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor_Move : MonoBehaviour {

    public float conveyor_speed;
    public float max_speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Move colliding object along the conveyor's front vector

	}

    void OnCollisionStay(Collision col)
    {
        if (col.transform.GetComponent<Rigidbody>())
        {
            if (col.transform.GetComponent<Rigidbody>().velocity.magnitude < max_speed)
            {
                Rigidbody rb = col.transform.GetComponent<Rigidbody>();
                rb.AddForce(-transform.right * conveyor_speed, ForceMode.Acceleration);
            }
        }
    }
}
