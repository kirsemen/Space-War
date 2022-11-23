using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class InputGrid : MonoBehaviour
{
    public InputGridElement[,] grid;
    public Vector2Int maxSize = new Vector2Int(2,5);
    public Color[] colorForTypes = new Color[5];
    public Modules modules;


    [InspectorButton("OnButtonClicked")]
    public bool Generate;
    public GameObject Prefab;

    private void OnButtonClicked()
    {
        int i = 0;
        while (i < 100)
        {
            if (transform.childCount == 0)
                break;
            Object.DestroyImmediate(transform.GetChild(0).gameObject);
            i++;
        }
        for (int x = 0; x < maxSize.x; x++)
            for (int y = 0; y < maxSize.y; y++)
            {
                GameObject go = PrefabUtility.InstantiatePrefab(Prefab) as GameObject;
                go.transform.parent = transform;
                go.GetComponent<InputGridElement>().position=new Vector2Int(x,y);
                go.transform.localPosition = new Vector3(x, -0.49f, -y);
                go.name = "x: " + x + ", y:" + y;
            }

        
    }
    public void Start()
    {
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
    private void Update()
    {
        if (!Application.IsPlaying(gameObject)|| maxSize.x!=0)
        {
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

}
