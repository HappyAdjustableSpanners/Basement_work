using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VRTK.VRTK_InteractableObject))]
public class Snappable : MonoBehaviour
{

    /// <summary>
    /// Detects collision with another snappable object, and snaps them together, combining their meshes
    /// </summary>
    public bool parent;
    private SpawnParticleSystem spawnPS;
    public Vector3 forward = Vector3.forward;
    public string objId;

    void Start()
    {
        spawnPS = GetComponent<SpawnParticleSystem>();
        objId = transform.name;
    }

    //Snap edge has detected collision
    public void OnCollision(GameObject parent, GameObject child, Transform snapPosition)
    {
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
                }
            }
        }
        else
            spawnPS.Spawn(Color.red);
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
            //child.transform.localRotation = Quaternion.Euler(child.transform.localRotation.x, 0f, child.transform.localRotation.z);
        }
    }
}