using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public KeyCode keyToOnEditing = KeyCode.E;
    public bool Editing = false;


    private GameObject InputSpaceship;
    private GameObject OutputSpaceship;

    [HideInInspector]
    public bool InSpacebaseTriger = false;

    private void Start()
    {
        InputSpaceship =transform.GetChild(0).gameObject;
        OutputSpaceship = transform.GetChild(1).gameObject;
    }
    private void Update()
    {
        if (GetComponent<Parametrs>().usingEnergy > GetComponent<Parametrs>().energy)
            return;

        if(InSpacebaseTriger&& Input.GetKeyDown(keyToOnEditing))
            Editing = !Editing;
        else if(!InSpacebaseTriger)
            Editing = false;
        if (Editing)
        {
            OutputSpaceship.SetActive(true);
            InputSpaceship.SetActive(false);
        }
        else
        {
            OutputSpaceship.SetActive(false);
            InputSpaceship.SetActive(true);
        }
    }
    
}
