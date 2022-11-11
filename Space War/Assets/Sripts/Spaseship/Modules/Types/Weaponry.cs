using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponry : DefaultType
{
    public enum DamageType
    {
        pinpoint,
        penetrating
    }
    public float damage = 0;
    public DamageType damageType = DamageType.pinpoint;

}
