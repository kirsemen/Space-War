using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    public Vector2Int size = new Vector2Int(1, 1);
    public Vector3 margin = new Vector3(0, 0, 0);
    public Vector2Int gridPos = new Vector2Int(-1, -1);
    public bool isEqiup = false;

    public Modules.Type type = new Modules.Type();

    private void Start()
    {
        if (gridPos.x == -1)
            transform.parent.parent.GetComponent<Modules>().Unequipped.Add(this);
        else
            Modules.S1.EquipModule(this);
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
            transform.position = Grid.S1.grid[gridPos.y, gridPos.x].transform.position + margin;
        }
    }
}
