using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputGridElement : MonoBehaviour
{
    public Vector2 pos = new Vector2(0, 0);
    private void OnDrawGizmosSelected()
    {
        float GizmosSize = 0.1f;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, GizmosSize);
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.up* GizmosSize*1.5f + transform.position);
        Gizmos.DrawRay(transform.position, transform.up*0.5f);
    }
}
