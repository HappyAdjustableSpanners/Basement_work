using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitConveyorBehaviour : MonoBehaviour {
  
    // Use this for initialization
    void Start () {
	}

    void OnTriggerEnter(Collider col)
    {
        //Only care about toys
        if (col.tag == "Toy")
        {
            if (IsToyComplete(col.gameObject))
            {
                EventManager.OnCompleteItemExitedConveyor(col.gameObject);
            }
            else
                EventManager.OnIncompleteItemExitedConveyor(col.gameObject);
        }
    }       

    private bool IsToyComplete(GameObject toy)
    {
        //If we are the parent obj check if we are complete
        if (toy.transform.root.GetComponent<PartCounter>().IsComplete())
        {
            return true;
        }
        else
            return false;
    }
}

