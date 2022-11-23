using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingController : MonoBehaviour
{
    public GameObject Mesh;
    public Parametrs parametrs;

    public float supportedSpeed = 0;



    public float SpeedChanging = 0.001f;
    public KeyCode keyUpSpeed = KeyCode.Q;
    public KeyCode keyDownSpeed = KeyCode.A;

    public bool _supportedSpeedIsO = false;

    private void Update()
    {
        if (Input.GetKeyDown(keyUpSpeed) || Input.GetKeyDown(keyDownSpeed))
        {
            _supportedSpeedIsO = false;
        }
    }

    void FixedUpdate()
    {
        var rb = GetComponent<Rigidbody>();

        if (!_supportedSpeedIsO)
        {
            if (Input.GetKey(keyUpSpeed) && Input.GetKey(keyDownSpeed)) { }
            else if (Input.GetKey(keyUpSpeed))
                supportedSpeed += SpeedChanging * Time.deltaTime;
            else if (Input.GetKey(keyDownSpeed))
                supportedSpeed -= SpeedChanging * Time.deltaTime;
        }

        if (supportedSpeed > 0)
        {
            float k = 0.5f * Time.deltaTime;
            rb.velocity = rb.velocity * (1 - k) + rb.velocity.magnitude * Mesh.transform.forward * k;

            if (supportedSpeed > rb.velocity.magnitude)
                rb.velocity += Mesh.transform.forward * parametrs.acceleration * Time.deltaTime;
            else if (supportedSpeed < rb.velocity.magnitude)
                rb.velocity -= Mesh.transform.forward * parametrs.deceleration * Time.deltaTime;
        }
        else if (supportedSpeed < 0)
        {
            float k = 0.5f * Time.deltaTime;
            rb.velocity = rb.velocity * (1 - k) + rb.velocity.magnitude * -Mesh.transform.forward * k;

            if (Mathf.Abs(supportedSpeed) > rb.velocity.magnitude)
                rb.velocity -= Mesh.transform.forward * parametrs.deceleration * Time.deltaTime;
            else if (Mathf.Abs(supportedSpeed) < rb.velocity.magnitude)
                rb.velocity += Mesh.transform.forward * parametrs.acceleration * Time.deltaTime;
        }
        else
        {
            if(rb.velocity.magnitude!=0)
                rb.velocity -= rb.velocity.normalized * parametrs.acceleration * Time.deltaTime;
        }

        if (supportedSpeed > -0.0001f && supportedSpeed < 0.0001f)
        {
            supportedSpeed = 0;
            _supportedSpeedIsO = true;
        }



        if (supportedSpeed < -parametrs.MinSpeed)
            supportedSpeed = -parametrs.MinSpeed;
        else if (supportedSpeed > parametrs.MaxSpeed)
            supportedSpeed = parametrs.MaxSpeed;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        transform.parent.GetComponent<SpaceshipController>().InSpacebaseTriger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        transform.parent.GetComponent<SpaceshipController>().InSpacebaseTriger = false;
    }
}
