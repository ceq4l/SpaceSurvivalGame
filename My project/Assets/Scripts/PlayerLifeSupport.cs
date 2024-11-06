using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public float HealthLevels;

    public float OxygenDecay;
    public float PowerDecay;

    public float AddedPowerDecay;

    [Header("UI References")]
    public Slider HealthSlider;
    public TMP_Text HealthDisplay;
    public Slider OxygenSlider;
    public TMP_Text OxygenDisplay;
    public Slider PowerSlider;
    public TMP_Text PowerDisplay;

    //Private Variables
    float CurrentOxygen;
    float CurrentPower;
    float CurrentHealth;

    private void Start()
    {
        //Assign References
        instance = this;

        //Update Starting Levels
        CurrentOxygen = OxygenLevels;
        CurrentPower = PowerLevels;
        CurrentHealth = HealthLevels;

        //Update Slider Maxes
        OxygenSlider.maxValue = OxygenLevels;
        PowerSlider.maxValue = PowerLevels;
        HealthSlider.maxValue = HealthLevels;
    }

    private void Update()
    {
        if (PlayerMovement.Instance.HasGravity)
            AddedPowerDecay = 0f;
        else
            AddedPowerDecay = 0.35f;

        //Take Oxygen and Power Overtime
        CurrentOxygen -= OxygenDecay * Time.deltaTime;
        CurrentPower -= (PowerDecay + AddedPowerDecay) * Time.deltaTime;

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
        HealthSlider.value = CurrentHealth;

        OxygenDisplay.text = Mathf.Round(CurrentOxygen).ToString() + "/" + OxygenSlider.maxValue;
        PowerDisplay.text = Mathf.Round(CurrentPower).ToString() + "/" + PowerSlider.maxValue;
        HealthDisplay.text = Mathf.Round(CurrentHealth).ToString() + "/" + HealthSlider.maxValue;
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