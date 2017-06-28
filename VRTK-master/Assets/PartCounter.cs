using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartCounter : MonoBehaviour {

    public int numParts;
    public GameObject mainPart;

    private int currentNumParts = 0;
    private bool complete = false;

    public void IncrementNumParts()
    {
        if(currentNumParts <= numParts && complete == false)
        {
            currentNumParts++;

            if(currentNumParts == numParts)
            {
                complete = true;
            }
        }
    }

    public bool IsComplete()
    {
        return complete;
    }

    public GameObject getMainPart()
    {
        return mainPart;
    }
}
