using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Holds equipment on the player
public class EquipmentController : MonoBehaviour
{
    public static List<string> equippedItems = new List<string>();

    [Tooltip("This string must match the string in Equippable in order for them to interact")]
    public string equipmentType;

    [Tooltip("Equippable curently being equipped")]
    public Equippable equipped;

    public UnityEvent thisEquipped;

    private void Start()
    {
        if (equippedItems.Contains(equipmentType))
        {
            thisEquipped.Invoke();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //if something is equipped
        if (equipped != null)
        {
            //Don't let it move when not grabbed
            if (!equipped.wasGrabbed)
            {
                equipped.transform.localPosition = Vector3.zero;
                equipped.transform.localRotation = Quaternion.identity;
            }
            //Unequip it when its being grabbed
            else
            {
                UnEquip();
            }
        }
    }

    //Something is in the trigger area
    private void OnTriggerStay(Collider triggerObject)
    {
        Equippable equippable = triggerObject.GetComponentInParent<Equippable>();
        //Nothing is Equipped
        if (equipped == null &&
        //object is equippable
        equippable != null &&
        //Its matchs equiptmentTypes
        equippable.equipmentType == equipmentType &&
        //Its not being held
        !equippable.wasGrabbed &&
        //It was dropped, if it even needs that
        (!equippable.requireDropToEquip || equippable.wasDropped))
        {
            //Equip it
            Equip(equippable);
        }
    }

    private void OnTriggerEnter(Collider triggerObject)
    {
        Equippable equippable = triggerObject.GetComponentInParent<Equippable>();
        if (equippable != null)
        {
            //equippable.Highlight();
        }
    }

    private void OnTriggerExit(Collider triggerObject)
    {
        Equippable equippable = triggerObject.GetComponentInParent<Equippable>();
        if (equippable != null)
        {
            //equippable.UnHighlight();
        }
    }

    //Equips given equippable
    private void Equip(Equippable equipment)
    {
        //Call its equipped event
        equipment.onEquipped.Invoke();
        //Save it as equipped
        equipped = equipment;
        //Set as child
        Transform thisTransform = GetComponent<Transform>();
        equipped.transform.SetParent(thisTransform, false);
        //No physics please
        equipment.GetComponent<Rigidbody>().isKinematic = true;
        //Front and center
        equipped.transform.localPosition = Vector3.zero;
        equipped.transform.localRotation = Quaternion.identity;
        //Drop it if its hot
        if (equipment.unequipInstantly)
        {
            UnEquip();
        }
        //Or never let go
        else if (equipment.lockEquipped)
        {
            equipment.interactableObject.GetComponent<Collider>().enabled = false;
        }

        if(!equippedItems.Contains(equipmentType))
        {
            equippedItems.Add(equipmentType);
        }
        thisEquipped.Invoke();
    }

    //Unequips current equipable
    public void UnEquip()
    {
        //Call unequip event
        equipped.onUnEquipped.Invoke();
        //Destroy if it wants
        if (equipped.destroyOnUnequip)
        {
            Destroy(equipped.gameObject);
        }
        //forget it was ever here
        equipped = null;
    }
}