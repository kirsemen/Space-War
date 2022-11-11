using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour
{
    public bool isEqiup = false;

    public Modules.Type type = new Modules.Type();


    private void Awake()
    {
        if (transform.GetChild(0).GetComponent<InputModule>().gridPos.x == -1)
            transform.parent.parent.GetComponent<Modules>().Unequipped.Add(this);
        else
            transform.parent.parent.GetComponent<Modules>().Equipped.Add(this);
    }
    private void Update()
    {
        transform.GetChild(1).gameObject.SetActive(isEqiup);
    }
}
