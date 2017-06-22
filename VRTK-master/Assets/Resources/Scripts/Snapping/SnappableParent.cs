using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VRTK.VRTK_InteractableObject))]
public class SnappableParent : MonoBehaviour
{

    /// <summary>
    /// We only want one of the snapped objects to do this behaviour, so have separated it into another script
    /// </summary>
    public float numParts;
    private bool complete = false;
    private float partCounter = 0;

    void Start()
    { 
    }

    public void AddPart(GameObject objectToSnap)
    {
        if (objectToSnap.CompareTag(gameObject.tag))
        {
            partCounter++;

            if (partCounter >= numParts)
            {
                complete = true;
            }
        }
    }

    public bool isComplete()
    {
        return complete;
    }


}