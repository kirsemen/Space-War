using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputGrid : MonoBehaviour
{
    public List<OutputGridElement> grid = new List<OutputGridElement>();
    public InputGrid IG;
    static public OutputGrid S1;

    public void Start()
    {
        S1 = this;
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).GetComponent<OutputGridElement>();
            grid.Add(child);
        }
    }
    public OutputGridElement Find(Vector2 pos)
    {
        foreach (var element in grid)
        {
            if (element.pos == pos)
            {
                return element;
            }
        }
        return null;
    }
}
