using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VRTK.VRTK_InteractableObject))]
public class SnappableParent : MonoBehaviour
{

    /// <summary>
    /// Detects collision with another snappable object, and snaps them together, combining their meshes
    /// </summary>
    public bool snapped;
    public float numParts;
    public bool complete;

    private float partCounter = 0;
    ParticleSystem celebrationParticles;


    void Start()
    {
        if (!GetComponent<ParticleSystem>())
        {
            GameObject ps = Instantiate(Resources.Load<ParticleSystem>("Prefabs/CelebrationPS").gameObject);
            ps.transform.parent = transform;
            ps.transform.localPosition = Vector3.zero;
            celebrationParticles = ps.GetComponent<ParticleSystem>();
        }
    }

    //Snap edge has detected collision
    public void OnCollision(GameObject objectToSnap)
    {
        //Get the snap position for this object
        Transform snapPosition = transform.Find(objectToSnap.name + "_snapPos");

        //Set parent of objectToSnap to this object
        objectToSnap.transform.SetParent(transform, true);

        //Destroy the rigidbody so it is not effected by physics (shouldn't have parent and child both with their own rigidbodies..)
        Destroy(objectToSnap.GetComponent<Rigidbody>());

        //The VRTK_InteractableObject script sets its parent back to the previous parent (null) when we release the object. 
        //We want it to stay parented to the parent snappable object, so we set the previous parent manually here (bit of a bodge workaround..)
        objectToSnap.GetComponent<VRTK.VRTK_InteractableObject>().setPreviousParent(transform);

        //Set the local position to the snapPosition
        objectToSnap.transform.localPosition = snapPosition.localPosition;
        objectToSnap.transform.localRotation = snapPosition.localRotation;

        //Set the tag to something other than toy as we dont want to detect this on exiting conveyor anymore
        objectToSnap.tag = "Untagged";

        //Set snapped to true
        snapped = true;
        
        //Increment part counter
        partCounter++;

        //Check if we have all parts
        if(numParts == partCounter)
        {
            complete = true;

            //Particle celebration effect
            celebrationParticles.Play();
        }
    }

    public bool isComplete()
    {
        return complete;
    }
}