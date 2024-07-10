using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class LightFlicker : MonoBehaviour
{
    [SerializeField] private Light ThunderLight;

    [Range(0, 2)]
    public float MinTime;

    [Range(0, 1)] 
    public float MaxTime;

    public float Timer;

    private void Start()
    {
        Timer = Random.Range(MinTime, MaxTime);
    }

    private void Update()
    {
        
    }

    private void FlickeringLight()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }

        if (Timer <= 0)
        {
            ThunderLight.enabled = !ThunderLight.enabled;
            Timer = Random.Range(MinTime, MaxTime);
        }
    }
}
