using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VRTK.VRTK_InteractableObject))]
public class Snappable : MonoBehaviour
{

    /// <summary>
    /// Detects collision with another snappable object, and snaps them together, combining their meshes
    /// </summary>
    private SpawnParticleSystem spawnPS;

    //public Vector3 forward = Vector3.forward;

    void Start()
    {
        spawnPS = transform.parent.GetComponent<SpawnParticleSystem>();
    }

    //Snap edge has detected collision
    public void OnCollision(/*GameObject parent, GameObject child, Transform snapPosition*/ GameObject col)
    {
        /*
        //Convert 
        //Set parent of child to this object
        child.transform.SetParent(parent.transform, true);

        //Destroy the rigidbody so it is not effected by physics (shouldn't have parent and child both with their own rigidbodies..)
        Destroy(child.GetComponent<Rigidbody>());

        //The VRTK_InteractableObject script sets its parent back to the previous parent (null) when we release the object. 
        //We want it to stay parented to the parent snappable object, so we set the previous parent manually here (bit of a bodge workaround..)
        child.GetComponent<VRTK.VRTK_InteractableObject>().setPreviousParent(parent.transform);


        //Check if we are a parent, if we are it means there are snap positions available
        if (snapPosition)
        {
            if (GetComponent<SnappableParent>() != null)
            {
                //Transform snapPosition = transform.Find(child.name + "_snapPos");

                //Set the local position to the snapPosition             
                child.transform.localPosition = snapPosition.localPosition;
                child.transform.localRotation = snapPosition.localRotation;
                spawnPS.Spawn(Color.green);
                GetComponent<SnappableParent>().AddPart(child);              

                //Check if toy is complete
                if (GetComponent<SnappableParent>().isComplete())
                {
                    spawnPS.Spawn(Color.yellow);
                    EventManager.OnToyCompleted(gameObject);
                }
            }
        }
        else
            spawnPS.Spawn(Color.red);
            

        //Parent this to obj

        transform.SetParent(col.transform);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0f, 0f);

        transform.GetComponent<VRTK.VRTK_InteractableObject>().setPreviousParent(col.transform);
        Destroy(transform.GetComponent<Rigidbody>());

    */

        //Set rotation

        //Get the direction of the parent object which is equal to the direction of the snapPanel local vector


        



        //Get the forward direction of this 
    }

    public void SnapRayCast(GameObject snapEdge)
    {
        RaycastHit hit;
        if(Physics.Raycast(snapEdge.transform.position, snapEdge.transform.forward, out hit, 25f, LayerMask.NameToLayer("Toys"), QueryTriggerInteraction.Collide))
        {
            if(hit.collider.gameObject.tag.Contains("Toy"))
            {
                transform.rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
                transform.SetParent(snapEdge.transform);
                transform.GetComponent<VRTK.VRTK_InteractableObject>().setPreviousParent(snapEdge.transform);

                Destroy(transform.GetComponent<Rigidbody>());
            }
        }
    }
    public void SnapFixedJoint(GameObject snapObj, GameObject snapEdge, bool isCorrectPart)
    {
        //Make child
        //Transform previousParent = transform.parent;
        //snapObj.transform.SetParent(transform);
        //snapObj.transform.GetComponent<VRTK.VRTK_InteractableObject>().setPreviousParent(transform);
        //
        ////Remove parent
        //snapObj.transform.parent = null;
        //snapObj.transform.GetComponent<VRTK.VRTK_InteractableObject>().setPreviousParent(null);

        //Make connected object
        FixedJoint fj = snapObj.AddComponent<FixedJoint>();
        fj.connectedBody = GetComponent<Rigidbody>();

        //Force one hand to stop interacting
        snapObj.GetComponent<VRTK.VRTK_InteractableObject>().ForceStopInteracting();

        //check is correct part 
        if(isCorrectPart)
        {
            spawnPS.Spawn(Color.green, snapEdge.transform.position);

            //Increment num parts
            PartCounter partCounter = transform.parent.GetComponent<PartCounter>();
            if (partCounter != null)
            {
                partCounter.IncrementNumParts();

                if (partCounter.IsComplete())
                {
                    spawnPS.Spawn(Color.green, partCounter.getMainPart().transform.position);
                }
            }
        }
    }

    private void SnapPosition(Vector3 forward, GameObject child)
    {
        if (forward == Vector3.forward || forward == -Vector3.forward)
        {
            child.transform.localPosition = new Vector3(0f, 0f, child.transform.localPosition.z);
            //child.transform.localRotation = Quaternion.Euler(child.transform.localRotation.x, child.transform.localRotation.y, 0f);
        }
        else if (forward == Vector3.up || forward == -Vector3.up)
        {
            child.transform.localPosition = new Vector3(0f, child.transform.localPosition.y, 0f);
            //child.transform.localRotation = Quaternion.Euler(0f, child.transform.localRotation.y, child.transform.localRotation.z);
        }
    }

}