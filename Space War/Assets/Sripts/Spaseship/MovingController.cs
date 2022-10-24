using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingController : MonoBehaviour
{
    public GameObject Mesh;


    public float supportedSpeed = 0.5f;
    public float speed = 0;
    public float acceleration = 0.01f;
    public float deceleration = 0.005f;


    public float SpeedChanging = 0.001f;
    public float MaxSpeed = 0.5f;
    public float MinSpeed = -0.2f;
    public KeyCode keyUpSpeed = KeyCode.Q;
    public KeyCode keyDownSpeed = KeyCode.A;

    private bool _keyChangeSpeedDown = false;
    private bool _supportedSpeedIsO = false;

    void Update()
    {



        if (speed < supportedSpeed)
            speed = (speed + acceleration > supportedSpeed) ? supportedSpeed : speed + acceleration;
        else
            speed = (speed - deceleration < supportedSpeed) ? supportedSpeed : speed - deceleration;

        Rigidbody rb = GetComponent<Rigidbody>();


        rb.AddForce(Mesh.transform.forward * speed * 500);
        if (speed > 0)
        {
            if (rb.velocity.magnitude < speed)
                speed = rb.velocity.magnitude;
            if (rb.velocity.magnitude > speed)
                rb.velocity = Mesh.transform.forward * speed;
        }
        else
        {
            if (-rb.velocity.magnitude > speed)
                speed = -rb.velocity.magnitude;
            if (-rb.velocity.magnitude < speed)
                rb.velocity = Mesh.transform.forward * speed;
        }


        if (Input.GetKey(keyUpSpeed) && Input.GetKey(keyDownSpeed))
            _keyChangeSpeedDown = false;
        else if (Input.GetKey(keyUpSpeed))
        {
            if (!_supportedSpeedIsO || !_keyChangeSpeedDown)
                supportedSpeed += SpeedChanging;

            if (supportedSpeed / (supportedSpeed + SpeedChanging) < 0)
                _supportedSpeedIsO = true;
            else
                _supportedSpeedIsO = false;
            _keyChangeSpeedDown = true;
        }
        else if (Input.GetKey(keyDownSpeed))
        {
            if (!_supportedSpeedIsO || !_keyChangeSpeedDown)
                supportedSpeed -= SpeedChanging;

            if (supportedSpeed / (supportedSpeed - SpeedChanging) < 0)
                _supportedSpeedIsO = true;
            else
                _supportedSpeedIsO = false;
            _keyChangeSpeedDown = true;
        }
        else
            _keyChangeSpeedDown = false;


        if (supportedSpeed > -SpeedChanging && supportedSpeed < SpeedChanging)
        {
            supportedSpeed = 0;
            _supportedSpeedIsO = true;

        }




        if (supportedSpeed < MinSpeed)
            supportedSpeed = MinSpeed;
        else if (supportedSpeed > MaxSpeed)
            supportedSpeed = MaxSpeed;



    }
}
