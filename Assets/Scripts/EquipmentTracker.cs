using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentTracker : MonoBehaviour
{
    List<string> EquipmentTypes = new List<string>();

    public UnityEvent allEquipped;

    // Start is called before the first frame update
    void Awake  ()
    {
        EquipmentController.equippedItems.Clear();

        foreach (var equipmentController in GetComponentsInChildren<EquipmentController>())
        {   
            if (!EquipmentTypes.Contains(equipmentController.equipmentType))
            {
                EquipmentTypes.Add(equipmentController.equipmentType);
            }
            equipmentController.thisEquipped.AddListener(CheckForAllEquipped);
        }
    }

    void CheckForAllEquipped()
    {
        foreach (var EquipmentType in EquipmentTypes)
        {
            if(!EquipmentController.equippedItems.Contains(EquipmentType))
            {
                return;
            }
        }

        allEquipped.Invoke();
    }
}
