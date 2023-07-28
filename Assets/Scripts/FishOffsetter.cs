using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishOffsetter : MonoBehaviour
{
    public Vector3 offsetRange;
    public float offsetTimeToReach;
    public float chanceToMove;

    private Vector3 targetOffset;
    private Vector3 startingPosition;
    private Coroutine coroutine;
    private bool wasStopped;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
        { 
            if(wasStopped == true)
            {
                wasStopped = false;
                //startingPosition = transform.localPosition - targetOffset;
            }
            if(coroutine == null & Random.Range(0f,100f) < chanceToMove)
            {
                //Randomize offset
                targetOffset.x = offsetRange.x * Random.Range(-1f, 1f);
                targetOffset.y = offsetRange.y * Random.Range(-1f, 1f);
                targetOffset.z = offsetRange.z * Random.Range(-1f, 1f);
                //Move to new position
                coroutine = StartCoroutine(Lerper());
            }
        }
        else
        {
            wasStopped = true;
        }
    }

    private IEnumerator Lerper()
    {
        float startTime = Time.time;
        Vector3 currentPosition = transform.localPosition;
        Vector3 targetPosition = startingPosition + targetOffset;
        float fracComplete = 0;
        while (fracComplete < 1)
        {
            yield return 0;

            // The fraction of the animation that has happened so far is
            // equal to the elapsed time divided by the desired time for
            // the total journey.
            fracComplete = (Time.time - startTime) / offsetTimeToReach;

            transform.localPosition = Vector3.Lerp(currentPosition, targetPosition, fracComplete);
        }
        coroutine = null;
    }
}
