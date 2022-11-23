using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Parametrs parametrs;

    public float RadiusToTarget = 4;

    public LayerMask EnemyMasks;

    private void Update()
    {
        if (parametrs.target == null)
        {
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            Parametrs nowTorget=null;
            float distance=float.MaxValue;
            foreach (GameObject go in allObjects)
            {
                if (((1 << go.layer) & EnemyMasks) != 0 && go!=gameObject/* && go.transform.parent.GetComponent<Parametrs>()!=null*/)
                {
                    Debug.Log(go.transform.parent.name);
                    float newDistance = Vector3.Distance(transform.position, go.transform.position);
                    if (distance < newDistance)
                    {
                        distance = newDistance;
                        nowTorget = go.transform.parent.GetComponent<Parametrs>();
                    }
                }
            }
            parametrs.target = nowTorget;
        }

        if (parametrs.rotationSpeed != 0 && parametrs.target != null)
        {
            var _MeshCurentAngle = transform.rotation.eulerAngles;
            var targetAngle = Quaternion.LookRotation(parametrs.target.transform.GetChild(0).position - transform.position).eulerAngles;
            var deltaMeshAngle = (Quaternion.Inverse(Quaternion.Euler(_MeshCurentAngle)) * Quaternion.Euler(targetAngle)).eulerAngles;

            float x = deltaMeshAngle.x % 360 <= 180 ? deltaMeshAngle.x % 360 : -180 + deltaMeshAngle.x % 180;
            float y = deltaMeshAngle.y % 360 <= 180 ? deltaMeshAngle.y % 360 : -180 + deltaMeshAngle.y % 180;

            x = Mathf.Abs(x) < parametrs.rotationSpeed ? x : parametrs.rotationSpeed * (Mathf.Abs(x) / x);
            y = Mathf.Abs(y) < parametrs.rotationSpeed ? y : parametrs.rotationSpeed * (Mathf.Abs(y) / y);

            Vector3 eulerRotation = (transform.rotation * Quaternion.Euler(x, y, 0)).eulerAngles;
            transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);

        }
    }
    private void FixedUpdate()
    {
        if (parametrs.target != null)
        {
            var rb = GetComponent<Rigidbody>();


            var Distance = Vector3.Distance(transform.position, parametrs.target.transform.GetChild(0).position);
            if (RadiusToTarget < Distance)
            {
                float k = 0.5f * Time.deltaTime;
                rb.velocity = rb.velocity * (1 - k) + rb.velocity.magnitude * transform.forward * k;

                float R1 = Distance - Mathf.Pow((rb.velocity.magnitude) / (parametrs.deceleration), 2) * (parametrs.deceleration) / 2 - RadiusToTarget;
                if (R1 > 0)
                    rb.velocity += transform.forward * parametrs.acceleration * Time.deltaTime;
                else
                    rb.velocity -= transform.forward * parametrs.deceleration * Time.deltaTime;

            }
            else
            {
                float k = 0.5f * Time.deltaTime;
                rb.velocity = rb.velocity * (1 - k) - rb.velocity.magnitude * transform.forward * k;

                float R1 = Distance + Mathf.Pow((rb.velocity.magnitude) / (parametrs.acceleration), 2) * (parametrs.acceleration) / 2 - RadiusToTarget;
                if (R1 > 0)
                    rb.velocity += transform.forward * parametrs.acceleration * Time.deltaTime;
                else
                    rb.velocity -= transform.forward * parametrs.deceleration * Time.deltaTime;
            }

            if (rb.velocity.magnitude > parametrs.MaxSpeed)
                rb.velocity = transform.forward * parametrs.MaxSpeed;


        }
    }
}
