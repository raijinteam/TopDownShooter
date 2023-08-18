using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RageType
{
    Duration,
    Damage,
    Firerate,
    KillRequirement
}

[System.Serializable]
public class UltimatePowerupData 
{
    public RageType rageType;
    public float[] amount;
}
