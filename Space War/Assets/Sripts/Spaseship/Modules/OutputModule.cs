using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputModule : MonoBehaviour
{
    public Vector2 position = new Vector2(-1, -1);
    private void Update()
    {
        Vector2Int pos = transform.parent.GetChild(0).GetComponent<InputModule>().gridPos;
        Vector2Int size = transform.parent.GetChild(0).GetComponent<InputModule>().size;
        if (pos.x != -1)
        {
            position.x = pos.x + size.x / 2.0f;
            position.y = pos.y + size.y / 2.0f;

        }
        else
        {
            position.x = -1;
            position.y = -1;
        }
        if (OutputGrid.S1 != null)
        {
            var element = OutputGrid.S1.Find(position);
            if (element != null)
            {
                transform.position = element.transform.position;
                transform.rotation = element.transform.rotation;

            }
        }
        
    }
}
