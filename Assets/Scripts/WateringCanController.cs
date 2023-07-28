using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCanController : MonoBehaviour
{
    public float rotationDifferenceMargin;
    public new ParticleSystem particleSystem;
    public float maxWater = 100;
    
    public enum Direction
    {
        up, down, left, right, forward, backward
    }

    public Direction firstDirection;
    public Direction secondDirection;
    public float directionalRatio;

    [SerializeField]
    private float remainingWater;

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, Vector3.Lerp(transform.up, -transform.right, 0.5f),Color.red,10,false);
        if (IsTilted() && (remainingWater > 0 || maxWater == 0))
        {
            remainingWater -= Time.deltaTime;
            if (particleSystem.isStopped)
            {
                particleSystem.Play();
            }
        }
        else if(!particleSystem.isStopped)
        {
            particleSystem.Stop();
        }
    }

    bool IsTilted()
    {
        #region direction swtich-cases
        Vector3 direction1;
        Vector3 direction2;
        switch (firstDirection)
        {
            case Direction.up:
                direction1 = transform.up;
                break;
            case Direction.down:
                direction1 = -transform.up;
                break;
            case Direction.left:
                direction1 = -transform.right;
                break;
            case Direction.right:
                direction1 = transform.right;
                break;
            case Direction.forward:
                direction1 = transform.forward;
                break;
            case Direction.backward:
                direction1 = -transform.forward;
                break;
            default:
                direction1 = Vector3.zero;
                break;
        }
        switch (secondDirection)
        {
            case Direction.up:
                direction2 = transform.up;
                break;
            case Direction.down:
                direction2 = -transform.up;
                break;
            case Direction.left:
                direction2 = -transform.right;
                break;
            case Direction.right:
                direction2 = transform.right;
                break;
            case Direction.forward:
                direction2 = transform.forward;
                break;
            case Direction.backward:
                direction2 = -transform.forward;
                break;
            default:
                direction2 = Vector3.zero;
                break;
        }
        #endregion

        return Vector3.Angle(Vector3.Lerp(direction1, direction2, directionalRatio), Vector3.up) > rotationDifferenceMargin;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<WateringCanRefil>())
        {
            remainingWater = maxWater;
        }
    }
}
