using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappableEdge : MonoBehaviour {

    SnappableParent parent;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("SnapEdge"))
        { 
            //Check if being grabbed
            if(transform.parent.GetComponent<VRTK.VRTK_InteractableObject>().IsGrabbed() && col.transform.parent.GetComponent<VRTK.VRTK_InteractableObject>().IsGrabbed()
                && transform.parent.GetComponent<SnappableParent>() && col.transform.parent.GetComponent<Snappable>())
            {
                parent = transform.parent.GetComponent<SnappableParent>();
                parent.OnCollision(col.transform.parent.gameObject);
                //GetComponent<BoxCollider>().isTrigger = false;
                //col.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }
}
