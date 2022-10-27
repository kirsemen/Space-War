using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGridElement : MonoBehaviour
{
    public Vector2Int position = new Vector2Int(0, 0);
    public Modules.Type type = new Modules.Type();

    private void Update()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);

        Module obj = Modules.S1.GetSelectedModule(9);
        if (obj != null)
        {
            InputModule module = obj.transform.GetChild(0).GetComponent<InputModule>();
            if (module.gridPos.x != -1)
                for (int i = 0; i < module.size.x; i++)
                    for (int j = 0; j < module.size.y; j++)
                        if ((module.gridPos.x + i) == position.x && (module.gridPos.y + j) == position.y)
                        {
                            transform.GetChild(0).gameObject.SetActive(true);
                            transform.GetChild(1).gameObject.SetActive(false);
                        }


            int[] indexs = obj.type.GetTypeIndex();
            Color newColor = new Color();
            foreach (var i in indexs)
                newColor += InputGrid.S1.colorForTypes[i] / indexs.Length;

            Color oldColor = transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.GetColor("_Color");
            oldColor.r = newColor.r; oldColor.g = newColor.g; oldColor.b = newColor.b;
            transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", oldColor);
            if (!IsUnitedByType(module))
            {
                transform.GetChild(1).gameObject.SetActive(false);
                newColor = new Color();
            }
            oldColor = transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().material.GetColor("_Color");
            oldColor.r = newColor.r; oldColor.g = newColor.g; oldColor.b = newColor.b;
            transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", oldColor);
        }
        else
        {
            Color oldColor = transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.GetColor("_Color");
            oldColor.r = 0; oldColor.g = 0; oldColor.b = 0;
            transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", oldColor);
            oldColor = transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().material.GetColor("_Color");
            oldColor.r = 0; oldColor.g = 0; oldColor.b = 0;
            transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", oldColor);
        }
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
