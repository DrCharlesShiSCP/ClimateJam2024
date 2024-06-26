using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time")] 
    [Tooltip("Day Length in Minutes")] 
    [SerializeField]
    private float _targetDayLength = 0.5f; // Length of day in minutes
    public float targetDayLength
    {
        get
        {
            return _targetDayLength;
        }
    }

    [SerializeField] 
    [Range(0f, 1f)] 
    private float _timeOfDay;
    public float timeOfDay
    {
        get
        {
            return _timeOfDay;
        }
    }

    [SerializeField] 
    private int _dayNumber = 0; // Tracks the days passed
    public int dayNumber
    {
        get
        {
            return _dayNumber;
        }
    }
    
    [SerializeField] 
    private int _yearNumber = 0;
    public int yearNumber
    {
        get
        {
            return _yearNumber;
        }
    }

    private float _timeScale = 100f; 
    // A conversion factor between realtime and game time, 24 hours long (scale 1), 1 hour long (scale 24)
    
    [SerializeField] 
    private int _yearLength = 100;
    public int yearLength
    {
        get
        {
            return _yearLength;
        }
    }

    public bool pause = false; // Pause the day night cycle without pausing the game

    [Header("Sun Light")] 
    [SerializeField] 
    private Transform dailyRotation;

    [SerializeField] 
    private Light sun;
    private float intensity;
    [SerializeField] private float sunBaseIntensity = 1;
    [SerializeField] private float sunVariation = 1.5f;
    [SerializeField] private Gradient sunColor;

    private void Update()
    {
        if (!pause)
        {
            UpdateTimeScale();
            UpdateTime();
        }
        AdjustSunRotation();
        SunIntensity();
    }

    private void UpdateTimeScale()
    {
        // This can change the target day length during runtime
        _timeScale = 24 / (_targetDayLength / 60);
    }

    private void UpdateTime()
    {
        // Time of the last frame, second in a day
        _timeOfDay += Time.deltaTime * _timeScale / 86400;
        if (_timeOfDay > 1) // New day and rest time of day, add to day number
        {
            _dayNumber++;
            _timeOfDay -= 1;
            if (_dayNumber > _yearLength) // New year
            {
                _yearNumber++;
                _dayNumber = 0;
            }
        }
    }

    // Rotates the sun daily (and seasonally)
    private void AdjustSunRotation()
    {
        float sunAngle = timeOfDay * 360f;
        dailyRotation.transform.localRotation = Quaternion.Euler(new Vector3(0f,0f,sunAngle));
    }

    private void SunIntensity()
    {
        intensity = Vector3.Dot(sun.transform.forward, Vector3.down);
        intensity = Mathf.Clamp01(intensity);
        sun.intensity = intensity * sunVariation + sunBaseIntensity;
    }

    private void AdjustSunColor()
    {
        sun.color = sunColor.Evaluate(intensity);
        
    }
    
}
