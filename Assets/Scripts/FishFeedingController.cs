using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFeedingController : MonoBehaviour
{
    public static FishFeedingController activeFishFeedingController;
    public ParticleSystem ps;
    public List<ParticleSystem.Particle> ToEat = new List<ParticleSystem.Particle>();

    private void Awake()
    {
        activeFishFeedingController = this;
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();

        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter, out var enterData);
        int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside, out var insideData);

        for (int i = 0; i < numEnter; i++)
        {
            bool isEaten = false;
            for (int j = 0; j < enterData.GetColliderCount(i); j++)
            {
                if (enterData.GetCollider(i, j) != ps.trigger.GetCollider(0))
                {
                    isEaten = true;
                    break;
                }
            }
            if(!isEaten)        
            {
                if(ToEat.FindIndex(particle => particle.randomSeed == enter[i].randomSeed) == -1)
                {
                    ParticleSystem.Particle p = enter[i];
                    ToEat.Add(p);
                    p.startLifetime = float.MaxValue;
                    p.remainingLifetime = float.MaxValue;
                    enter[i] = p;
                }
            }
        }

        for (int i = 0; i < numInside; i++)
        {
            bool isEaten = false;
            for (int j = 0; j < insideData.GetColliderCount(i); j++)
            {
                if (insideData.GetCollider(i, j) != ps.trigger.GetCollider(0))
                {
                    isEaten = true;
                    break;
                }
            }
            if (isEaten)
            {
                ParticleSystem.Particle p = inside[i];
                //Debug.Log(p.randomSeed + " Eaten");
                EatParticle(p);
                p.startLifetime = 0;
                p.remainingLifetime = 0;
                inside[i] = p;
            }
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
    }

    public void EatParticle(ParticleSystem.Particle particle)
    {
        //Debug.Log("Ate " + ToEat.FindAll(wParticle => particle.randomSeed == wParticle.randomSeed).Count);
        ToEat.RemoveAll(wParticle => particle.randomSeed == wParticle.randomSeed);
        FishEatingController.FoodEaten(particle);
    }
}
