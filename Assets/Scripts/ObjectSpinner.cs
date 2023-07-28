using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Spins an object (with Slerp) by a set amount in a set amount of time
public class ObjectSpinner : MonoBehaviour
{
    [Tooltip("Adds this to the current rotation of the object")]
    public Vector3 amountToSpin;

    [Tooltip("The time it takes to complete the rotation")]
    public float timeToSpin;

    [Tooltip("Object to spin, auto poulates with this objects transform if none are given")]
    public Transform transformToSpin;

    public UnityEvent onStartSpin;

    //Keeps track of if the spin is happening
    private Coroutine coroutine;

    public bool canSpin = true;
    public bool neverStopSpinning;
    public bool startSpinnning;

    private bool isSpinningForever;

    // Use this for initialization
    private void Start()
    {
        if (transformToSpin == null)
        {
            transformToSpin = this.transform;
        }
        if (startSpinnning)
        {
            Spin();
        }
    }

    private void Update()
    {
        if(isSpinningForever)
        {
            transformToSpin.Rotate(amountToSpin * Time.deltaTime);
        }
    }

    public void Spin()
    {
        if (canSpin)
        {
            if (neverStopSpinning)
            {
                isSpinningForever = true;
            }
            else if (timeToSpin <= 0)
            {
                transformToSpin.Rotate(amountToSpin);
                onStartSpin.Invoke();
            }
            else if (coroutine == null)
            {
                coroutine = StartCoroutine(Slerper());
                onStartSpin.Invoke();
            }
            else
            {
                Debug.Log(name + " is spinning, calm down!");
            }
        }
    }

    private IEnumerator Slerper()
    {
        float startTime = Time.time;
        Vector3 startRotation = transformToSpin.localEulerAngles;
        Vector3 endRotation = transformToSpin.localEulerAngles + amountToSpin;
        float fracComplete = 0;
        while (fracComplete < 1)
        {
            yield return 0;

            // The fraction of the animation that has happened so far is
            // equal to the elapsed time divided by the desired time for
            // the total journey.
            fracComplete = (Time.time - startTime) / timeToSpin;

            transformToSpin.localEulerAngles = Vector3.Slerp(startRotation, endRotation, fracComplete);
        }
        coroutine = null;
    }
}