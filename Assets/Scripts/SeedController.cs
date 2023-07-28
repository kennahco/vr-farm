using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SeedController : MonoBehaviour
{
    public UnityEvent CompleteEvent;
    public float seedsRequired;

    public float seedsPlanted;

    void OnParticleTrigger()
    {
        seedsPlanted++;
        if(seedsPlanted > seedsRequired)
        {
            Complete();
        }
    }

    void Complete()
    {
        CompleteEvent.Invoke();
    }
}
