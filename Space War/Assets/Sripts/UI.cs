using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    static UI main;
    private void Awake()
    {
        main = this;
        gameObject.SetActive(false);
    }
    public static void SetActive(bool active)
    {
        main.gameObject.SetActive(active);
    }
}
