using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormControl : MonoBehaviour
{
    [SerializeField] 
    private ParticleSystem ThunderParticle;

    [SerializeField] 
    private ParticleSystem RainParticle;

    public bool PlayRain;
    public bool PlayThunder;
    private bool IsPlayingThunder;
    private bool IsPlayingRain;

    private void Start()
    {
        PlayThunder = false;
        PlayRain = false;
        IsPlayingRain = false;
        IsPlayingThunder = false;
        ThunderParticle.Stop();
        RainParticle.Stop();
    }

    void Update()
    {
        if (PlayThunder && !IsPlayingThunder)
        {
            PlayThunderParticle();
        }
        else if (!PlayThunder && IsPlayingThunder)
        {
            StopThunderParticle();
        }

        if (PlayRain && !IsPlayingRain)
        {
            PlayRainParticle();
        }
        else if (!PlayRain && IsPlayingRain)
        {
            StopPlayRainParticle();
        }
    }

    private void PlayThunderParticle()
    {
        ThunderParticle.Play();
        IsPlayingThunder = true;
    }

    private void StopThunderParticle()
    {
        ThunderParticle.Stop();
        IsPlayingThunder = false;
    }

    private void PlayRainParticle()
    {
        RainParticle.Play();
        IsPlayingRain = true;
    }

    private void StopPlayRainParticle()
    {
        RainParticle.Play();
        IsPlayingRain = false;
    }
}
