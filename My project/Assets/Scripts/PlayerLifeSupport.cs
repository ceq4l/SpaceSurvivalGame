using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeSupport : MonoBehaviour
{
    //Static Instance
    public static PlayerLifeSupport instance;

    [Header("Settings")]
    public float OxygenLevels;
    public float PowerLevels;

    public float OxygenDecay;
    public float PowerDecay;

    [Header("UI References")]
    public Slider OxygenSlider;
    public Slider PowerSlider;

    //Private Variables
    float CurrentOxygen;
    float CurrentPower;

    private void Start()
    {
        //Assign References
        instance = this;

        //Update Starting Levels
        CurrentOxygen = OxygenLevels;
        CurrentPower = PowerLevels;

        //Update Slider Maxes
        OxygenSlider.maxValue = OxygenLevels;
        PowerSlider.maxValue = PowerLevels;
    }

    private void Update()
    {
        //Take Oxygen and Power Overtime
        CurrentOxygen -= OxygenDecay * Time.deltaTime;
        CurrentPower -= PowerDecay * Time.deltaTime;

        //Clamp The levels
        CurrentOxygen = Mathf.Clamp(CurrentOxygen, 0, OxygenLevels);
        CurrentPower = Mathf.Clamp(CurrentPower, 0, PowerLevels);

        //Update The UI
        UpdateUI();
    }

    void UpdateUI()
    {
        OxygenSlider.value = CurrentOxygen;
        PowerSlider.value = CurrentPower;
    }

    public void AddOxygen(float Amount)
    {
        CurrentOxygen += Amount;
    }

    public void AddPower(float Amount)
    {
        CurrentPower += Amount;
    }
}