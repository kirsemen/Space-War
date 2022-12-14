using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Parametrs : MonoBehaviour
{

    public Parametrs target;
    public int id = 0;

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

    public float rotationSpeed = 0;

    public static Parametrs FindTargetById(int id)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            try
            {
                if (go.GetComponent<Parametrs>().id == id)
                {
                    return go.GetComponent<Parametrs>();
                }
            }
            catch (System.Exception) { }
        }
        return null;
    }

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
                HP -= damage;
                shield -= damage;
                break;
            default:
                break;
        }
        check();
    }

    public void reset()
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

    public void Set(ParametrsSync p)
    {
        target = (p.targetId == -1) ? null : FindTargetById(p.targetId);
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

public struct ParametrsSync : INetworkSerializable
{
    public int targetId;

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

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref targetId);
        serializer.SerializeValue(ref HP);
        serializer.SerializeValue(ref maxHP);
        serializer.SerializeValue(ref shield);
        serializer.SerializeValue(ref maxShield);
        serializer.SerializeValue(ref speedRepairShield);
        serializer.SerializeValue(ref energy);
        serializer.SerializeValue(ref usingEnergy);
        serializer.SerializeValue(ref MaxSpeed);
        serializer.SerializeValue(ref acceleration);
        serializer.SerializeValue(ref deceleration);
        serializer.SerializeValue(ref rotationSpeed);
    }
    public ParametrsSync(Parametrs p)
    {
        targetId = (p.target == null) ? -1 : p.target.id;
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

//public struct MyComplexStruct : INetworkSerializable
//{
//    public Vector3 Position;
//    public Quaternion Rotation;

//    // INetworkSerializable
//    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
//    {
//        serializer.SerializeValue(ref Position);
//        serializer.SerializeValue(ref Rotation);
//    }
//    // ~INetworkSerializable
//}

class ParametrId
{
    private static int counter;
    public static int GetNewId()
    {
        return counter++;
    }
}
