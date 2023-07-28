using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneSelectorSpawner : MonoBehaviour
{
    public GameObject sceneSelector;
    public GameObject activeSceneSelector;
    public InputActionReference spawnerInputAction;

    public Transform headTransform;
    public bool wasPressed;

    private void Update()
    {
        if(spawnerInputAction.action.ReadValue<float>() > 0.5)
        {
            if(!wasPressed)
            {
                SpawnSceneSelector();
            }
            wasPressed = true;
        }
        else
        {
            wasPressed = false;
        }

    }

    public void SpawnSceneSelector()
    {
        if(activeSceneSelector != null)
        {
            Destroy(activeSceneSelector);
        }

        var newSceneSelector = Instantiate(sceneSelector);
        newSceneSelector.transform.position = headTransform.position + headTransform.forward;
        newSceneSelector.transform.LookAt(headTransform);
        newSceneSelector.transform.Rotate(Vector3.up, 180);
        activeSceneSelector = newSceneSelector;
    }
}
