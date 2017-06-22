using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappableEdge : MonoBehaviour {

    private Snappable snapBehaviour;
    public bool snapped = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("SnapEdge"))
        { 
            //Check if being grabbed
            if(transform.parent.GetComponent<VRTK.VRTK_InteractableObject>().IsGrabbed() || col.transform.parent.GetComponent<VRTK.VRTK_InteractableObject>().IsGrabbed())
            {
                if (!snapped && !col.GetComponent<SnappableEdge>().snapped)
                {
                    Transform snapPos;
                    GameObject child, parent;

                    //Make sure we call OnCollision from the parent if there is one
                    if(transform.parent.GetComponent<Snappable>().parent)
                    {
                        parent = gameObject;
                        child = col.gameObject;
                    }
                    else if( col.transform.parent.GetComponent<Snappable>().parent)
                    {
                        parent = col.gameObject;
                        child = gameObject;
                    }
                    else
                    {
                        parent = gameObject;
                        child = col.gameObject;
                    }

                    snapPos = parent.transform.Find(child.transform.parent.name + "_snapPos");
                    parent.transform.parent.GetComponent<Snappable>().OnCollision(parent, child.transform.parent.gameObject, snapPos);

                    //Set snapped to false
                    snapped = false;
                    col.GetComponent<SnappableEdge>().snapped = false;

                    //Remove triggers to improve performance
                    GetComponent<BoxCollider>().isTrigger = false;
                    col.GetComponent<BoxCollider>().isTrigger = false;
                }
            }
        }
    }
}
