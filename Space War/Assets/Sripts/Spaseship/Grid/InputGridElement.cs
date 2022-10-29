using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGridElement : MonoBehaviour
{
    public Vector2Int position = new Vector2Int(0, 0);
    public Modules.Type type = new Modules.Type();

    private void Update()
    {
        if (type.GetTypeIndex().Length == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        if (IsEmpety())
        {
            Color oldColor = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.GetColor("_Color");
            oldColor.a = 1;
            transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", oldColor);
        }
        else
        {
            Color oldColor = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.GetColor("_Color");
            oldColor.a = 0.3f;
            transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", oldColor);
        }

        Module obj = Modules.S1.GetSelectedModule(9);
        if (obj != null)
        {
            InputModule module = obj.transform.GetChild(0).GetComponent<InputModule>();


            int[] indexs = obj.type.GetTypeIndex();
            Color newColor = new Color();
            foreach (var i in indexs)
                newColor += InputGrid.S1.colorForTypes[i] / indexs.Length;

            if (!IsUnitedByType(module))
            {
                newColor = new Color();
                Color color = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.GetColor("_Color");
                color.a = 0.3f;
                transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
            }

            Color oldColor = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.GetColor("_Color");
            oldColor.r = newColor.r; oldColor.g = newColor.g; oldColor.b = newColor.b;
            transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", oldColor);
        }
        else
        {
            Color oldColor = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.GetColor("_Color");
            oldColor.r = 0; oldColor.g = 0; oldColor.b = 0;
            transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", oldColor);
        }
    }
    private bool IsEmpety()
    {
        bool IsEmpety = true;
        foreach (var item in Modules.S1.Equipped)
        {
            InputModule IModule = item.transform.GetChild(0).GetComponent<InputModule>();
            for (int x = 0; x < IModule.size.x; x++)
                for (int y = 0; y < IModule.size.y; y++)
                {
                    Vector2Int pos = IModule.gridPos + new Vector2Int(x, y);
                    if (pos == position)
                    {
                        IsEmpety = false;
                        return IsEmpety;
                    }
                }
        }
        return IsEmpety;
    }


    public bool IsUnitedByType(InputModule module)
    {
        bool s = true;
        foreach (var item in module.transform.parent.GetComponent<Module>().type.GetTypeIndex())
        {
            bool include = false;
            foreach (var item1 in type.GetTypeIndex())
                if (item1 == item)
                {
                    include = true;
                    break;
                }
            if (!include)
            {
                s = false;
                break;
            }
        }
        return s;
    }

}
