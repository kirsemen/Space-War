using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GridElement : MonoBehaviour
{
    public Vector2Int position = new Vector2Int(0, 0);
    public Modules.Type type = new Modules.Type();

    private void Update()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);

        var obj = Modules.S1.GetSelectedModule(9);
        if (obj != null)
        {
            var module = obj.GetComponent<Module>();
            if (module.gridPos.x != -1)
                for (int i = 0; i < module.size.x; i++)
                    for (int j = 0; j < module.size.y; j++)
                        if ((module.gridPos.x + i) == position.x && (module.gridPos.y + j) == position.y)
                        {
                            transform.GetChild(0).gameObject.SetActive(true);
                            transform.GetChild(1).gameObject.SetActive(false);
                        }
            
            if (!IsUnitedByType(module))
            {
                transform.GetChild(1).gameObject.SetActive(false);
            }


        }
    }
    public bool IsUnitedByType(Module module)
    {
        bool s = true;
        foreach (var item in module.type.GetTypeIndex())
        {
            bool include = false;
            foreach (var item1 in type.GetTypeIndex())
                if (item1 == item)
                {
                    include = true; break;

                }
            if (!include)
            {
                s = false; break;

            }
        }
        return s;
    }

}
