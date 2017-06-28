using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewToyBehaviour : MonoBehaviour {

    public CountRotations countRotations;
    public GameObject screw;
    public bool screwed = false;


    private SpawnParticleSystem PS;
    private float oldMin = 0f;
    private float oldMax = 1800f;
    private float newMin = -0.06f;
    private float newMax = 0;

    // Use this for initialization
    void Start () {
        PS = GetComponent<SpawnParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {

        if (screwed == false)
        {
            //Get rotation value
            float rot = countRotations.totalRotation;

            //Scale values

            //Truncate rot value
            if (rot > oldMax)
            {
                rot = oldMax;
            }
            if (rot < oldMin)
            {
                rot = oldMin;
            }

            //new_value = ( (old_value - old_min) / (old_max - old_min) ) * (new_max - new_min) + new_min"
            float val = ((rot - oldMin)) / (oldMax - oldMin) * (newMin - newMax) + newMax;
            screw.transform.localPosition = new Vector3(0f, val, 0f);

            //If we have finished screwing
            if (screw.transform.localPosition.y <= newMin)
            {
                screwed = true;
                PS.Spawn(Color.green, transform.position);
            }
        }
        else
            screw.transform.localPosition = new Vector3(0f, newMin, 0f);

        	
	}
}
