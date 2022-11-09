using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingController : MonoBehaviour
{
    public GameObject Mesh;


    public float supportedSpeed = 0;
    public float MaxSpeed = 3f;
    public float MinSpeed = 1.5f;

    public float acceleration = 0.3f;
    public float deceleration = 0.15f;


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
                rb.velocity += Mesh.transform.forward * acceleration * Time.deltaTime;
            else if (supportedSpeed < rb.velocity.magnitude)
                rb.velocity -= Mesh.transform.forward * deceleration * Time.deltaTime;
        }
        else if (supportedSpeed < 0)
        {
            float k = 0.5f * Time.deltaTime;
            rb.velocity = rb.velocity * (1 - k) + rb.velocity.magnitude * -Mesh.transform.forward * k;

            if (Mathf.Abs(supportedSpeed) > rb.velocity.magnitude)
                rb.velocity -= Mesh.transform.forward * deceleration * Time.deltaTime;
            else if (Mathf.Abs(supportedSpeed) < rb.velocity.magnitude)
                rb.velocity += Mesh.transform.forward * acceleration * Time.deltaTime;
        }

        if (supportedSpeed > -0.0001f && supportedSpeed < 0.0001f)
        {
            supportedSpeed = 0;
            _supportedSpeedIsO = true;
        }



        if (supportedSpeed < -MinSpeed)
            supportedSpeed = -MinSpeed;
        else if (supportedSpeed > MaxSpeed)
            supportedSpeed = MaxSpeed;

        if (rb.velocity.magnitude > supportedSpeed && supportedSpeed > 0)
            rb.velocity = Mesh.transform.forward * supportedSpeed;
        else if (rb.velocity.magnitude < supportedSpeed && supportedSpeed < 0)
            rb.velocity = -Mesh.transform.forward * supportedSpeed;


        //var rb = GetComponent<Rigidbody>();

        //if (speed < supportedSpeed)
        //    speed = (speed + acceleration > supportedSpeed) ? supportedSpeed : speed + acceleration;
        //else
        //    speed = (speed - deceleration < supportedSpeed) ? supportedSpeed : speed - deceleration;



        //rb.AddForce(Mesh.transform.forward * speed * 500);
        //if (speed > 0)
        //{
        //    if (rb.velocity.magnitude < speed)
        //        speed = rb.velocity.magnitude;
        //    if (rb.velocity.magnitude > speed)
        //        rb.velocity = Mesh.transform.forward * speed;
        //}
        //else
        //{
        //    if (-rb.velocity.magnitude > speed)
        //        speed = -rb.velocity.magnitude;
        //    if (-rb.velocity.magnitude < speed)
        //        rb.velocity = Mesh.transform.forward * speed;
        //}


        //if (Input.GetKey(keyUpSpeed) && Input.GetKey(keyDownSpeed))
        //    _keyChangeSpeedDown = false;
        //else if (Input.GetKey(keyUpSpeed))
        //{
        //    if (!_supportedSpeedIsO || !_keyChangeSpeedDown)
        //        supportedSpeed += SpeedChanging;

        //    if (supportedSpeed / (supportedSpeed + SpeedChanging) < 0)
        //        _supportedSpeedIsO = true;
        //    else
        //        _supportedSpeedIsO = false;
        //    _keyChangeSpeedDown = true;
        //}
        //else if (Input.GetKey(keyDownSpeed))
        //{
        //    if (!_supportedSpeedIsO || !_keyChangeSpeedDown)
        //        supportedSpeed -= SpeedChanging;

        //    if (supportedSpeed / (supportedSpeed - SpeedChanging) < 0)
        //        _supportedSpeedIsO = true;
        //    else
        //        _supportedSpeedIsO = false;
        //    _keyChangeSpeedDown = true;
        //}
        //else
        //    _keyChangeSpeedDown = false;


        //if (supportedSpeed > -acceleration && supportedSpeed < acceleration)
        //{
        //    supportedSpeed = 0;
        //    _supportedSpeedIsO = true;

        //}




        //if (supportedSpeed < MinSpeed)
        //    supportedSpeed = MinSpeed;
        //else if (supportedSpeed > MaxSpeed)
        //    supportedSpeed = MaxSpeed;



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
