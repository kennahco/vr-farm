using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconController : MonoBehaviour
{
    Transform cameraTransform;
    Transform beaconTransform;
    Renderer beaconRenderer;

    private const int hiddenDistance = 5;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        beaconRenderer = GetComponentInChildren<MeshRenderer>();
        beaconTransform = beaconRenderer.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float newAlpha = Mathf.Clamp((Vector3.Distance(cameraTransform.position, beaconTransform.position) - hiddenDistance) * 10, 0, 75) / 255;
        beaconRenderer.material.color = new Color(beaconRenderer.material.color.r, beaconRenderer.material.color.g, beaconRenderer.material.color.b, newAlpha);
    }
}
