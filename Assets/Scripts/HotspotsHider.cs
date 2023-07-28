using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotsHider : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            HotspotController.ToggleAllHotspots();
            PortalController.ToggleAllPortals();
        }
    }
}
