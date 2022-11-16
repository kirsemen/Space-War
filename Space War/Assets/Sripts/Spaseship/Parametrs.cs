using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parametrs : MonoBehaviour
{
    
    public Parametrs target;

    public float HP = 0;
    public float maxHP = 0;

    public float shield = 0;
    public float maxShield = 0;
    public float speedRepairShield = 0;

    public int energy = 0;
    public int usingEnergy = 0;

    public float MaxSpeed = 0;
    public float MinSpeed = 0;

    public float acceleration = 0;
    public float deceleration = 0;

    public float rotationSpeed=0;

    private void Update()
    {
        shield += speedRepairShield * Time.deltaTime;
        check();
    }
    public void check()
    {
        if (HP > maxHP)
            HP = maxHP;
        else if (HP < 0)
            HP = 0;
        if (shield > maxShield)
            shield = maxShield;
        else if (shield < 0)
            shield = 0;
    }

    public void GetDamage(float damage, Weaponry.DamageType type = Weaponry.DamageType.pinpoint)
    {
        switch (type)
        {
            case Weaponry.DamageType.pinpoint:
                if (shield >= damage)
                    shield -= damage;
                else
                {
                    shield = 0;
                    HP -= damage - shield;
                }
                break;
            case Weaponry.DamageType.penetrating:
                HP-=damage;
                shield -= damage;
                break;
            default:
                break;
        }
        check();
    }

    public void Reset()
    {
        HP = 0;
        maxHP = 0;

        shield = 0;
        maxShield = 0;
        speedRepairShield = 0;

        energy = 0;
        usingEnergy = 0;

        MaxSpeed = 0;
        MinSpeed = 0;

        acceleration = 0;
        deceleration = 0;

        rotationSpeed = 0;
    }
}
