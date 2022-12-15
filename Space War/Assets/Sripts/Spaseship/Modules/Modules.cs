using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Modules : MonoBehaviour
{
    [System.Serializable]
    public class Type
    {
        [Flags]
        public enum _Type
        {
            weaponry = 0b1,
            armor = 0b10,
            engine = 0b100,
            generator = 0b1000,
            shield = 0b10000
        };
        public _Type type;
        public int[] GetTypeIndex()
        {
            List<int> output = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                if ((Convert.ToInt32(type) >> i) % 2 == 1 || (Convert.ToInt32(type) == -1))
                {
                    output.Add(i);
                }
            }
            return output.ToArray();
        }
    }


    public List<Module> Equipped;
    public List<Module> Unequipped;

    public bool isBot = false;
    public InputGrid inputGrid;
    public OutputGrid outputGrid;
    public Parametrs parametrs;

    public LayerMask selectedModuleLayer;

    private void Update()
    {
        for (int i = 0; i < Equipped.Count; i++)
            if (Equipped[i].transform.GetChild(0).GetComponent<InputModule>().gridPos.x == -1)
            {
                Unequipped.Add(Equipped[i]);
                Equipped.Remove(Equipped[i]);
            }

        for (int i = 0; i < Unequipped.Count; i++)
            if (Unequipped[i].transform.GetChild(0).GetComponent<InputModule>().gridPos.x != -1)
            {
                Equipped.Add(Unequipped[i]);
                Unequipped.Remove(Unequipped[i]);
            }

        foreach (var obj in Equipped)
        {
            obj.transform.parent = transform.GetChild(0);
            obj.transform.GetComponent<Module>().isEqiup = true;
        }
        foreach (var obj in Unequipped)
        {
            obj.transform.parent = transform.GetChild(1);
            obj.transform.GetComponent<Module>().isEqiup = false;
        }
        UpdateParametrs();
    }


    public Module GetSelectedModule()
    {
        int layer= (int)Mathf.Log(selectedModuleLayer, 2);

        foreach (var item in Equipped)
        {
            if (item.transform.GetChild(0).GetChild(0).gameObject.layer == layer)
            {
                return item;
            }
        }
        foreach (var item in Unequipped)
        {
            if (item.transform.GetChild(0).GetChild(0).gameObject.layer == layer)
            {
                return item;
            }
        }
        return null;
    }

    public void UpdateParametrs()
    {
        parametrs.reset();
        foreach (var item in Equipped)
        {
            parametrs.HP += item.GetComponent<DefaultType>().HP;
            parametrs.maxHP += item.GetComponent<DefaultType>().HP;
            parametrs.usingEnergy += item.GetComponent<DefaultType>().EnergyConsumption;
            switch (item.type.type)
            {
                case Type._Type.engine:
                    parametrs.MaxSpeed += item.GetComponent<Engine>().MaxSpeed;
                    parametrs.MinSpeed += item.GetComponent<Engine>().MinSpeed;
                    parametrs.acceleration += item.GetComponent<Engine>().acceleration;
                    parametrs.deceleration += item.GetComponent<Engine>().deceleration;
                    parametrs.rotationSpeed += item.GetComponent<Engine>().rotationSpeed;
                    break;
                case Type._Type.generator:
                    parametrs.energy += item.GetComponent<Generator>().EnergyProduction;
                    break;
                case Type._Type.shield:
                    parametrs.shield += item.GetComponent<Shield>().shield;
                    parametrs.maxShield += item.GetComponent<Shield>().shield;
                    parametrs.speedRepairShield += item.GetComponent<Shield>().speedRepairShield;
                    break;
                default:
                    break;
            }
        }

    }

}
