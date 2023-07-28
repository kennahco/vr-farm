using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrenchController : MonoBehaviour
{
    public Material dugMaterial;
    public UnityEvent TrenchDug;

    private MeshRenderer meshRenderer;
    private float lastHitTime;
    private float collisionThreshold = 0.5f;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "DiggableTerrain")
        {
            lastHitTime = Time.time;
        }
    }

    private void Update()
    {
        if(Time.time > lastHitTime + collisionThreshold)
        {
            TrenchCleared();
        }
    }

    private void TrenchCleared()
    {
        meshRenderer.material = dugMaterial;
        TrenchDug.Invoke();
        this.enabled = false;
    }
}
