using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitConveyorBehaviour : MonoBehaviour {

    private GameController gameController;

	// Use this for initialization
	void Start () {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        //Only care about toys
        if (col.tag.Contains("Toy"))
        {
            //If we are the parent obj check if we are complete
            if (col.GetComponent<SnappableParent>())
            {
                if (col.GetComponent<SnappableParent>().isComplete())
                {
                    gameController.CompleteItemExitConveyor(col.gameObject);
                }
                else
                    gameController.IncompleteItemExitConveyor(col.gameObject);
            }
            else
                gameController.IncompleteItemExitConveyor(col.gameObject);
        }
    }
}
