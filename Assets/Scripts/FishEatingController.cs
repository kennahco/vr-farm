using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEatingController : MonoBehaviour
{
    static List<FishEatingController> activeFish = new List<FishEatingController>();

    public Transform tankBounds;

    FishFeedingController fishFeedingController;
    ParticleSystem.Particle? targetParticle;
    Transform parentTransform;
    float lastEatTime;
    Coroutine coroutine;

    private const float DISTANCE_LIMIT = 100;
    private const float TURN_LIMIT = 360;
    private const float FISH_SPEED = 2;
    private const float EAT_PAUSE_TIME = 0.25f;
    private const float RETURN_THRESHOLD = 0.25f;
    private const float EAT_THRESHOLD = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        parentTransform = transform.parent;
        fishFeedingController = FishFeedingController.activeFishFeedingController;
    }

    public static void FoodEaten(ParticleSystem.Particle foodEaten)
    {
        foreach (var fish in activeFish)
        {
            if (foodEaten.randomSeed == fish.targetParticle.Value.randomSeed)
            {
                if (fish.lastEatTime + EAT_PAUSE_TIME < Time.time && Vector3.Distance(fish.targetParticle.Value.position, fish.transform.position) < EAT_THRESHOLD)
                {
                    fish.lastEatTime = Time.time;
                }
                fish.targetParticle = fish.GetNewTargetParticle();
            }
        }
    }

    private void OnEnable()
    {
        activeFish.Add(this);
    }

    private void OnDisable()
    {
        activeFish.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetParticle == null && fishFeedingController.ToEat.Count > 0)
        {
            targetParticle = GetNewTargetParticle();
        }

        //Move Towards Target Food OR Back Away After Eating
        if (targetParticle.HasValue)
        {
            if(lastEatTime + EAT_PAUSE_TIME < Time.time && Vector3.Distance(targetParticle.Value.position, transform.position) < RETURN_THRESHOLD)
            {
                lastEatTime = Time.time;
                //fishFeedingController.EatParticle(targetParticle.Value);
            }
            else
            {
                Vector3 relativePos = targetParticle.Value.position - transform.position;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(relativePos), TURN_LIMIT * Time.deltaTime);

                float distance;
                if(lastEatTime + EAT_PAUSE_TIME < Time.time)
                {
                    float angleMultiplier = Mathf.Clamp01((Quaternion.Angle(transform.rotation, Quaternion.LookRotation(relativePos)) / 90 * -1) + 1);
                    distance = Mathf.Min(FISH_SPEED * Time.deltaTime * angleMultiplier, Vector3.Distance(targetParticle.Value.position, transform.position));
                }
                else
                {
                    distance = FISH_SPEED * -0.5f * Time.deltaTime;
                }

                transform.position += (transform.forward * distance);
            }
        }
        //Move Towards 000,000 
        else
        {
            if(Vector3.Distance(parentTransform.position, transform.position) > RETURN_THRESHOLD)
            {
               Vector3 relativePos = parentTransform.position - transform.position;
                transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(relativePos), TURN_LIMIT * Time.deltaTime);

                float angleMultiplier = Mathf.Clamp01((Quaternion.Angle(transform.rotation, Quaternion.LookRotation(relativePos)) / 90 * -1) +1);
                transform.position += (transform.forward * Mathf.Min(FISH_SPEED * Time.deltaTime * angleMultiplier, Vector3.Distance(parentTransform.position, transform.position)));
            }
            else
            {
                if(transform.parent == null)
                {
                    transform.SetParent(parentTransform, true);
                }
                transform.localRotation = Quaternion.RotateTowards(this.transform.localRotation, Quaternion.identity, TURN_LIMIT * Time.deltaTime);
            }
        }

        //The bounding might not be needed (and isn't working)
        Vector3 boundedPosition = transform.position;
        boundedPosition.y = Mathf.Clamp(boundedPosition.x, tankBounds.position.x - tankBounds.localScale.x / 2, tankBounds.position.x + tankBounds.localScale.x / 2);
        boundedPosition.y = Mathf.Clamp(boundedPosition.y, tankBounds.position.y - tankBounds.localScale.y / 2, tankBounds.position.y + tankBounds.localScale.y / 2);
        boundedPosition.y = Mathf.Clamp(boundedPosition.z, tankBounds.position.z - tankBounds.localScale.z / 2, tankBounds.position.z + tankBounds.localScale.z / 2);

        //Debug using Debug.DrawRay()
        //transform.position = boundedPosition;
    }

    private ParticleSystem.Particle? GetNewTargetParticle()
    {
        ParticleSystem.Particle? targetParticle = null;
        foreach (var potenialTarget in fishFeedingController.ToEat)
        {
            if (!targetParticle.HasValue && Vector3.Distance(this.transform.position, potenialTarget.position) < DISTANCE_LIMIT)
            {
                targetParticle = potenialTarget;
            }
            else if(targetParticle.HasValue && Vector3.Distance(this.transform.position, targetParticle.Value.position) > Vector3.Distance(this.transform.position, potenialTarget.position))
            {
                targetParticle = potenialTarget;
            }
        }

        if(targetParticle != null)
        {
            transform.SetParent(null, true);
        }

        return targetParticle;
    }
}
