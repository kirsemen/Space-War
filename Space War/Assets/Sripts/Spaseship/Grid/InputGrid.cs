using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputGrid : MonoBehaviour
{
    public InputGridElement[,] grid;
    public Vector2Int maxSize = new Vector2Int();
    public Color[] colorForTypes = new Color[4];
    static public InputGrid S1;


    public void Start()
    {
        S1 = this;
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).GetComponent<InputGridElement>();
            maxSize.x = child.position.x + 1 > maxSize.x ? child.position.x + 1 : maxSize.x;
            maxSize.y = child.position.y + 1 > maxSize.y ? child.position.y + 1 : maxSize.y;
        }
        grid = new InputGridElement[maxSize.y, maxSize.x];
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).GetComponent<InputGridElement>();
            grid[child.position.y, child.position.x] = child;
        }
    }

}
