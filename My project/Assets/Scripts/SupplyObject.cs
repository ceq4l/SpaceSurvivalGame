using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyObject : MonoBehaviour
{
    public float OxygenAddAmount;
    public float PowerAddAmount;

    public void Interact()
    {
        PlayerLifeSupport.instance.AddOxygen(OxygenAddAmount);
        PlayerLifeSupport.instance.AddPower(PowerAddAmount);
    }
}
