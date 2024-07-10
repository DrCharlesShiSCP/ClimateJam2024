using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderControl : MonoBehaviour
{
    [SerializeField] 
    private ParticleSystem ThunderParticle;

    [SerializeField] 
    private ParticleSystem RainParticle;
    
    public bool PlayThunder;
    public bool PlayRain;
    private bool HasPlayedThunder;
    private bool HasPlayedRain;
    

    private void Start()
    {
        PlayThunder = false;
        PlayRain = false;
        HasPlayedThunder = false;
        HasPlayedRain = false;
        ThunderParticle.Stop();
        RainParticle.Stop();
    }

    void Update()
    {
        if (PlayThunder && !HasPlayedThunder)
        {
            PlayThunderParticle();
        }
        else if (!PlayThunder && HasPlayedThunder)
        {
            StopThunderParticle();
        }
        
        if (PlayRain && !HasPlayedRain)
        {
            PlayRainParticle();
        }
        else if (!PlayRain && HasPlayedRain)
        {
            StopRainParticle();
        }
    }

    // Thunder
    private void PlayThunderParticle()
    {
        ThunderParticle.Play();
        HasPlayedThunder = true;
    }

    private void StopThunderParticle()
    {
        ThunderParticle.Stop();
        HasPlayedThunder = false;
    }
    
    // Rain
    private void PlayRainParticle()
    {
        RainParticle.Play();
        HasPlayedRain = true;
    }

    private void StopRainParticle()
    {
        RainParticle.Stop();
        HasPlayedRain = false;
    }
}
