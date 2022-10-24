using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GridElement[,] grid;
    public Vector2Int maxSize = new Vector2Int();
    static public Grid G1;
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).GetComponent<GridElement>();
            maxSize.x = child.position.x + 1 > maxSize.x ? child.position.x + 1 : maxSize.x;
            maxSize.y = child.position.y + 1 > maxSize.y ? child.position.y + 1 : maxSize.y;
        }

    }
    private void Start()
    {
        G1 = this;
        grid = new GridElement[maxSize.y, maxSize.x];
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).GetComponent<GridElement>();
            grid[child.position.y, child.position.x] = child;
        }
    }

}
