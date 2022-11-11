using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject target;
    public Parametrs parametrs;

    [Delayed] public float MeshSoftRotation = 0.5f;

    public float RadiusToTarget = 4;

    public bool _supportedSpeedIsO = false;


    private void Update()
    {

        var _MeshCurentAngle = transform.rotation.eulerAngles;
        var targetAngle = Quaternion.LookRotation(target.transform.position - transform.position).eulerAngles;
        var deltaMeshAngle = (Quaternion.Inverse(Quaternion.Euler(_MeshCurentAngle)) * Quaternion.Euler(targetAngle)).eulerAngles;
        float x = deltaMeshAngle.x % 360 <= 180 ? deltaMeshAngle.x % 360 : -180 + deltaMeshAngle.x % 180;
        float y = deltaMeshAngle.y % 360 <= 180 ? deltaMeshAngle.y % 360 : -180 + deltaMeshAngle.y % 180;

        x = Mathf.Abs(x) < MeshSoftRotation ? x : MeshSoftRotation * (Mathf.Abs(x) / x);
        y = Mathf.Abs(y) < MeshSoftRotation ? y : MeshSoftRotation * (Mathf.Abs(y) / y);

        Vector3 eulerRotation = (transform.rotation * Quaternion.Euler(x, y, 0)).eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            var rb = GetComponent<Rigidbody>();

            float k = 0.5f * Time.deltaTime;
            rb.velocity = rb.velocity * (1 - k) + rb.velocity.magnitude * transform.forward * k;

            var Distance = Vector3.Distance(transform.position, target.transform.position);
            if (RadiusToTarget < Distance)
            {
                _supportedSpeedIsO = false;
                float R1 = Distance - Mathf.Pow((rb.velocity.magnitude) / (parametrs.deceleration), 2) * (parametrs.deceleration) / 2 - RadiusToTarget;
                if (R1 > 0)
                    rb.velocity += transform.forward * parametrs.acceleration * Time.deltaTime;
                else
                    rb.velocity -= transform.forward * parametrs.deceleration * Time.deltaTime;

            }
            else if (!_supportedSpeedIsO)
                rb.velocity -= transform.forward * parametrs.deceleration * Time.deltaTime;




            if (rb.velocity.magnitude > -0.0001f && rb.velocity.magnitude < 0.0001f)
            {
                rb.velocity = Vector3.zero;
                _supportedSpeedIsO = true;
            }
            if (rb.velocity.magnitude > parametrs.MaxSpeed)
                rb.velocity = transform.forward * parametrs.MaxSpeed;


        }
    }
}
