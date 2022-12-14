using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputModule : MonoBehaviour
{
    public Vector2 position = new Vector2(-1, -1);
     OutputGrid outputGrid;
    private void Start()
    {
        outputGrid = transform.parent.parent.parent.GetComponent<Modules>().outputGrid;
    }

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
        if (outputGrid != null)
        {
            
            var element = outputGrid.Find(position);
            if (element != null)
            {
                transform.position = element.transform.position;
                transform.localRotation = element.transform.rotation;

            }
        }
        
    }
}
