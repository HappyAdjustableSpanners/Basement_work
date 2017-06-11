using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VRTK.VRTK_InteractableObject))]
public class Snappable : MonoBehaviour
{

    /// <summary>
    /// Detects collision with another snappable object, and snaps them together, combining their meshes
    /// </summary>
    public bool snapped;

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

        //Set snapped to true
        snapped = true;
    }
}