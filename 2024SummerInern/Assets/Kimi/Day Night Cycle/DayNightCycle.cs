using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

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
    private float elapsedTime;

    [SerializeField] 
    private Text clockText;

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
    
    // Time Curve Variables here
    [SerializeField] 
    private AnimationCurve timeCurve;
    private float timeCurveNormalization;

    [Header("Sun Light")] 
    [SerializeField] 
    private Transform dailyRotation;

    [SerializeField] 
    private Light sun;
    private float intensity;
    [SerializeField] private float sunBaseIntensity = 1;
    [SerializeField] private float sunVariation = 1.5f;
    [SerializeField] private Gradient sunColor;


    [Header("Seasonal Variables")] 
    [SerializeField]
    private Transform sunSeasonalRotation;
    [SerializeField] 
    [Range(-45f, 45f)] 
    private float maxSeasonalTilt;

    [Header("Modules")] 
    private List<DN_ModuleBase> moduleList = new List<DN_ModuleBase>();

    private void Start()
    {
        NormalizeTimeCurve();
    }

    private void Update()
    {
        if (!pause)
        {
            UpdateTimeScale();
            UpdateTime();
            UpdateClock();
        }
        AdjustSunRotation();
        SunIntensity();
        AdjustSunColor();
        UpdateModules(); // Will update modules each frame
    }

    private void UpdateTimeScale()
    {
        // This can change the target day length during runtime
        _timeScale = 24 / (_targetDayLength / 60);

        _timeScale *= timeCurve.Evaluate(timeOfDay); // changes timescale based on time curve
        _timeScale /= timeCurveNormalization; // keeps day length at target value
    }

    private void NormalizeTimeCurve()
    {
        float stepSize = 0.01f; // Numerical Integration
        int numberSteps = Mathf.FloorToInt(1f / stepSize);
        float curveTotal = 0;

        for (int i = 0; i < numberSteps; i++)
        {
            curveTotal += timeCurve.Evaluate(i * stepSize);
        }

        timeCurveNormalization = curveTotal / numberSteps;
    }

    private void UpdateTime()
    {
        // Time of the last frame, second in a day
        _timeOfDay += Time.deltaTime * _timeScale / 86400;
        elapsedTime = Time.deltaTime;
        if (_timeOfDay > 1) // New day and rest time of day, add to day number
        {
            elapsedTime = 0;
            _dayNumber++;
            _timeOfDay -= 1;
            if (_dayNumber > _yearLength) // New year
            {
                _yearNumber++;
                _dayNumber = 0;
            }
        }
    }

    private void UpdateClock()
    {
        float time = elapsedTime / (targetDayLength * 60); // between 0 and 1
        float hour = Mathf.FloorToInt(time * 24);
        float minute = Mathf.FloorToInt((time * 24) - hour) * 60;

        clockText.text = hour.ToString() + " : " + minute.ToString();
    }

    // Rotates the sun daily (and seasonally)
    private void AdjustSunRotation()
    {
        float sunAngle = timeOfDay * 360f;
        dailyRotation.transform.localRotation = Quaternion.Euler(new Vector3(0f,0f,sunAngle));

        float seasonalAngle = -maxSeasonalTilt * Mathf.Cos(dayNumber / _yearLength * 2f * Mathf.PI);
        sunSeasonalRotation.localRotation = Quaternion.Euler(new Vector3(seasonalAngle, 0f, 0f));
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

    public void AddModule(DN_ModuleBase module)
    {
        moduleList.Add(module);
    }

    public void RemoveModule(DN_ModuleBase module)
    {
        moduleList.Remove(module);
    }

    // Update each module based on current sun intensity
    private void UpdateModules()
    {
        foreach (DN_ModuleBase module in moduleList)
        {
            module.UpdateModule(intensity);
        }
    }
}
