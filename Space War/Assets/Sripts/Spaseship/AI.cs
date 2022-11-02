using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject target;
    public float MeshSoftRotation = 0.005f;

    private void Start()
    {
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            
        }

    }
    private void Update()
    {
        if (target != null)
        {
            transform.GetChild(0).rotation = Quaternion.Lerp(transform.GetChild(0).rotation, 
                Quaternion.LookRotation(target.transform.position - transform.GetChild(0).position),  
                MeshSoftRotation);
            Vector3 eulerRotation = transform.GetChild(0).rotation.eulerAngles;
            transform.GetChild(0).rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);

        }
    }
}
