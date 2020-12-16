using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    public ParticleSystem[] allParitcles;
    public float lifeTime = 1f;

    private void Start()
    {
        allParitcles = GetComponentsInChildren<ParticleSystem>();
        Destroy(gameObject, lifeTime);
    }

    public void Play()
    {
        foreach (ParticleSystem particle in allParitcles)
        {
            particle.Stop();
            particle.Play();
        }
    }
}
