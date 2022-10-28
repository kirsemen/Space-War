using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BezierPoint : MonoBehaviour
{
    [HideInInspector]
    public Vector3 SecondBezierPoint = Vector3.zero;
    private void OnDrawGizmos()
    {
        if (transform.parent.GetComponent<Bezier>().draw)
        {
            float SphereR = 0.03f;
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position - SecondBezierPoint, transform.position + SecondBezierPoint);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position + SecondBezierPoint, SphereR);
            Gizmos.DrawSphere(transform.position - SecondBezierPoint, SphereR);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, SphereR);

        }
    }
    private void Awake()
    {
        SecondBezierPoint = transform.up * transform.localScale.y;
    }
    private void Update()
    {
        if (!Application.IsPlaying(gameObject))
        {
            SecondBezierPoint = transform.up * transform.localScale.y;
        }
    }
}
