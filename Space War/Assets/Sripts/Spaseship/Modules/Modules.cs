using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modules : MonoBehaviour
{
    public List<Module> Equipped;
    public List<Module> Unequipped;
    static public Modules S1;
    private void Start()
    {
        S1 = this;
    }
    private void Update()
    {
        for (int i = 0; i < Equipped.Count; i++)
            if (Equipped[i].gridPos.x == -1)
            {
                Unequipped.Add(Equipped[i]);
                Equipped.Remove(Equipped[i]);
            }
        for (int i = 0; i < Unequipped.Count; i++)
            if (Unequipped[i].gridPos.x != -1)
            {
                Equipped.Add(Unequipped[i]);
                Unequipped.Remove(Unequipped[i]);
            }

        foreach (var module in Equipped)
        {
            module.transform.parent = transform.GetChild(0);
            module.isEqiup = true;
        }
        foreach (var module in Unequipped)
        {
            module.transform.parent = transform.GetChild(1);
            module.isEqiup = false;
        }
    }
    public void EquipModule(Module module)
    {
        if (module.gridPos.x != -1 && module.isEqiup == false)
        {
            module.isEqiup = true;
            Equipped.Add(module);
            Unequipped.Remove(module);
        }

    }
    private Vector2Int GetGridPos(Module module, Vector2Int pos)
    {
        pos -= module.size / 2;
        var size = module.size;
        size -= module.size / 2;

        if (pos.x < 0)
            pos.x = 0;
        if (pos.y < 0)
            pos.y = 0;
        for (int x = pos.x; x > pos.x - size.x; x--)
            for (int y = pos.y; y > pos.y - size.y; y--)
            {
                var position = new Vector2Int(x, y);
                if ((position + module.size).x <= Grid.G1.maxSize.x && position.x >= 0 && (position + module.size).y <= Grid.G1.maxSize.y && position.y >= 0)
                {
                    bool isIn = false;
                    foreach (var item in Equipped)
                    {
                        if (item == module) continue;
                        if ((
                            ((position.x <= item.gridPos.x && item.gridPos.x <= (position.x + module.size.x - 1)) ||
                            (position.x <= (item.gridPos.x + item.size.x - 1) && (item.gridPos.x + item.size.x - 1) <= (position.x + module.size.x - 1))) &&
                            ((position.y <= item.gridPos.y && item.gridPos.y <= (position.y + module.size.y - 1)) ||
                            (position.y <= (item.gridPos.y + item.size.y - 1) && (item.gridPos.y + item.size.y - 1) <= (position.y + module.size.y - 1)))
                            ) || (
                            ((item.gridPos.x <= position.x && position.x <= (item.gridPos.x + item.size.x - 1)) ||
                            (item.gridPos.x <= (position.x + module.size.x - 1) && (position.x + module.size.x - 1) <= (item.gridPos.x + item.size.x - 1))) &&
                            ((item.gridPos.y <= position.y && position.y <= (item.gridPos.y + item.size.y - 1)) ||
                            (item.gridPos.y <= (position.y + module.size.y - 1) && (position.y + module.size.y - 1) <= (item.gridPos.y + item.size.y - 1)))
                            ))
                        {
                            isIn = true;
                            break;
                        }
                    }
                    if (isIn)
                    {
                        return new Vector2Int(-1, -1);
                    }



                    return position;
                }
            }
        return new Vector2Int(-1, -1);
    }

    public void ConnectModule(Module module, Vector2Int pos)
    {
        module.gridPos = GetGridPos(module, pos);
        if (module.gridPos.x != -1)
        {
            for (int i = 0; i < module.size.x; i++)
                for (int j = 0; j < module.size.y; j++)
                {
                    Grid.G1.grid[j + module.gridPos.y, i + module.gridPos.x].gameObject.transform.Find("Cube").gameObject.SetActive(true);
                    Grid.G1.grid[j + module.gridPos.y, i + module.gridPos.x].gameObject.transform.Find("Plane").gameObject.SetActive(false);
                }
        }
        EquipModule(module);
        module.SetPosByGrid();
    }
}
