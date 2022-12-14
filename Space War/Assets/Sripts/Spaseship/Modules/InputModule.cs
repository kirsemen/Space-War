using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class InputModule : MonoBehaviour
{

    public Vector2Int size = new Vector2Int(1, 1);
    public Vector3 margin = new Vector3(0, 0, 0);
    public Vector2Int gridPos = new Vector2Int(-1, -1);
    [HideInInspector] public Modules modules;
    InputGrid inputGrid;

    private void Start()
    {

        modules = transform.parent.parent.parent.GetComponent<Modules>();
        inputGrid = transform.parent.parent.parent.GetComponent<Modules>().inputGrid;
        if (Application.IsPlaying(gameObject))
        {
            if (modules.isBot)
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void Update()
    {
        if (!Application.IsPlaying(gameObject))
        {
            modules = transform.parent.parent.parent.GetComponent<Modules>();
            inputGrid = modules.inputGrid;
            try
            {
                transform.position = inputGrid.grid[gridPos.y, gridPos.x].transform.position + margin;
            }
            catch (System.Exception) { }

        }
        else
        {
            if (transform.parent.GetComponent<Module>() == modules.GetSelectedModule())
                transform.GetChild(1).gameObject.SetActive(true);
            else
                transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void SetPosByGrid()
    {
        if (gridPos.x != -1)
        {
            if (gridPos.y == -1)
            {
                Debug.LogError("gridPos.x != -1 and  gridPos.y == -1");
                return;
            }

            transform.position = inputGrid.grid[gridPos.y, gridPos.x].transform.position + margin;
        }
    }

    private Vector2Int GetGridPos1(int x, int y)
    {
        var position = new Vector2Int(x, y);
        if ((position + size).x <= inputGrid.maxSize.x && position.x >= 0 && (position + size).y <= inputGrid.maxSize.y && position.y >= 0)
        {
            bool isIn = false;
            foreach (var item in modules.Equipped)
            {
                var module = item.transform.GetChild(0).GetComponent<InputModule>();
                if (module == this) continue;
                if ((
                    ((position.x <= module.gridPos.x && module.gridPos.x <= (position.x + size.x - 1)) ||
                    (position.x <= (module.gridPos.x + module.size.x - 1) && (module.gridPos.x + module.size.x - 1) <= (position.x + size.x - 1))) &&
                    ((position.y <= module.gridPos.y && module.gridPos.y <= (position.y + size.y - 1)) ||
                    (position.y <= (module.gridPos.y + module.size.y - 1) && (module.gridPos.y + module.size.y - 1) <= (position.y + size.y - 1)))
                    ) || (
                    ((module.gridPos.x <= position.x && position.x <= (module.gridPos.x + module.size.x - 1)) ||
                    (module.gridPos.x <= (position.x + size.x - 1) && (position.x + size.x - 1) <= (module.gridPos.x + module.size.x - 1))) &&
                    ((module.gridPos.y <= position.y && position.y <= (module.gridPos.y + module.size.y - 1)) ||
                    (module.gridPos.y <= (position.y + size.y - 1) && (position.y + size.y - 1) <= (module.gridPos.y + module.size.y - 1)))
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

            for (int i = 0; i < size.x; i++)
                for (int j = 0; j < size.y; j++)
                    if (!inputGrid.grid[j + y, i + x].IsUnitedByType(this))
                    {
                        return new Vector2Int(-1, -1);
                    }

            return position;
        }
        return new Vector2Int(-1, -1);
    }


    private Vector2Int GetGridPos(Vector2Int pos)
    {
        pos -= this.size / 2;
        var size = this.size;

        for (int x = pos.x; x > pos.x - size.x; x--)
            for (int y = pos.y; y > pos.y - size.y; y--)
            {
                var position = GetGridPos1(x, y);
                if (position.x != -1)
                    return position;
            }
        for (int x = pos.x; x < pos.x + size.x + 1; x++)
            for (int y = pos.y; y > pos.y - size.y; y--)
            {
                var position = GetGridPos1(x, y);
                if (position.x != -1)
                    return position;
            }
        for (int x = pos.x; x > pos.x - size.x; x--)
            for (int y = pos.y; y < pos.y + size.y + 1; y++)
            {
                var position = GetGridPos1(x, y);
                if (position.x != -1)
                    return position;
            }
        for (int x = pos.x; x < pos.x + size.x + 1; x++)
            for (int y = pos.y; y < pos.y + size.y + 1; y++)
            {
                var position = GetGridPos1(x, y);
                if (position.x != -1)
                    return position;
            }

        return new Vector2Int(-1, -1);
    }

    public void ConnectModule(Vector2Int pos)
    {
        gridPos = GetGridPos(pos);
        if (gridPos.x != -1 && transform.parent.GetComponent<Module>().isEqiup == false)
        {
            transform.parent.GetComponent<Module>().isEqiup = true;
            modules.Equipped.Add(transform.parent.GetComponent<Module>());
            modules.Unequipped.Remove(transform.parent.GetComponent<Module>());
        }
        SetPosByGrid();
    }
}

