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
            generator = 0b1000
        };
        public _Type type;
        public int[] GetTypeIndex()
        {
            List<int> output = new List<int>();
            for (int i = 0; i < 4; i++)
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
    public bool isBot=false;
    public InputGrid inputGrid;
    public OutputGrid outputGrid;


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
    }


    public Module GetSelectedModule(int layer)
    {
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

}
