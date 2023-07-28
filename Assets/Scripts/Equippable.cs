using BNG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Equippable object to fit into Equipment Controllers
public class Equippable : MonoBehaviour {

    //called when object is equipped
    public UnityEvent onEquipped;
    //called when object is unequipped
    public UnityEvent onUnEquipped;
    [Tooltip("VRTK_InteractableObject that controls the object, will be auto populated if one exits on the same object as the script")]
    public Grabbable interactableObject;

    [Tooltip("This string must match the string in Equipment Controller inorder for them to interact")]
    public string equipmentType;

    [Tooltip("The equippable will call its equip event, unequip, then fall off")]
    public bool unequipInstantly;
    [Tooltip("The equippable will be destroyed when taken off, good pair with unequipInstantly")]
    public bool destroyOnUnequip;
    [Tooltip("Require the equippable to be dropped last frame in order to equip")]
    public bool requireDropToEquip;
    [Tooltip("Prevent the equippable from being removed (by the player)")]
    public bool lockEquipped;


    //is the object being held?
    [HideInInspector]
    public bool wasGrabbed;

    //was the object being held last frame, and now isn't
    [HideInInspector]
    public bool wasDropped;

    void Start()
    {
        //Acquire interactableObject
        if (interactableObject == null)
        {
            interactableObject = GetComponent<Grabbable>();
        }
    }

    private void Update()
    {
        if(interactableObject.GetPrimaryGrabber() == null && wasGrabbed)
        {
            wasDropped = true;
            StartCoroutine(ResetWasDropped());
            wasGrabbed = false;
        }
        else if(interactableObject.GetPrimaryGrabber() != null)
        {
            wasGrabbed = true;
        }
    }

    //resets WasDropped a frame later
    private IEnumerator ResetWasDropped()
    {
        yield return null;
        yield return null;
        wasDropped = false;
    }
}
