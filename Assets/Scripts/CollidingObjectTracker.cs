using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollidingObjectTracker : MonoBehaviour
{
    public List<GameObject> collidingObjects = new List<GameObject>();
    public List<GameObject> targetObjects = new List<GameObject>();
    public bool requireAll;
    public bool containedLastFrame;
    public UnityEvent containsTarget;

    private void OnTriggerStay(Collider other)
    {
        if (!collidingObjects.Contains(other.gameObject))
        {
            collidingObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        collidingObjects.RemoveAll((item) => item == other.gameObject);
    }

    private void Update()
    {
        if(targetObjects.Count > 0)
        {
            foreach (var item in targetObjects)
            {
                if (collidingObjects.Contains(item))
                {
                    if (!requireAll)
                    {
                        containedLastFrame = true;
                        containsTarget.Invoke();
                        return;
                    }
                }
                else
                {
                    if (requireAll)
                    {
                        containedLastFrame = false;
                        return;
                    }
                }
            }
            if (requireAll)
            {
                containedLastFrame = true;
                containsTarget.Invoke();
            }
            else
            {
                containedLastFrame = false;
            }
        }
    }

    private void OnDisable() {
        collidingObjects.Clear();
    }
}
