using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public KeyCode keyToOnEditing = KeyCode.E;
    public bool Editing = false;


    private GameObject InputSpaceshipCam;
    private GameObject OutputSpaceship;

    [HideInInspector]
    public bool InSpacebaseTriger = false;

    public LayerMask SpasebaseLayer;

    private void Start()
    {
        InputSpaceshipCam = transform.GetChild(0).gameObject;
        OutputSpaceship = transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        if (GetComponent<Parametrs>().usingEnergy <= GetComponent<Parametrs>().energy&&transform.parent.GetComponent<NetworkObject>().IsOwner)
        {
            if (InSpacebaseTriger && Input.GetKeyDown(keyToOnEditing))
                Editing = !Editing;
            else if (!InSpacebaseTriger)
                Editing = false;
        }


        UI.SetActive(!Editing);
        if (Editing)
        {
            OutputSpaceship.SetActive(true);
            InputSpaceshipCam.SetActive(false);
        }
        else
        {
            OutputSpaceship.SetActive(false);
            InputSpaceshipCam.SetActive(true);
        }
    }

}
