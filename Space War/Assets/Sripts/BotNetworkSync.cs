using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BotNetworkSync : NetworkBehaviour
{
    public NetworkVariable<Vector3> position = new NetworkVariable<Vector3>();
    public NetworkVariable<Quaternion> rotation = new NetworkVariable<Quaternion>();
    public NetworkVariable<ParametrsSync> parametrs = new NetworkVariable<ParametrsSync>();


    private void Update()
    {
        
        if (IsServer)
        {
            position.Value = transform.position;
            rotation.Value = transform.rotation;
            parametrs.Value.Set(GetComponent<Parametrs>());
        }
        else 
        {
            transform.position = position.Value;
            transform.rotation = rotation.Value;
        }
    }
}
public struct ParametrsSync
{
    public float HP;
    public float maxHP;

    public float shield;
    public float maxShield;
    public float speedRepairShield;

    public int energy;
    public int usingEnergy;

    public float MaxSpeed;
    public float MinSpeed;

    public float acceleration;
    public float deceleration;

    public float rotationSpeed;

    public ParametrsSync(Parametrs p)
    {
        HP = p.HP;
        maxHP = p.maxHP;

        shield = p.shield;  
        maxShield = p.maxShield;
        speedRepairShield = p.speedRepairShield;

        energy = p.energy;
        usingEnergy = p.usingEnergy;

        MaxSpeed = p.MaxSpeed;
        MinSpeed = p.MinSpeed;

        acceleration = p.acceleration; 
        deceleration = p.deceleration;

        rotationSpeed = p.rotationSpeed;
    }

    public void Set(Parametrs p)
    {
        HP = p.HP;
        maxHP = p.maxHP;

        shield = p.shield;
        maxShield = p.maxShield;
        speedRepairShield = p.speedRepairShield;

        energy = p.energy;
        usingEnergy = p.usingEnergy;

        MaxSpeed = p.MaxSpeed;
        MinSpeed = p.MinSpeed;

        acceleration = p.acceleration;
        deceleration = p.deceleration;

        rotationSpeed = p.rotationSpeed;
    }

}
