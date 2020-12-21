using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiParticleEffect : MonoBehaviour
{
    private List<ParticleSystem> _particles;

    public void Play()
    {
        foreach(var particles in _particles)
            particles.Play();
    }

    public void Stop()
    {
        foreach(var particles in _particles)
            particles.Stop();
    }

    private void Start()
    {
        _particles = new List<ParticleSystem>(GetComponentsInChildren<ParticleSystem>());    
    }
}
