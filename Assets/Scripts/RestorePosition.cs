using BNG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Restores an object to its starting position after a configurable delay
public class RestorePosition : MonoBehaviour {

	//starting position of the object, nullable means it won't show in the inspector either. 
    public Vector3? initialPos = null;
	//starting rotation of the object, also nullable by the by. 
    public Vector3? initialRot = null;

    [Tooltip("Sets if the object uses local or world position to check & reset, good for child objects on moving things. ")]
    public bool useLocalPosition;

    [Tooltip("Prevents the object from restoring, often used temporarily")]
    public bool stopRestore;

    [Tooltip("The delay before an object restores, can be set to 0 for instant reseting")]
    public float restoreDelay = 10;

    //tracks if the object is scheduled to restore
    Coroutine coroutine; 
    //saved parent of the object
    Transform parent;
    Grabbable interactable;
    new Rigidbody rigidbody;

    [SerializeField]
    bool restoreAsKinematic = false;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();

        //listen for grab events
        interactable = GetComponent<Grabbable>();
    }

    //Clears any old coroutines that likely would've been broken by being disabled
    void OnEnable()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    public void SetStopRestore(bool newState)
    {
        stopRestore = newState;
    }

    // Update is called once per frame
    void Update () {
        if(rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody>();
            return;
        }
        //starting info is empty
        if(initialPos == null)
        {
            //and its come to a rest (or lacks physics)
            if(rigidbody == null || rigidbody.velocity.magnitude == 0)
            {
                //Store starting info
                ResetStartPosition();
            }
        }
        //restoring isn't prevented, its not being restored or grabbed.
        else if (!stopRestore && coroutine == null && interactable.GetPrimaryGrabber() == null)
        {
            //get its position
            Vector3 wPos;
            if (useLocalPosition)
            {
                wPos = transform.localPosition;
            }
            else
            {
                wPos = transform.position;
            }

            //margin for float wierdness
            float margin = 100f;

            //its far enough?
            if (!Mathf.Approximately((Mathf.Round(wPos.x * margin) / margin), (Mathf.Round(((Vector3)initialPos).x * margin) / margin)) || !Mathf.Approximately((Mathf.Round(wPos.y * margin) / margin), (Mathf.Round(((Vector3)initialPos).y * margin) / margin)) || !Mathf.Approximately((Mathf.Round(wPos.z * margin) / margin), (Mathf.Round(((Vector3)initialPos).z * margin) / margin)))
            {
                //schedule a restore
                coroutine = StartCoroutine(RestoreToInitial());
            }
        }
	}

    float currentDelay;

    //Wait for given delay and tries to restore the object, restarts until successful
    IEnumerator RestoreToInitial()
    {   
        currentDelay = restoreDelay;
        while(true)
        {   
            //Wait for given delay
            //yield return new WaitForSeconds(restoreDelay);
            if (!stopRestore && interactable.GetPrimaryGrabber() == null)
            {   
                currentDelay -= Time.deltaTime;
                if(currentDelay < 0)
                {
                    //Restore The Object
                    RestoreFunction();
                    break;
                }
            }
            else
            {
                currentDelay = restoreDelay;
            }
            yield return null;
        }
    }
    
    //Restore the object to starting values, stop any motion aswell
    public void RestoreFunction()
    {
        //Reset Parent (Should this be SetParent? I dunno I think Gabriel did it.)
        transform.parent = parent;
        //Reset ____ Position
        if (useLocalPosition)
        {   
            //Local
            transform.localPosition = (Vector3)initialPos;
        }
        else
        {
            //World
            transform.position = (Vector3)initialPos;
        }
        //Reset rotation
        transform.localEulerAngles = (Vector3)initialRot;
        if(rigidbody != null)
        {
            //Halt!
            rigidbody.isKinematic = restoreAsKinematic;
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.Sleep();
        }
        //The coroutine must be done (and unity doesn't track that)
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    //Save either starting world or local position, and starting local rotation
    public void ResetStartPosition()
    {
        //save ____ position
        if (useLocalPosition)
        { 
            //local
            initialPos = transform.localPosition;
        }
        else
        {
            //world
            initialPos = transform.position;
        }
        //and rotation too (only local)
        initialRot = transform.localEulerAngles;
    }
}
