using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class InputCameraController : MonoBehaviour
{
    public GameObject Mesh;
    public GameObject Q;

    public GameObject E;

    public float CameraSoftRotation = 0.03f;
    private float size = 1;
    private float CuretSize = 1;

    public float SpeedSizing = 0.1f;
    public float MinSize = 0.5f;
    public float MaxSize = 100;
    public float SizeSoftSizing = 0.01f;


    private Vector2 _mousePosition = new Vector2(0, 0);
    private bool _mouseIsDown = false;

    private Vector3 CameraAngle = new Vector3(0, 0, 0);
    private Vector3 _CameraCurentAngle = new Vector3(0, 0, 0);



    void Update()
    {
        transform.rotation = Mesh.transform.rotation;


        if (Q != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int a = (1 << 10);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, a))
            {
                var G = hit.collider.gameObject.transform.parent;
                Q.transform.position = ray.direction * hit.distance * 0.7f + transform.GetChild(0).position;
                Modules.S1.ConnectModule(Q.GetComponent<Module>(), G.GetComponent<GridElement>().position);
            }
            else
            {
                Q.GetComponent<Module>().gridPos.x = -1;
                Q.GetComponent<Module>().gridPos.y = -1;

                Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                int a1 = (1 << 8);
                RaycastHit hit1;
                if (Physics.Raycast(ray1, out hit1, 100, a1))
                    Q.transform.position = ray1.direction * hit1.distance * 0.7f + transform.GetChild(0).position;

                else
                    Q.transform.position = ray.direction * 9 + transform.GetChild(0).position;


            }

            if (Input.GetMouseButtonDown(0))
            {
                Modules.S1.EquipModule(Q.GetComponent<Module>());

                Q.layer = 8;
                Q = null;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                int a = 1 << 8;
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, a))
                {
                    Q = hit.collider.gameObject;
                    Q.layer = 9;
                }

            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            _mouseIsDown = true;
            _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(1))
        {
            _mouseIsDown = false;
        }
        if (_mouseIsDown)
        {
            Vector2 mouseDelta = _mousePosition - new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            CameraAngle = new Vector3(mouseDelta.y / 10, -mouseDelta.x / 10, 0) + CameraAngle;
            CameraAngle.x = CameraAngle.x < -22 ? -22 : (CameraAngle.x > 44 ? 44 : CameraAngle.x);
            _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        var deltaCamAngle = (CameraAngle - _CameraCurentAngle) * CameraSoftRotation;
        _CameraCurentAngle += deltaCamAngle;
        E.transform.rotation = Quaternion.Euler(deltaCamAngle + _CameraCurentAngle);

        Camera.main.transform.rotation = E.transform.rotation * E.transform.GetChild(0).localRotation;
        Camera.main.transform.position = E.transform.GetChild(0).position;
        


        float mw = Input.GetAxis("Mouse ScrollWheel");
        size += mw * -10 * SpeedSizing * size;

        if (size < MinSize)
            size = MinSize;
        else if (size > MaxSize)
            size = MaxSize;
        CuretSize += (size - CuretSize) * SizeSoftSizing;
        E.transform.localScale = new Vector3(1, CuretSize, CuretSize);

    }

}
