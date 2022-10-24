using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBaseController : MonoBehaviour
{
    public KeyCode keyToOnEditing = KeyCode.E;
    public bool Editing = false;


    public GameObject InputSpaceship;
    public GameObject OutputSpaceship;


    private bool _EditingIsChanging = false;

    private void OnTriggerStay(Collider other)
    {   
        if (Input.GetKey(keyToOnEditing))
        {
            if (!_EditingIsChanging)
            {
                Editing = !Editing;
                _EditingIsChanging = true;
            }
        }
        else
        {
            _EditingIsChanging = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Editing = false;
    }
    private void Update()
    {
        if (Editing)
        {
            OutputSpaceship.SetActive(false);
            InputSpaceship.SetActive(true);

        }
        else
        {
            OutputSpaceship.SetActive(true);
            InputSpaceship.SetActive(false);

        }
    }
}
